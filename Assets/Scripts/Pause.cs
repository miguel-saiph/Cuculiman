using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour {

	public GameObject pauseScreenCanvas;

	private bool paused = false;

	// Use this for initialization
	void Start () {

		//Turn on audio listener if it was paused before
		if (AudioListener.pause) {
			AudioListener.pause = false;
		}

		if (Time.timeScale < 1f) {
			Time.timeScale = 1f;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
		if (Input.GetButtonDown("Pause")) {
			PauseGame ();
		}

		if (paused) {
			if (Input.GetKeyDown (KeyCode.Escape) || Input.GetKeyDown(KeyCode.Joystick1Button6)) {
				Application.Quit ();
			}
		}
	
	}

	public void PauseGame() {

		AudioListener.pause = !AudioListener.pause;

		if (paused) {

			Time.timeScale = 1f;
			pauseScreenCanvas.SetActive(false);
			paused = false;
	
		} else {

			Time.timeScale = 0f;
			pauseScreenCanvas.SetActive(true);
			paused = true;
		}
	}
}
