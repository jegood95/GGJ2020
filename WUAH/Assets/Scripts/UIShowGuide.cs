using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIShowGuide : MonoBehaviour
{
	public GameObject CheckMark;
	private PaintingManager _Manager;
	
	void Start()
	{
		_Manager = PaintingManager.Instance;
		CheckMark.SetActive(_Manager.ShowGuide);
	}

	public void OnToggle()
	{
		_Manager.ShowGuide = !_Manager.ShowGuide;
		CheckMark.SetActive(_Manager.ShowGuide);
		_Manager.Player.Scrap.RendererTransparent.enabled = _Manager.ShowGuide;
	}
}
