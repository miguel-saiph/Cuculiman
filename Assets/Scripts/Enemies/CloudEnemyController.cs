using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudEnemyController : MonoBehaviour {


	public float speed = 3f;

	public Vector2 leftLimit;
	public Vector2 rightLimit;

	public bool facingRight = false;

	public GameObject lighting;
	public bool hasLighting = true;
	public float lightingCooldown;

	private Animator anim;


	void Start () {

		//Porque el sprite está originalmente orientado a la izquierda
		if (facingRight) {
			speed *= -1;
			var s = transform.localScale;
			s.x *= -1;
			transform.localScale = s;
		}

	}
	
	// Update is called once per frame
	void FixedUpdate () {

		if (transform.localPosition.x >= rightLimit.x || transform.localPosition.x <= leftLimit.x) {
			Flip ();
		}
		transform.Translate (Vector2.left * speed * Time.deltaTime);
		
	}
		

	void Flip() {

		speed *= -1;
		var s = transform.localScale;
		s.x *= -1;
		transform.localScale = s;
		facingRight = !facingRight;
	}

	public void DropLighting() {

		if (hasLighting) {
			if (facingRight) {
				GameObject newLighting = Instantiate (lighting, new Vector2 (transform.position.x + 0.4f, transform.position.y + 0.1f), transform.rotation);
				//Flipping lighting
				var s = newLighting.transform.localScale;
				s.x *= -1;
				newLighting.transform.localScale = s;
			} else {
				Instantiate (lighting, new Vector2 (transform.position.x - 0.4f, transform.position.y - 0.1f), transform.rotation);
			}
		}
		hasLighting = false;
		Invoke ("NewLighting", lightingCooldown); //Cooldown

	}

	void NewLighting() {
		hasLighting = true;
	}
}
