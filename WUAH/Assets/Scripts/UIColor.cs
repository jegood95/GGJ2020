using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIColor : MonoBehaviour
{
	public Image Image;
	public Outline Outline;
	public Text Eraser;
	
	private UIPaintingPallete _Pallete;
	private Color _Color;
	private bool _IsEraser;

	public Color Color
	{
		get { return _Color; }
	}

	public void Initialize(UIPaintingPallete inPallete, Color inColor, bool inSelected, bool inIsEraser = false)
	{
		_Pallete = inPallete;
		_Color = inColor;
		Image.color = _Color;
		
		if (inSelected == true)
		{
			Select();
		}
		else
		{
			Deselect();
		}

		_IsEraser = inIsEraser;
		if (_IsEraser == true)
		{
			Image.color = Color.white;
			Eraser.enabled = true;
		}
		else
		{
			Eraser.enabled = false;
		}
	}

	public void OnSelect()
	{
		_Pallete.SelectColor(_Color, _IsEraser);
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
