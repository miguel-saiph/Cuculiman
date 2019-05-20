using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FiretubFlame : MonoBehaviour {

    private Animator anim;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        anim.SetTrigger("ignite");
        
    }
}
