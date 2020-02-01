using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintingScrap : MonoBehaviour, Selectable
{
    public MeshRenderer Renderer;
    public MeshRenderer RendererTransparent;
    public MeshCollider Collider;
    public ParticleSystem Hover;
    public Vector3 ViewingPositon;
    
    private ScrapData _Scrap;
    private Vector2 _Size;
    private Texture2D _Texture;
    private Texture2D _TextureTransparent;

    public void SetScrap(ScrapData inScrap)
    {
        _Scrap = inScrap;
        _Size = new Vector2(_Scrap.Texture.width, _Scrap.Texture.height);
        _Texture = new Texture2D((int)_Size.x, (int)_Size.y);
        _TextureTransparent = new Texture2D(_Texture.width, _Texture.height);
        _TextureTransparent.SetPixels(_Scrap.Texture.GetPixels());
        Renderer.material.mainTexture = _Texture;
        Renderer.material.color = Color.white;
        Color transparent = Color.white;
        transparent.a = 0.25f;
        RendererTransparent.material.mainTexture = _TextureTransparent;
        RendererTransparent.material.color = transparent;
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
        Hover.Play();
    }

    public void OnUnhover()
    {
        Hover.Stop();
        Hover.Clear();
    }

    public void OnSelect(RaycastHit inHit, PlayerController inPlayer)
    {
        // Do nothing
    }

    public void OnDeselect()
    {
        // Do nothing
    }

    public Vector3 GetViewingPosition()
    {
        // Do nothing
        return Vector3.zero;
    }

    public Quaternion GetViewingRotation()
    {
        // Do nothing
        return Quaternion.identity;
    }
}
