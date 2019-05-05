using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropFire : MonoBehaviour {

	public GameObject lilFirePrefab;
	public float timeToDrop = 4;

	private float savedTime;

	// Use this for initialization
	void Start () {

		savedTime = timeToDrop;
		
	}
	
	// Update is called once per frame
	void Update () {

		if (Time.time >= savedTime) {

			gameObject.GetComponent<Animator> ().SetTrigger ("dropFire");
			savedTime = Time.time + timeToDrop;
		}
		
	}

	public void SpawnLilFire() {

		Instantiate (lilFirePrefab, new Vector2 (transform.position.x - 0.4f, transform.position.y), transform.rotation);
	}
}
