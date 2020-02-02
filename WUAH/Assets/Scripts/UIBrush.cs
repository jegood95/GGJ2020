using UnityEngine;
using UnityEngine.UI;

public class UIBrush : MonoBehaviour
{
    public Image Image;
    public Outline Outline;
	
    private UIPaintingPallete _Pallete;
    private int _Brush;

    public int Brush
    {
	    get { return _Brush; }
    }

    public void Initialize(UIPaintingPallete inPallete, int inBrush, int inMaxBrush, Color inColor, bool inSelected)
    {
        _Pallete = inPallete;
        _Brush = inBrush;
        float scale = (float)_Brush / (float)inMaxBrush;
        Image.transform.localScale = new Vector3(scale, scale, scale);
        SetColor(inColor);

        if (inSelected == true)
        {
	        Select();
        }
        else
        {
	        Deselect();
        }
    }

    public void SetColor(Color inColor)
    {
	    Image.color = inColor;
    }

    public void OnSelect()
    {
		_Pallete.SelectBrush(_Brush);
    }
    
    public void Select()
    {
	    Outline.enabled = true;
    }

    public void Deselect()
    {
	    Outline.enabled = false;
    }
}
