using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIReposition : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Awake () {

		/*
		if ((Screen.width == 640 && Screen.height == 480) || (Screen.width == 800 && Screen.height == 600)) {

			gameObject.GetComponent<RectTransform>().position = new Vector3(transform.position.x, transform.position.y - 64, transform.position.z);
		}*/

		if (Camera.main.aspect < 1.33) { //Camera aspect = width / height
			gameObject.GetComponent<RectTransform>().position = new Vector3(transform.position.x, transform.position.y - 64, transform.position.z);
		}
	}
}
