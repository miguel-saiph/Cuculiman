using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script para cambiar los sprites de una animación en tiempo de ejecución
//Los spritesheets deben estar en una carpeta llamada Resources
//Los nuevos sprites deben tener el mismo nombre del sprite original
//El spritesheet puede tener otro nombre (cambiárselo después de hacer el slice)

public class ReSkinAnimation : MonoBehaviour {

	public string spriteSheetName;

	// Use this for initialization
	void Start () {
		
	}


	// Update is called once per frame
	void LateUpdate () {

		//var subSprites = Resources.LoadAll<Sprite> ("Enemies/" + spriteSheetName);

		foreach (var renderer in GetComponentsInChildren<SpriteRenderer>()) {

			string spriteName = renderer.sprite.name; //Nombre del sprite original
			var newSprite = Resources.LoadAll<Sprite> ("Enemies/" + spriteSheetName); //Array con todos los sprites del spritesheet

			for (int i = 0; i < newSprite.Length; i++) {
				if (newSprite [i].name == spriteName) {
					renderer.sprite = newSprite [i];
					break;
				}
				
			}

		}
		
	}
		
}
