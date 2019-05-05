using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomCamera2D : MonoBehaviour {

	public Transform target;

	public float xOffset = 0f;
	public float yOffset = 0f;

	public float xLimit = 9;
	public float yLimit = 6;

	public bool isXLocked = false;
	public bool isYLocked = false;

	private float cameraSize;

	void Start () {

		cameraSize = Camera.main.orthographicSize;
		
	}
	
	// Update is called once per frame
	void Update () {

		float xTarget = target.position.x + xOffset;
		float yTarget = target.position.y + yOffset;

		//Locking in the limits
		if (Mathf.Abs(xTarget) >= xLimit - cameraSize * 2) {
			isXLocked = true;
		} else {
			isXLocked = false;
		}

		if (Mathf.Abs(yTarget) >= yLimit - 3) {
			isYLocked = true;
		} else {
			isYLocked = false;
		}
			
		float xNew = transform.position.x;
		if (!isXLocked) {
			xNew = xTarget;
		}

		float yNew = transform.position.y;
		if (!isYLocked) {
			yNew = yTarget;
		}
			
		transform.position = new Vector3 (xNew, yNew, transform.position.z);
		
	}
}
