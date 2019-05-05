using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeText : MonoBehaviour {

	public float fadeTime = 0.5f;

	public float secondsBetweenText = 3f;
	private float aux;

	private float timeToChange = 0;
	private int x = 4;


	// Use this for initialization
	void Start () {

		aux = secondsBetweenText;
		FadeIn ();
		
	}
	
	// Update is called once per frame
	void Update () {

		//Color.Lerp (GetComponent<Text> ().color, Color.clear, fadeOutTime * Time.deltaTime);
		timeToChange = Time.time;

		if (timeToChange >= secondsBetweenText) {
			Invoke ("FadeOut", 1f);
			Invoke("ChangeText", 1.5f);
			Invoke ("FadeIn", 1.5f);
			secondsBetweenText += aux;
		}

		if (x == 0) {
			if (IntroManager.im) {
				IntroManager.im.StartCamera ();
			}
			Destroy (gameObject);
		}


	}

	void ChangeText() {

		x -= 1;

		switch (x) {
		case 3:
			{
				GetComponent<Text> ().text = "FINISHED ALL MEGAMAN \n GAMES IN A MEGA MARATHON.";
				return;
			}
		case 2:
			{
				GetComponent<Text> ().text = "DR. WILLY, TIRED OF BEING DEFEATED\n SO MANY TIMES, DECIDED TO TAKE REVENGE";
				return;
			}

		case 1:
			{
				GetComponent<Text> ().text = "CONFINING JP IN HIS OWN GAME.";
				return;

			}
		}


	}

	void FadeOut() {
		
		GetComponent<Text>().CrossFadeAlpha(0.0f, fadeTime, false);
	}

	void FadeIn() {

		GetComponent<Text>().CrossFadeAlpha(1.0f, fadeTime, false);
	}


}
