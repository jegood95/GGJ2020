using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintingScrap : MonoBehaviour, Selectable
{
    public MeshRenderer Renderer;
    public MeshRenderer RendererTransparent;
    public MeshCollider Collider;
    public GameObject Hover;
    public Vector3 ViewingPositon;
    public float PercentToComplete;
    
    private ScrapData _Scrap;
    private Vector2 _Size;
    private Texture2D _Texture;
    private bool _IsDone = false;
    private float _PercentComplete;

    public float PercentComplete
    {
        get { return _PercentComplete; }
    }

    public bool IsDone
    {
        get { return _IsDone; }
    }

    public ScrapData Scrap
    {
        get { return _Scrap; }
    }

    void Start()
    {
        Hover.SetActive(false);
    }

    public void SetScrap(ScrapData inScrap)
    {
        if (_Scrap == inScrap)
        {
            return;
        }

        _Scrap = inScrap;
        _Size = new Vector2(_Scrap.Texture.width, _Scrap.Texture.height);
        _Texture = new Texture2D((int)_Size.x, (int)_Size.y);

        Color[] pixels = _Texture.GetPixels();
        for (int index = 0; index < pixels.Length; index++)
        {
            pixels[index].a = 0f;
        }
        _Texture.SetPixels(pixels);
        _Texture.Apply();
        
        Renderer.material.mainTexture = _Texture;
        Renderer.material.color = Color.white;
        Color transparent = Color.white;
        transparent.a = 0.5f;
        RendererTransparent.material.mainTexture = _Scrap.Texture;
        RendererTransparent.material.color = transparent;
        
        Hover.SetActive(false);
    }
    
    public void Paint(Vector2 inPoint, Color inColor, int inBrushSize)
    {
        if (_Scrap == null)
        {
            return;
        }
        
        inPoint *= _Size;

        int halfBrushSize = inBrushSize / 2;
        int quaterBrushSize = halfBrushSize / 2;
        
        if ((inPoint.x - quaterBrushSize) < 0 ||
            (inPoint.x + quaterBrushSize) >= _Size.x ||
            (inPoint.y - quaterBrushSize) < 0 ||
            (inPoint.y + quaterBrushSize) >= _Size.y)
        {
            return;
        }

        int startingX = (int)inPoint.x - halfBrushSize;
        int endingX = startingX + inBrushSize;
        
        int startingY = (int)inPoint.y - halfBrushSize;
        int endingY = startingY + inBrushSize;
        
        for (int x = startingX; x < endingX; x++)
        {
            if (x < 0 ||
                x >= _Size.x)
            {
                continue;
            }
            
            for (int y = startingY; y < endingY; y++)
            {
                if (y < 0 ||
                    y >= _Size.y)
                {
                    continue;
                }
                
                _Texture.SetPixel(x, y, inColor);
            }
        }
        
        _Texture.Apply();
    }

    public void OnHover()
    {
        Hover.SetActive(true);
    }

    public void OnUnhover()
    {
        Hover.SetActive(false);
    }

    public void OnSelect(RaycastHit inHit, PlayerController inPlayer)
    {
        Hover.SetActive(false);
        RendererTransparent.enabled = PaintingManager.Instance.ShowGuide;
    }

    public void OnDeselect()
    {
        RendererTransparent.enabled = _IsDone == false;
    }

    public Vector3 GetViewingPosition()
    {
        return transform.position + ViewingPositon;
    }

    public Quaternion GetViewingRotation()
    {
        // Do Nothing
        return Quaternion.identity;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.position + ViewingPositon, 0.5f);
    }

    public float Evaluate()
    {
        if (_Scrap == null ||
            _Scrap.Texture == null)
        {
            return 0f;
        }
        
        Color[] original = _Scrap.Texture.GetPixels();
        Color[] painted = _Texture.GetPixels();

        int amountCorrect = 0;
        for (int index = 0; index < painted.Length; index++)
        {
            if (painted[index].a == original[index].a)
            {
                amountCorrect++;
            }
        }

        _PercentComplete = (float) amountCorrect / (float) painted.Length;
        return _PercentComplete;
    }

    public void CheckIsDone()
    {
        _IsDone = Evaluate() >= PercentToComplete;
    }
}
