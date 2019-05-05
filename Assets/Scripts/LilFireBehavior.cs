using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LilFireBehavior : MonoBehaviour {

	public float speed = -1;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		transform.Translate (Vector2.right * speed * Time.deltaTime);
		
	}
}
