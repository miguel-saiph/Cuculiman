using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Play the intro an then the music with loop
public class LoopMusic : MonoBehaviour {

	public AudioClip introMusic;
	public AudioClip stageMusic;

	// Use this for initialization
	void OnEnable () {

		GetComponent<AudioSource> ().Stop ();
		GetComponent<AudioSource> ().clip = introMusic;
		GetComponent<AudioSource> ().Play();

		//StartCoroutine (ChangeMusic ());
	}
	
	// Update is called once per frame
	void Update () {


		if (!GetComponent<AudioSource>().isPlaying) {
			GetComponent<AudioSource> ().Stop ();
			GetComponent<AudioSource> ().clip = stageMusic;
			GetComponent<AudioSource> ().loop = true;
			GetComponent<AudioSource> ().Play ();
			this.enabled = false;

		}
	}

	IEnumerator ChangeMusic() {

		yield return new WaitForSeconds (introMusic.length - 1f);

		GetComponent<AudioSource> ().Stop ();
		GetComponent<AudioSource> ().clip = stageMusic;
		GetComponent<AudioSource> ().loop = true;
		GetComponent<AudioSource> ().Play ();

	}
}
