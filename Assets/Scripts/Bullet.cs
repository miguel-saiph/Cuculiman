using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    [Tooltip("Movement speed")]
    [SerializeField] private float speed;


    private enum DestroyMode
    {
        OffCamera, Time, OnImpact
    }

    [Tooltip("When should it be destroyed")]
    [SerializeField] private DestroyMode destroyMode;

    public float Speed
    {
        get
        {
            return speed;
        }

        set
        {
            speed = value;
        }
    }

    // Use this for initialization
    void Start () {

        if (speed < 0)
            Flip();
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
	}

    private void OnBecameInvisible()
    {
        if (destroyMode == DestroyMode.OffCamera)
        {
            Destroy(gameObject);
        }
    }

    void Flip()
    {
        var s = transform.localScale;
        s.x *= -1;
        transform.localScale = s;
    }
}
