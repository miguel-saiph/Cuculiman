using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroMovement : MonoBehaviour {

	public float speed = 1f;
	public int timeToStart = 5;

	private bool stop = true;

	void Start () {

		Invoke ("Empezar", timeToStart);
	}
	
	// Update is called once per frame
	void Update () {

		if (transform.position.y >= 9.65f && !stop) {
			stop = true;
			IntroManager.im.ShowMenu ();
		}

		if (!stop) {
			speed += 0.001f;
			transform.Translate (speed * Vector2.up * Time.deltaTime);
		}
		
	}

	void Empezar() {
		
		stop = false;
	}

}
