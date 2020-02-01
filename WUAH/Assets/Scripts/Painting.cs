using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Painting : MonoBehaviour, Selectable
{
    public MeshRenderer Renderer;
    public int Size;
    public ParticleSystem Hover;
    public Vector3 ViewingPositon;

    private Texture2D _Texture;

    void Start()
    {
        _Texture = new Texture2D(Size, Size);
        Renderer.material.mainTexture = _Texture;
        Hover.Stop();
    }
    
    public void Paint(Vector2 inPoint, Color inColor, int inBrushSize)
    {
        inPoint *= Size;

        int halfBrushSize = inBrushSize / 2;
        int quaterBrushSize = halfBrushSize / 2;
        
        if ((inPoint.x - quaterBrushSize) < 0 ||
            (inPoint.x + quaterBrushSize) >= Size ||
            (inPoint.y - quaterBrushSize) < 0 ||
            (inPoint.y + quaterBrushSize) >= Size)
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
                x >= Size)
            {
                continue;
            }
            
            for (int y = startingY; y < endingY; y++)
            {
                if (y < 0 ||
                    y >= Size)
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
        inPlayer.ChangeMode(InputMode.Painting);
    }

    public Vector3 GetViewingPosition()
    {
        return transform.position + ViewingPositon;
    }

    public Quaternion GetViewingRotation()
    {
        return transform.rotation;
    }
}
