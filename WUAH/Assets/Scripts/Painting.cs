using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Painting : MonoBehaviour
{
    public MeshRenderer Renderer;
    public int Size;

    private Texture2D _Texture;

    void Start()
    {
        _Texture = new Texture2D(Size, Size);
        Renderer.material.mainTexture = _Texture;
    }
    
    public void Paint(Vector2 inPoint, Color inColor, int inBrushSize)
    {
        inPoint *= Size;

        int startingX = (int)inPoint.x - inBrushSize/2;
        int endingX = startingX + inBrushSize;
        
        int startingY = (int)inPoint.y - inBrushSize/2;
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
}
