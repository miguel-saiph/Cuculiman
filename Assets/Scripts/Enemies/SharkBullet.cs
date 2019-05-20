using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkBullet : MonoBehaviour {

    [SerializeField] private float speed;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }
}
