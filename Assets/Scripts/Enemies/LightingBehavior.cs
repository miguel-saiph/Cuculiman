using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightingBehavior : MonoBehaviour {

	public float speed = 3f;
	//public float angle = 180f;

	//private Vector3 currentPosition;
	//private Vector3 newPosition;

	private Vector3 targetPosition;
	private Vector3 normalizedDirection;

	// Use this for initialization
	void Start() {
		
		//newPosition = PolarCoordinates (angle, 1f);
		targetPosition = GameObject.Find("JP").transform.position;
		normalizedDirection = (targetPosition - transform.position).normalized;

	}
	
	// Update is called once per frame
	void FixedUpdate () {
			
		//transform.Translate (newPosition * speed * Time.deltaTime);
		//transform.localPosition = Vector2.MoveTowards (transform.localPosition, newPosition, speed * Time.deltaTime);
		transform.position += normalizedDirection * speed * Time.deltaTime;
	}

	/*
	Vector3 PolarCoordinates(float angle, float radius) {
		Vector3 relativePosition = new Vector3 (radius * Mathf.Cos (angle), radius * Mathf.Sin (angle), 0);

		return currentPosition + relativePosition;
	}*/

	void OnBecameInvisible() {
		Destroy (gameObject);
	}

}
