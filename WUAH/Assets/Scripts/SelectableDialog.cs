using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectableDialog : MonoBehaviour, Selectable
{
	public ChainedDialog Dialog;
	public ParticleSystem Hover;

	public void OnHover()
	{
		Hover.Play();
	}

	public void OnUnhover()
	{
		Hover.Stop();
	}

	public void OnSelect(RaycastHit inHit, PlayerController inPlayer)
	{
		UIManager.Instance.Dialog.Queue(Dialog);
	}

	public void OnDeselect()
	{
		// Do Nothing
	}

	public Vector3 GetViewingPosition()
	{
		return Vector3.zero;
	}

	public Quaternion GetViewingRotation()
	{
		return Quaternion.identity;
	}
}
