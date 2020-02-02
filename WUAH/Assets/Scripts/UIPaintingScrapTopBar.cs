using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPaintingScrapTopBar : MonoBehaviour
{
	public GameObject ShowGuideCheckMark;
	public Text PercentComplete;
	
	private PaintingManager _Manager;
	private float _Timer;
	
	void Start()
	{
		_Manager = PaintingManager.Instance;
		ShowGuideCheckMark.SetActive(_Manager.ShowGuide);
		_Timer = 0.5f;
	}

	private void OnEnable()
	{
		_Manager = PaintingManager.Instance;
		UpdatePercentComplete();
	}

	private void Update()
	{
		if (_Timer > 0f)
		{
			_Timer -= Time.deltaTime;
		}
		else
		{
			UpdatePercentComplete();
			_Timer = 0.5f;
		}
	}

	private void UpdatePercentComplete()
	{
		if (_Manager != null &&
			_Manager.Player != null &&
		    _Manager.Player.Scrap != null)
		{
			_Manager.Player.Scrap.CheckIsDone();
			float percent = _Manager.Player.Scrap.PercentComplete;
			PercentComplete.text = $"{percent:P}";
			PercentComplete.color = _Manager.Player.Scrap.IsDone ? Color.green : Color.red;
		}
	}

	public void OnShowGuideToggle()
	{
		_Manager.ShowGuide = !_Manager.ShowGuide;
		ShowGuideCheckMark.SetActive(_Manager.ShowGuide);
		_Manager.Player.Scrap.RendererTransparent.enabled = _Manager.ShowGuide;
	}
}
