using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Clase que se activa al ser golpeado

public class Invincibility : MonoBehaviour {

	public float duration = 3f;

	private SpriteRenderer spriteRenderer;
	private string originalTag;

	private float realTime;

	// Use this for initialization
	void Awake () {

		spriteRenderer = this.GetComponent<SpriteRenderer> ();
		originalTag = gameObject.tag;
	}

	void OnEnable() {

		//Cambia el tag para que los enemigos no le hagan daño
		gameObject.tag = "Invincible";
		realTime = Time.time + duration;

	}
	
	// Update is called once per frame
	void Update () {

		//Si no se ha complido el tiempo, activa y desactiva el sprite
		if (Time.time <= realTime) {
			if (spriteRenderer.enabled) {
				spriteRenderer.enabled = false;
			} else {
				spriteRenderer.enabled = true;
			}
		} else {
			//Para asegurarme que siempre terminará con el sprite activo
			if (!spriteRenderer.enabled) {
				spriteRenderer.enabled = true;
			}
			gameObject.tag = originalTag;
			gameObject.GetComponent<Invincibility> ().enabled = false;

		}
		
	}
}
