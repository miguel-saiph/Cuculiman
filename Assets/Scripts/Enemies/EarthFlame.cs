using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthFlame : MonoBehaviour {

    [SerializeField] private float speed;

    private enum Orientation
    {
        Left, Right
    }

    [Tooltip("Should it travel left or right?")]
    [SerializeField] private Orientation defaultOrientation;

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
        
	}
	
	// Update is called once per frame
	void Update () {
        
		
	}

    private void Move()
    {
        int modifier = defaultOrientation == Orientation.Left ? -1 : 1; 
        transform.position = new Vector2(transform.position.x + speed * modifier, transform.position.y);
    }
}
