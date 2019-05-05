using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnFirePillar : MonoBehaviour {

	public GameObject firePillarPrefab1;
	public GameObject firePillarPrefab2;
	public float secondsBetweenSpawn = 5f;

	public Vector3 viewPortPoint1; //El espacio relativo a la cámara donde debería aparecer el pilar
	public Vector3 viewPortPoint2;

	public Text topText;
	public Text bottomText;

	private bool spawnNormal = true;

	private float savedTime;
	private int contador = 1;

	// Use this for initialization
	void Start () {

		savedTime = secondsBetweenSpawn;
		
	}
	
	// Update is called once per frame
	void Update () {

		if (Time.time >= savedTime) {
			if (spawnNormal) {
				Instantiate (firePillarPrefab1, Camera.main.ViewportToWorldPoint (viewPortPoint1), Quaternion.identity);
				spawnNormal = !spawnNormal;
				contador++;
				Invoke ("EnableTop", 2f);

			} else {
				Instantiate (firePillarPrefab2, Camera.main.ViewportToWorldPoint (viewPortPoint2), firePillarPrefab2.transform.rotation);
				spawnNormal = !spawnNormal;
				contador++;
				Invoke ("EnableBottom", 2f);

			}
			savedTime = Time.time + secondsBetweenSpawn;
		}
	}

	void EnableTop() {
		if (contador == 3) {
			topText.text = "VALOR QUE HACE CALOR, QUE HACE CALOR, QUE HACE CALOR";
			contador = 0;
		} else {
			topText.text = "VALOR QUE HACE CALOR";
		}
		bottomText.enabled = false;
		topText.enabled = true;
	}

	void EnableBottom() {
		if (contador == 3) {
			bottomText.text = "VALOR QUE HACE CALOR, QUE HACE CALOR, QUE HACE CALOR";
			contador = 0;
		} else {
			bottomText.text = "VALOR QUE HACE CALOR";
		}
		bottomText.enabled = true;
		topText.enabled = false;
	}
}
