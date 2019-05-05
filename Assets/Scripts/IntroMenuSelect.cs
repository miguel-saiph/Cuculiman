using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class IntroMenuSelect : MonoBehaviour, ISelectHandler {

	public GameObject selectArrow;

	// Use this for initialization
	void Start () {
		
	}
	
	public void OnSelect(BaseEventData eventData) {

		selectArrow.GetComponent<RectTransform>().position = new Vector3(transform.position.x - 0.13f * Screen.width, transform.position.y, selectArrow.transform.position.x);

	}
}
