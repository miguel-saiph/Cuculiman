using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class IntroManager : MonoBehaviour {

	public static IntroManager im;

	public Camera mainCamera;

	public Canvas textCanvas;
	public Canvas menuCanvas;
	public GameObject playButton;
	public GameObject exitButton;

	void Start () {

		GlobalOptions.language = GlobalOptions.Language.English;
		GlobalOptions.lives = 3;

		//Importante para poder usar las funciones desde afuera
		if (im == null) {
			im = this.gameObject.GetComponent<IntroManager> ();
		}

		Cursor.visible = false;
		menuCanvas.transform.GetChild (0).GetComponent<Button>().Select();

	}
	
	// Update is called once per frame
	void Update () {

		if (textCanvas.isActiveAndEnabled) {
			if (Input.GetButtonDown("Jump") || Input.GetButtonDown("Submit")) {
				mainCamera.transform.position = new Vector3 (-4.16f, 9.663568f, -10f);
				textCanvas.gameObject.SetActive(false);
				ShowMenu ();
			}
		}

	}

	//Just for intro
	public void StartCamera() {

		mainCamera.GetComponent<IntroMovement> ().enabled = true;
		textCanvas.gameObject.SetActive(false);

	}

	//Just for intro
	public void ShowMenu() {

		GetComponent<AudioSource> ().enabled = false;
		menuCanvas.gameObject.SetActive(true);
		menuCanvas.transform.GetChild (0).GetComponent<Button>().Select();

	}

	public void StartGame() {

		SceneManager.LoadScene ("Stage Selection");
	}

	public void ExitGame() {
		Application.Quit ();
	}

	public void ToSpanish() {

		playButton.GetComponent<Text> ().text = "JUGAR";
		exitButton.GetComponent<Text> ().text = "SALIR";
		GlobalOptions.language = GlobalOptions.Language.Español;
	}

	public void ToEnglish() {

		playButton.GetComponent<Text> ().text = "PLAY";
		exitButton.GetComponent<Text> ().text = "EXIT";
		GlobalOptions.language = GlobalOptions.Language.English;
	}

}
