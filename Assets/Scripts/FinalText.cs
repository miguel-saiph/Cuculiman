using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FinalText : MonoBehaviour {

	// Use this for initialization
	void Start () {

		if (GlobalOptions.language == GlobalOptions.Language.English) {

			GetComponent<Text> ().text = "THANK YOU FOR PLAYING";
		} else {
			GetComponent<Text> ().text = "GRACIAS POR JUGAR";
		}
		
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetButtonDown("Submit")) {

			Application.Quit ();
		}
		
	}
}
