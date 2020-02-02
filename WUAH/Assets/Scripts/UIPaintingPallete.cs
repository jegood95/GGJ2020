using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPaintingPallete : MonoBehaviour
{
	public GameObject ColorPrefab;
	public GameObject BrushPrefab;

	public Transform Colors;
	public Transform Brushes;

	private List<UIColor> _Colors = new List<UIColor>();
	private List<UIBrush> _Brushes = new List<UIBrush>();
	private PaintingManager _Manager;

	void Start()
	{
		_Manager = PaintingManager.Instance;

		Color earaser = new Color(0f, 0f, 0f, 0f);
		GameObject eraserColorInstance = Instantiate(ColorPrefab, Colors);
		UIColor uiEraserColor = eraserColorInstance.GetComponent<UIColor>();
		uiEraserColor.Initialize(this, earaser, false, inIsEraser: true);
		_Colors.Add(uiEraserColor);
		
		foreach (Color color in _Manager.Colors)
		{
			bool selected = color == _Manager.Player.Color;
			GameObject colorInstance = Instantiate(ColorPrefab, Colors);
			UIColor uiColor = colorInstance.GetComponent<UIColor>();
			uiColor.Initialize(this, color, selected);
			_Colors.Add(uiColor);
		}

		int max = int.MinValue;
		foreach (int brush in _Manager.Brushes)
		{
			max = Mathf.Max(brush, max);
		}

		foreach (int brush in _Manager.Brushes)
		{
			bool selected = brush == _Manager.Player.Brush;
			GameObject brushInstance = Instantiate(BrushPrefab, Brushes);
			UIBrush uiBrush = brushInstance.GetComponent<UIBrush>();
			uiBrush.Initialize(this, brush, max, _Manager.Player.Color, selected);
			_Brushes.Add(uiBrush);
		}
	}

	public void SelectColor(Color inColor, bool inIsEraser)
	{
		Color brushColor = (inIsEraser == true) ? Color.white : inColor;
		foreach (UIBrush brush in _Brushes)
		{
			brush.SetColor(brushColor);
		}
		
		foreach (UIColor color in _Colors)
		{
			if (color.Color == inColor)
			{
				color.Select();
			}
			else
			{
				color.Deselect();
			}
		}
		
		_Manager.Player.SetColor(inColor);
	}
	
	public void SelectBrush(int inBrush)
	{
		foreach (UIBrush brush in _Brushes)
		{
			if (brush.Brush == inBrush)
			{
				brush.Select();
			}
			else
			{
				brush.Deselect();
			}
		}
		
		_Manager.Player.SetBrushSize(inBrush);
	}
}
