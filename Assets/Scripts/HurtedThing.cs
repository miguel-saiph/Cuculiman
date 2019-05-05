using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtedThing : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		transform.position = GetComponentInParent<Transform> ().position;

		
	}
}
