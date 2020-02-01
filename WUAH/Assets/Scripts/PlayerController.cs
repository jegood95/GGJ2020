using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Transform MyTransform;
	public CharacterController Controller;
	public float Speed;
	public Vector2 Sensitivity;

	private float _MinX = -360f;
	private float _MaxX = 360f;
	private float _MinY = -60f;
	private float _MaxY = 60f;
	private float rotationY = 0f;

	private void Start()
	{
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}

	// Update is called once per frame
	void Update ()
	{
		float rotationX = MyTransform.localEulerAngles.y + Input.GetAxis("Mouse X") * Sensitivity.x;
		rotationY += Input.GetAxis("Mouse Y") * Sensitivity.y;
		rotationY = Mathf.Clamp(rotationY, _MinY, _MaxY);
		MyTransform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);

		// float horizontal = Input.GetAxis("Horizontal");
		float vertical = Input.GetAxis("Vertical");
		Vector3 forward = MyTransform.TransformDirection(Vector3.forward);
		forward.Normalize();
		float curSpeed = Speed * vertical;
		Controller.SimpleMove(forward * curSpeed);

		forward.y = 0f;
		forward.Normalize();
		float horizontal = Input.GetAxis("Horizontal");
		curSpeed = Speed * horizontal;
		Vector3 left = -Vector3.Cross(forward, Vector3.up).normalized;
		Controller.SimpleMove(left * curSpeed);
	}
}
