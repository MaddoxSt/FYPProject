using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followCam : MonoBehaviour {

	float yaw;
	float pitch;

	public Transform target;
	public float distanceFromTarget = 2f;

	public Transform towardsPlayerMove;

	public float mouseSensitivity = 5f;

	public Vector2 maxMin = new Vector2(-5,30);

	public float rotationSmooth = 0.2f;

	Vector3 rotationSmoothVelocity;
	Vector3 currentRotation;


	void Start(){

		towardsPlayerMove.transform.position = target.transform.position;
		towardsPlayerMove.transform.parent = target.transform;
		Cursor.lockState=CursorLockMode.Locked;
		
		


	}
	void LateUpdate(){

		yaw += Input.GetAxis ("Mouse X")*mouseSensitivity;
		pitch-=Input.GetAxis("Mouse Y")* mouseSensitivity;
		pitch = Mathf.Clamp (pitch, maxMin.x, maxMin.y);

		currentRotation = Vector3.SmoothDamp (currentRotation, new Vector3 (pitch, yaw), ref rotationSmoothVelocity, rotationSmooth);

		transform.eulerAngles = currentRotation;

		transform.position = target.position - transform.forward * distanceFromTarget;
	}
}
