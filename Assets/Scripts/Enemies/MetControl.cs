using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetControl : MonoBehaviour {

	public GameObject projectilePrefab;
	public float projectileSpeed;
	public int hp = 5;
	public float timeOfColor = 0.2f; //El tiempo en que va a estar de otro color cuando reciba un disparo
	public GameObject explosionPrefab;
	public bool pivotIsCentered = true; //Hacer un objeto vacío en el centro si esto es falso
	public bool facingRight;
	public AudioClip damaged;
	public AudioClip noDamaged;

	private bool canBeDamaged = false;
	private GameObject projectile;

	private List<GameObject> projectiles = new List<GameObject>();

	// Use this for initialization
	void Start () {

		if (facingRight) {
			var s = transform.localScale;
			s.x *= -1;
			transform.localScale = s;
		}
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D (Collider2D other) {

		if (other.tag == "Lemon") {
			
			Destroy (other.gameObject);

			if (canBeDamaged) {
				if (this.GetComponent<ReSkinAnimation> ()) {
					this.GetComponent<ReSkinAnimation> ().enabled = true;
					Invoke ("DisableReSkin", timeOfColor);
				}
				//Sound
				GetComponent<AudioSource>().clip = damaged;
				GetComponent<AudioSource> ().Play ();

				//Destruction
				hp -= 1;
				if (hp == 0) {
					Destroy (gameObject);
					if (explosionPrefab) {
						Vector3 enemyPosition;
						if (!pivotIsCentered) {
							enemyPosition = transform.GetChild (0).position;
						} else {
							enemyPosition = transform.position;
						}
						Instantiate (explosionPrefab, enemyPosition, Quaternion.identity);
					}
						
				}
			} else {
				
				//Wrong sound
				GetComponent<AudioSource>().clip = noDamaged;
				GetComponent<AudioSource> ().Play ();
			}
		}
	}

	public void MetShoot() {

		if (!canBeDamaged) {
			canBeDamaged = true;
		}

		if (facingRight) {
			projectile = Instantiate (projectilePrefab, new Vector3 (transform.position.x + 0.25f, transform.position.y - 0.015f, transform.position.y), transform.rotation);
			projectile.GetComponent<MetShootBehavior> ().speed *= -1;
		} else {
			projectile = Instantiate (projectilePrefab, new Vector3 (transform.position.x - 0.25f, transform.position.y - 0.015f, transform.position.y), transform.rotation);
		}

		projectiles.Add (projectile);

	}

	public void ReturnToShell() {

		canBeDamaged = false;
	}

	void DisableReSkin() {

		if (this.GetComponent<ReSkinAnimation>()) {
			this.GetComponent<ReSkinAnimation> ().enabled = false;
		}

	}

	void OnDestroy() {

		/*
		//Destroy all the remaining bullets
		foreach (GameObject pr in projectiles) {

			Destroy (pr);
		} */

		//Send message to the spawner to spawn again
		if (GetComponentInParent<CameraSpawner>()) {
			GetComponentInParent<CameraSpawner> ().hasSpawned = false;
		}

	}
}
