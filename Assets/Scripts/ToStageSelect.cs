using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToStageSelect : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {


		if (Input.GetButtonDown("Jump") || Input.GetButtonDown("Fire1")) {

			if (GameManager.gm.currentLevel == GameManager.Levels.Ronquidoman) {
				GlobalOptions.stage2 = true;
			} else if (GameManager.gm.currentLevel == GameManager.Levels.Celesteman)
            {
				GlobalOptions.stage1 = true;
			}
            else if (GameManager.gm.currentLevel == GameManager.Levels.Giacaman)
            {
                GlobalOptions.stage3 = true;
            }

            SceneManager.LoadScene ("Stage Selection");

			
		}
	}
}
