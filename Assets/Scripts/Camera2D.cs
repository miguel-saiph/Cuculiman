using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera2D : MonoBehaviour {

	public Transform target;

	/*
	public Transform trackingtarget {
		get { return trackingtarget; }
		set { trackingtarget = value; }
	} */

	public float xOffset = 0f;
	public float yOffset = 0f;

	public float followSpeed = 2f;

	public bool isXLocked = false;
	public bool isYLocked = false;

	public float xLimitLeft;
	public float xLimitRight;

	void Start () {
		
	}
	
	// Update is called once per frame
	void LateUpdate () {

		//if (Camera.main.WorldToViewportPoint (new Vector2 (xLimit, 0)).x <= 0) {
		//	isXLocked = true;
		//}

		//Para que la cámara no siga al personaje hasta que este se mueva
		//90 is to lock near boss room (may be other number on other stages)
		/*
		if (isXLocked) {
			if (target.position.x >= Camera.main.ViewportToWorldPoint (new Vector2 (0.5f + xOffset, 0)).x && target.position.x <= 90f) {
				isXLocked = false;
			}
		}
		*/

		if (isXLocked) {
			if (target.position.x >= Camera.main.ViewportToWorldPoint (new Vector2 (0.5f + xOffset, 0)).x) {
				isXLocked = false;
			}
		}

		float xTarget = target.position.x + xOffset;
		float yTarget = target.position.y + yOffset;

		//Interpolating to reduce dizzines
		float xNew = transform.position.x;
		if (!isXLocked) {
			xNew = Mathf.Lerp (transform.position.x, xTarget, Time.deltaTime * followSpeed);
		}

		float yNew = transform.position.y;
		if (!isYLocked) {
			yNew = Mathf.Lerp (transform.position.y, yTarget, Time.deltaTime * followSpeed);
		}
			
		//Limita el movimiento de la cámara hacia la izquierda
		if (Mathf.Abs(xLimitLeft) > 0) {
			//Cuando el límite se hace visible a la cámara y se le pide ir hacia la izquierda
			if (Camera.main.WorldToViewportPoint (new Vector2 (xLimitLeft, 0)).x >= 0 && xNew <= transform.position.x) {
				xNew = transform.position.x;
			}
		}

		//Limita el movimiento de la cámara hacia la derecha
		if (Mathf.Abs(xLimitRight) > 0) {
			if (Camera.main.WorldToViewportPoint (new Vector2 (xLimitRight, 0)).x <= 1 && xNew >= transform.position.x) {
				xNew = transform.position.x;
			}
		}

		transform.position = new Vector3 (xNew, yNew, transform.position.z);

	}
}
