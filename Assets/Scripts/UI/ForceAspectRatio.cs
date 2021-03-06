using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceAspectRatio : MonoBehaviour {

	public float width = 16f;
	public float height = 9f;

	//Set the desired aspect ratio (16:9 for default)

	void Awake() {
		/*
		if (Camera.main.aspect < 1.33) { //Camera aspect = width / height
			width = 4;
			height = 3;
		}*/

	}
	void Start () {

		float targetAspect = width / height;

		//Determine the game window's current aspect by this amount
		float windowAspect = (float)Screen.width / (float)Screen.height;

		//Current viewport height should be scaled by this amount
		float scaleHeight = windowAspect / targetAspect;

		//Obtain camera component so we can modify its viewport
		Camera camera = GetComponent<Camera>();

		//If scaled height is less than current height, add letterbox
		if (scaleHeight < 1.0f) {

			Rect rect = camera.rect;

			rect.width = 1.0f;
			rect.height = scaleHeight;
			rect.x = 0;
			rect.y = (1f - scaleHeight) / 2f;

			camera.rect = rect;
		}
		else //Add pillarbox
		{
			float scaleWidth = 1f / scaleHeight;

			Rect rect = camera.rect;

			rect.width = scaleWidth;
			rect.height = 1f;
			rect.x = (1f - scaleWidth) / 2f;
			rect.y = 0;

			camera.rect = rect;

		}
		
	}

}
