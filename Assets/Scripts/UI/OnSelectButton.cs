using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class OnSelectButton : MonoBehaviour, ISelectHandler {

	public GameObject jpFace;
	public Sprite faceLeft;
	public Sprite faceRight;
	public Sprite faceCentral;

	public void OnSelect(BaseEventData eventData) {

		//Audio
		//This conditiong is to avoid the first sound
		if (GetComponent<AudioSource> ().loop) {
			GetComponent<AudioSource> ().loop = false;
		} else {
			GetComponent<AudioSource> ().Play ();
		}

		//Image
		if (gameObject.name == "Border 1") {
			jpFace.GetComponent<Image> ().sprite = faceLeft;
		} else if (gameObject.name == "Border 2") {
			jpFace.GetComponent<Image> ().sprite = faceRight;
		} else {
			jpFace.GetComponent<Image> ().sprite = faceCentral;
		}
	}
}
