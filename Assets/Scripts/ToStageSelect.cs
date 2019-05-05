using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToStageSelect : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {


		if (Input.GetButtonDown("Jump") || Input.GetButtonDown("Fire1")) {

			if (GameManager.gm.ronquidomanStage) {
				GlobalOptions.stage2 = true;
			} else {
				GlobalOptions.stage1 = true;
			}
				
			SceneManager.LoadScene ("Stage Selection");

			
		}
	}
}
