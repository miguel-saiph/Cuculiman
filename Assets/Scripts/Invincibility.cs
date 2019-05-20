using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Clase que se activa al ser golpeado

public class Invincibility : MonoBehaviour {

	public float duration = 3f;

	private SpriteRenderer spriteRenderer;
	private string originalTag;

	private float realTime;

    [SerializeField] private float transparencyTime = 0.01f;
    private bool flag = true;
    
	void Awake () {

		spriteRenderer = this.GetComponent<SpriteRenderer> ();
		originalTag = gameObject.tag;
	}

	void OnEnable() {

		//Cambia el tag para que los enemigos no le hagan daño
		gameObject.tag = "Invincible";
		realTime = Time.time + duration;

	}
	
	void Update () {

		//Si no se ha cumplido el tiempo, activa y desactiva la transparencia
		if (Time.time <= realTime) {
            
            /*
			if (spriteRenderer.color == Color.white) {
				spriteRenderer.color = new Color(1,1,1,0.5f);
			} else {
                spriteRenderer.color = Color.white;
			}
            */

            if(flag)
            {
                flag = false;
                StartCoroutine(Transparency());
            }
            
            
        } else {
            //Para asegurarme de que siempre terminará con los parámetros originales
            spriteRenderer.color = Color.white;
            gameObject.tag = originalTag;
            gameObject.GetComponent<Invincibility>().enabled = false;

        }
		
	}

    IEnumerator Transparency()
    {
        if (spriteRenderer.color == Color.white)
        {
            spriteRenderer.color = new Color(1, 1, 1, 0.5f);
        }
        else
        {
            spriteRenderer.color = Color.white;
        }

        yield return new WaitForSeconds(transparencyTime);

        flag = true;
    }
}
