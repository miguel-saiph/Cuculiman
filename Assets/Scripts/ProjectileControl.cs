using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileControl : MonoBehaviour {

	public float speed = 10f;

	public bool shootRight = true;
	// Use this for initialization
	void Start () {

		if (!shootRight) {
			speed *= -1;
		}
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		transform.Translate (Vector3.right * speed * Time.deltaTime);
		
	}
}
