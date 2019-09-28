using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour {

    [SerializeField] private float speed;
    [SerializeField] private GameObject bulletPrefab;
    private Rigidbody2D rb;

	// Use this for initialization
	void Start () {

        rb = GetComponent<Rigidbody2D>();

    }
	
	// Update is called once per frame
	void FixedUpdate () {

        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        if (x < 0)
            GetComponent<SpriteRenderer>().flipX = true;
        if (x > 0)
            GetComponent<SpriteRenderer>().flipX = false;

        Vector2 movement = new Vector2(x, y);

        transform.Translate(movement * speed * Time.deltaTime);

        

    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump") || Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        GameObject bullet;
        int orientation = GetComponent<SpriteRenderer>().flipX ? -1 : 1;

        bullet = Instantiate(bulletPrefab, new Vector2(transform.position.x + 0.14f * orientation, transform.position.y), Quaternion.identity);
        
        bullet.GetComponent<ProjectileControl>().speed *= orientation;
    }
}
