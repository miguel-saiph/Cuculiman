using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameDialogue : MonoBehaviour {

	public string[] dialogues_esp;
	public string[] dialogues_eng;

	public GameObject textBackground;

	public GameObject bossDoor;
	public AudioClip bossMusic;

	private GameObject textObject;
	private int index; //To acces to dialogues
	private bool canContinue = false;
	private int dialoguesCounter;
	private string[] dialogues;

	// Use this for initialization
	void OnEnable () {

		index = 0;
		if (GlobalOptions.language == GlobalOptions.Language.English) {
			dialogues = dialogues_eng;
		} else {
			dialogues = dialogues_esp;
		}
		dialoguesCounter = dialogues.Length;

		if(Camera.main.GetComponent<AudioSource>()) {
			Camera.main.GetComponent<AudioSource> ().volume = 0.3f;
		}

		GameObject.Find ("JP").GetComponent<Animator> ().SetBool ("jump", false);
		GameObject.Find ("JP").GetComponent<Animator> ().Play ("Idle");

		if (dialogues.Length > 0) {
			textBackground.SetActive (true);
			Time.timeScale = 0;
			textObject = textBackground.gameObject.transform.GetChild (0).gameObject;
			ShowDialogue (dialogues [0]);
		} else {
			this.enabled = false;
		}
			
		
	}
	
	// Update is called once per frame
	void Update () {

		if (dialoguesCounter <= 0) {
			enabled = false;
		}

		if (Input.GetButtonDown ("Jump") && canContinue) {
			dialoguesCounter--;
			if (index < dialogues.Length -  1) {
				index++;
				ShowDialogue (dialogues [index]);
			}
				
		}
		
	}


	void ShowDialogue(string dialogue) {

		canContinue = false;

		string newDialogue = "";
		int counter = dialogue.Length;
		float delay = 0.08f;

		textObject.GetComponent<Text> ().text = newDialogue;

		foreach (char word in dialogue) {
			counter--;
			newDialogue += word.ToString();
			StartCoroutine(TextWithDelay (newDialogue, delay, counter));
			delay += 0.08f;
		}
			
	}

	IEnumerator TextWithDelay(string dialogue, float delay, int counter) {

		yield return new WaitForSecondsRealtime (delay);

		textObject.GetComponent<Text> ().text = dialogue;

		if (counter == 0) {
			canContinue = true;
		}

	}

	void OnDisable() {

		textBackground.SetActive (false);
		Time.timeScale = 1f;
		//GameObject.Find ("JP").GetComponent<JpControl> ().enabled = true;
		bossDoor.GetComponent<Animator> ().SetTrigger ("close");

		if(Camera.main.GetComponent<AudioSource>()) {
			Camera.main.GetComponent<AudioSource> ().Stop();
			Camera.main.GetComponent<AudioSource> ().volume = 1;
			Camera.main.GetComponent<AudioSource> ().clip = bossMusic;
			Camera.main.GetComponent<AudioSource> ().Play();
		}

	}
}
