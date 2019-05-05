using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetShootBehavior : MonoBehaviour {

	public float speed;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void FixedUpdate () {

		transform.position += Vector3.left * speed * Time.deltaTime;
		
	}
}
