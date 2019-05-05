using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropLighting : MonoBehaviour {

	public GameObject cloudEnemy;
	public bool isLeftTrigger;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (!cloudEnemy) {
			Destroy (transform.parent.gameObject);
		}
		
	}

	void OnTriggerEnter2D (Collider2D other) {


		if (other.tag == "Player" || other.tag == "Invincible") {

			if (cloudEnemy) {
				if (cloudEnemy.GetComponent<CloudEnemyController> ().hasLighting) {
					//SI es trigger izquierdo dispara sólo si está mirando a la izquierda
					if (isLeftTrigger) {
						if (!cloudEnemy.GetComponent<CloudEnemyController> ().facingRight) {
							cloudEnemy.GetComponent<Animator> ().SetTrigger ("lighting");
						}
					} else {
						if (cloudEnemy.GetComponent<CloudEnemyController> ().facingRight) {
							cloudEnemy.GetComponent<Animator> ().SetTrigger ("lighting");
						}
					}
				}
			}
		}
	}

	void OnTriggerStay2D (Collider2D other) {

		if (other.tag == "Player" || other.tag == "Invincible") {

			if (cloudEnemy) {
				if (cloudEnemy.GetComponent<CloudEnemyController> ().hasLighting) {
					//SI es trigger izquierdo dispara sólo si está mirando a la izquierda
					if (isLeftTrigger) {
						if (!cloudEnemy.GetComponent<CloudEnemyController> ().facingRight) {
							cloudEnemy.GetComponent<Animator> ().SetTrigger ("lighting");
						}
					} else {
						if (cloudEnemy.GetComponent<CloudEnemyController> ().facingRight) {
							cloudEnemy.GetComponent<Animator> ().SetTrigger ("lighting");
						}
					}
				}
			}
		}
	}
}
