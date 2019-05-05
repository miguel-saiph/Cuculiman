using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlackScreenDisabler : MonoBehaviour {

	public float timeToDisable;

	void OnEnable () {

		Invoke ("Disable", timeToDisable);
		
	}


	void Disable() {

		if (GetComponent<Image> ().enabled) { 
			GetComponent<Image> ().enabled = false;
		}

		GetComponent<BlackScreenDisabler> ().enabled = false;
	}
}
