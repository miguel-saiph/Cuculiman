using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffCameraDestroyer : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

	void Update() {


	}
	
	// Update is called once per frame
	void OnBecameInvisible () {
		
		Destroy (gameObject);
	}
}
