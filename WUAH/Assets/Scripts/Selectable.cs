using UnityEngine;

public interface Selectable
{
	void OnHover();
	void OnUnhover();
	void OnSelect(RaycastHit inHit, PlayerController inPlayer);
	void OnDeselect();
	Vector3 GetViewingPosition();
	Quaternion GetViewingRotation();
}
