using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AutoVictory : MonoBehaviour {

    public AudioClip victoryMusic;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Animator>().SetFloat("speed", 0);
            collision.gameObject.GetComponent<JpControl>().enabled = false;
            Camera.main.GetComponent<SpawnFirePillar>().enabled = false;
            VictoryDance();
        }
    }

    void VictoryDance()
    {

        if (GameObject.Find("JP"))
        {
            GameObject.Find("JP").GetComponent<Animator>().SetTrigger("victory");
            GameObject.Find("JP").GetComponent<Animator>().Play("JpRock");
            Camera.main.GetComponent<AudioSource>().Stop();
            Camera.main.GetComponent<AudioSource>().clip = victoryMusic;
            Camera.main.GetComponent<AudioSource>().Play();
            

            Invoke("EnableStageSelect", 3f);
        }
    }

    void EnableStageSelect()
    {
        GetComponent<ToStageSelect>().enabled = true;
    }
}
