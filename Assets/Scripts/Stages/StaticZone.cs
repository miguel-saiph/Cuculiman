using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class StaticZone : MonoBehaviour {
    
    private enum Entrance
    {
        Left, Right
    }
    [SerializeField] private Entrance entrance;
    [SerializeField] private bool spawnHazzard = true;
    private bool staticCamera = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Invincible")
        {
            if (true)
            {
                GameManager.gm.staticCamera = true;
                if (GameManager.gm.currentLevel == GameManager.Levels.Giacaman && spawnHazzard)
                    Camera.main.GetComponent<SpawnFirePillar>().enabled = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Invincible")
        {
            //Collider2D[] list = new Collider2D[2];
            //collision.OverlapCollider(new ContactFilter2D(), list);
            if (entrance == Entrance.Left)
            {
                // Only deactivates static camera if player exits from the left
                if (collision.gameObject.transform.position.x <= gameObject.GetComponent<BoxCollider2D>().bounds.center.x)
                {
                    GameManager.gm.staticCamera = false;
                    if (GameManager.gm.currentLevel == GameManager.Levels.Giacaman && spawnHazzard)
                        Camera.main.GetComponent<SpawnFirePillar>().enabled = false;
                }
            } else
            {
                if (collision.gameObject.transform.position.x >= gameObject.GetComponent<BoxCollider2D>().bounds.center.x)
                {
                    GameManager.gm.staticCamera = false;
                    if (GameManager.gm.currentLevel == GameManager.Levels.Giacaman && spawnHazzard)
                    Camera.main.GetComponent<SpawnFirePillar>().enabled = false;
                }

            }
                
                
                
        }
    }
}
