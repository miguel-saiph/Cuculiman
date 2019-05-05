using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossLifeBarBehavior : MonoBehaviour {

	public int bossHp = 29;
	public int healthAmount; //The power of the player's bullet
	public bool hasEnrage;
	public GameObject hurtedThing;
	public GameObject explosionPrefab;
	public AudioClip notHurted;
	public AudioClip hurted;
	public AudioClip victoryMusic;

	private GameObject[] bars;
	private List<string> barritas = new List<string> ();

	// Use this for initialization
	void Start () {

		bars = GameObject.FindGameObjectsWithTag ("BossBar");

		foreach (GameObject temp in bars)
		{
			barritas.Add(temp.name);
		}

		barritas.Sort ();

		/*
		//Disable bars 
		for (int i = 0; i <= bossHp; i++) {
			GameObject.Find(barritas[i]).GetComponent<Image>().enabled = false;
		} */

		//Enable bars with delay
		float time = 0.2f;
		for (int i = 0; i <= bossHp; i++) {
			StartCoroutine (EnableBar(i, time));
			time += 0.05f;
		}
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D (Collider2D other) {

		if (other.tag == "Lemon" && !GetComponentInParent<Invincibility> ().enabled) {

			if (!GetComponentInParent<Animator> ().GetCurrentAnimatorStateInfo (0).IsName ("Sleep")) {
				
				//Substract health
				if (healthAmount > bossHp) {
					healthAmount = bossHp;
				}

				for (int i = 0; i < healthAmount; i++) {
					GameObject.Find (barritas [bossHp]).SetActive (false);
					//barritas.Remove (barritas [playerHp]);
					bossHp -= 1;
				}

				GetComponent<AudioSource> ().clip = hurted;
				GetComponent<AudioSource> ().Play ();
			
				GetComponentInParent<Invincibility> ().enabled = true;
				if (hurtedThing) {
					Instantiate (hurtedThing, transform.GetChild (0).position, transform.rotation, transform);
				}
			} else {
				GetComponent<AudioSource> ().clip = notHurted;
				GetComponent<AudioSource> ().Play ();
			}


			//Destroying player's bullet
			Destroy (other.gameObject);

			
			//Enrage
			if (hasEnrage) {
				if (bossHp < 14) {
					if (GetComponentInParent<CelesteManController> ()) {
						if (GetComponentInParent<Animator> ().GetCurrentAnimatorStateInfo (0).IsName ("Jump")) {
							Invoke ("CelesteEnrage", 0.2f);
						} else {
							CelesteEnrage ();
						}
					} else {
						GetComponentInParent<RonquidomanController> ().enrageAnim = true;
					}
					hasEnrage = false;
				}
			}

			//Jump?
			if (GetComponentInParent<Animator> ().GetCurrentAnimatorStateInfo (0).IsName ("Run") 
				&& !GetComponentInParent<Animator> ().GetCurrentAnimatorStateInfo (0).IsName ("Glowing") 
				&& !GetComponentInParent<Animator> ().GetBool("enrage")
				&& !GetComponentInParent<Animator> ().GetCurrentAnimatorStateInfo (0).IsName ("Jump")) {

				GetComponentInParent<Rigidbody2D> ().velocity = Vector3.zero;
				GetComponentInParent<Rigidbody2D> ().AddForce (new Vector2 (0f, 200f));
				
			}


			//Death
			if (bossHp == 0) {
				Instantiate (explosionPrefab, transform.GetChild (0).position, transform.rotation);
				GameObject.Find(barritas[bossHp]).SetActive(false);
				GameObject.Find("JP").GetComponent<Animator> ().SetFloat("speed", 0);
				GameObject.Find ("JP").GetComponent<Animator> ().SetBool("jump", false);
				GameObject.Find ("JP").GetComponent<Animator> ().Play ("Idle");
				GameObject.Find("JP").GetComponent<Animator> ().SetTrigger ("reset");
				GameObject.Find("JP").GetComponent<JpControl> ().enabled = false;
				Camera.main.GetComponent<AudioSource> ().Stop();
				Camera.main.GetComponent<AudioSource> ().clip = victoryMusic;
				Camera.main.GetComponent<AudioSource> ().Play();
				GameManager.gm.Victory ();
				Destroy (transform.parent.gameObject);
			}
		}
	}

	IEnumerator EnableBar(int pos, float delay) {

		yield return new WaitForSeconds (delay);

		GameObject.Find(barritas[pos]).GetComponent<Image>().enabled = true;
		if (GameObject.Find(barritas[pos]).GetComponent<AudioSource>()) {
			GameObject.Find(barritas[pos]).GetComponent<AudioSource>().Play();
		}

		if (pos == bossHp) {
			if (GetComponentInParent<CelesteManController> ()) {
				GetComponentInParent<CelesteManController> ().enabled = true;
			} else {
				GetComponentInParent<RonquidomanController> ().enabled = true;
			}
			GameObject.Find("JP").GetComponent<JpControl> ().enabled = true;
			GetComponentInParent<Animator> ().SetTrigger ("start");
		}
	}

	void CelesteEnrage() {
		GetComponentInParent<Animator> ().SetBool ("enrage", true);
		GetComponentInParent<CelesteManController> ().Enrage ();
	}
		
}
