using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeJumpZone : MonoBehaviour {

    private GameObject player;
    public bool isActive = true;

	// Use this for initialization
	void Start () {
        player = GameObject.Find("JP");
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Invincible")
        {
            if (player.GetComponent<Rigidbody2D>().velocity.y < 0 && isActive)
                Camera.main.GetComponent<Camera2D>().isYLocked = false;
        }
    }
}
