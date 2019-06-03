using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class FiremanStage : MonoBehaviour {

    public bool staticCamera = false;

	// Use this for initialization
	void Start () {
        PlayerPrefs.SetString("CurrentLevel", "Giacaman");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            staticCamera = true;
            Camera.main.GetComponent<SpawnFirePillar>().enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {

            //Collider2D[] list = new Collider2D[2];
            //collision.OverlapCollider(new ContactFilter2D(), list);

            // Only deactivates static camera if player exits from the left
            if (collision.gameObject.transform.position.x <= gameObject.GetComponent<BoxCollider2D>().bounds.center.x)
            {
                staticCamera = false;
                Camera.main.GetComponent<SpawnFirePillar>().enabled = true;
            }
                
        }
    }
}
