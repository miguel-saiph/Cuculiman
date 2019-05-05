using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DelayedText : MonoBehaviour {

	public string textToWrite;
	public float delay = 0.15f;

	private string newText = "";

	// Use this for initialization
	void Start () {

		Invoke ("WriteText", 1f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	IEnumerator TextWithDelay(string newText, float delay) {

		yield return new WaitForSecondsRealtime (delay);

		GetComponent<Text> ().text = newText;

	}

	private void WriteText() {

		foreach (char word in textToWrite) {
			newText += word.ToString();
			StartCoroutine(TextWithDelay (newText, delay));
			delay += 0.12f;
		}
	}
}
