using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SelectionManager : MonoBehaviour {

	public Button jPFace;
	public Button stage1;
	public Button stage2;
	public Canvas selectionScreen;
	public Canvas presentationCanvas;
	public Sprite completedImage;

	private bool stage1Completed;
	private bool stage2Completed;

	// Use this for initialization
	void Start () {

		Cursor.visible = false;
		jPFace.Select (); //IMPORTANT to automatic set key inputs

		//GlobalOptions.lives = 3;
		stage1Completed = GlobalOptions.stage1;
		stage2Completed = GlobalOptions.stage2;

		if (stage1Completed) {
			GameObject.Find ("Celeste Image").GetComponent<Image> ().sprite = completedImage;
			GameObject.Find ("Celeste Image").GetComponent<Image> ().SetNativeSize();
		}

		if (stage2Completed) {
			GameObject.Find ("Ronquido Image").GetComponent<Image> ().sprite = completedImage;
			GameObject.Find ("Ronquido Image").GetComponent<Image> ().SetNativeSize();
		}

		if (stage1Completed && stage2Completed) {
			SceneManager.LoadScene ("Ending");
		}
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void ToLevel1() {

		selectionScreen.GetComponent<AudioSource> ().Stop ();
		GetComponent<AudioSource> ().Play ();

		Invoke ("ActivePresentation1", 1.5f);
		stage1.GetComponent<Animator> ().enabled = false;
		//SceneManager.LoadScene ("CelesteManStage");
	}

	public void ToLevel2() {
		selectionScreen.GetComponent<AudioSource> ().Stop ();
		GetComponent<AudioSource> ().Play ();
		//SceneManager.LoadScene ("RonquidoMan Stage");
		Invoke ("ActivePresentation2", 1.5f);
		stage2.GetComponent<Animator> ().enabled = false;
	}

	private void ActivePresentation1() {
		selectionScreen.gameObject.SetActive (false);
		presentationCanvas.gameObject.SetActive (true);
		Camera.main.GetComponent<Camera>().backgroundColor = Color.black;
		presentationCanvas.gameObject.transform.GetChild (1).gameObject.SetActive(true);
		Invoke ("ActiveLevel1", 3f);
	}

	private void ActiveLevel1() {
		SceneManager.LoadScene ("CelesteManStage");
	}

	private void ActivePresentation2() {
		selectionScreen.gameObject.SetActive (false);
		presentationCanvas.gameObject.SetActive (true);
		Camera.main.GetComponent<Camera>().backgroundColor = Color.black;
		presentationCanvas.gameObject.transform.GetChild (2).gameObject.SetActive(true);
		Invoke ("ActiveLevel2", 3f);
	}

	private void ActiveLevel2() {
		SceneManager.LoadScene ("RonquidoMan Stage");
	}
		
}
