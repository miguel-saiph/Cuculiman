using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopAnimation : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GetComponent<Animator>().SetBool("Loop", true);
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
