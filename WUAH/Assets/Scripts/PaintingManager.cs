using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class PaintingManager : MonoBehaviour
{
	public PlayerController Player;
	public List<Color> Colors;
	public List<int> Brushes;

	public static PaintingManager Instance;

	private bool _ShowGuide;

	public bool ShowGuide
	{
		get { return _ShowGuide; }
		set { _ShowGuide = value; }
	}
	
	private void Awake()
	{
		Instance = this;
		Player.SetColor(Colors[0]);
		Player.SetBrushSize(Brushes[0]);
		_ShowGuide = true;
	}
}
