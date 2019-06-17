using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SelectionManager : MonoBehaviour {

	public Button jPFace;
	public Button stage1;
	public Button stage2;
    public Button stage3;
    public Canvas selectionScreen;
	public Canvas presentationCanvas;
	public Sprite completedImage;

	private bool stage1Completed;
	private bool stage2Completed;
    private bool stage3Completed;

    private int _stage;

	// Use this for initialization
	void Start () {

		Cursor.visible = false;
		jPFace.Select (); //IMPORTANT to automatic set key inputs
        stage2.GetComponent<Animator>().SetTrigger("Normal") ;

        //GlobalOptions.lives = 3;
        stage1Completed = GlobalOptions.stage1;
		stage2Completed = GlobalOptions.stage2;
        stage3Completed = GlobalOptions.stage3;

        if (stage1Completed) {
			GameObject.Find ("Celeste Image").GetComponent<Image> ().sprite = completedImage;
			GameObject.Find ("Celeste Image").GetComponent<Image> ().SetNativeSize();
		}

		if (stage2Completed) {
			GameObject.Find ("Ronquido Image").GetComponent<Image> ().sprite = completedImage;
			GameObject.Find ("Ronquido Image").GetComponent<Image> ().SetNativeSize();
		}

        if (stage3Completed)
        {
            GameObject.Find("Giaca image").GetComponent<Image>().sprite = completedImage;
            GameObject.Find("Giaca image").GetComponent<Image>().SetNativeSize();
        }

        if (stage1Completed && stage2Completed && stage3Completed) {
			SceneManager.LoadScene ("Ending");
		}
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void GoToStage(int stage) {

        _stage = stage;

		selectionScreen.GetComponent<AudioSource> ().Stop ();
		GetComponent<AudioSource> ().Play ();
        StartCoroutine(ToStage());

		Invoke ("ActivatePresentation", 1.5f);

        if (_stage == 1)
		    stage1.GetComponent<Animator> ().enabled = false;
        if (_stage == 2)
            stage2.GetComponent<Animator>().enabled = false;
        if (_stage == 3)
            stage3.GetComponent<Animator>().enabled = false;
    }

	private void ActivatePresentation() {
		selectionScreen.gameObject.SetActive (false);
		presentationCanvas.gameObject.SetActive (true);
		Camera.main.GetComponent<Camera>().backgroundColor = Color.black;
		presentationCanvas.gameObject.transform.GetChild (_stage).gameObject.SetActive(true);

	}

    IEnumerator ToStage()
    {
        AudioSource audio = presentationCanvas.GetComponent<AudioSource>();
        audio.Play();
        Debug.Log(audio.clip.length);
        yield return new WaitForSeconds(audio.clip.length);
        
        if (_stage == 1)
            SceneManager.LoadScene("CelesteManStage");
        if (_stage == 2)
            SceneManager.LoadScene("RonquidoMan Stage");
        if (_stage == 3)
            SceneManager.LoadScene("FireMan Stage");
    }
		
}
