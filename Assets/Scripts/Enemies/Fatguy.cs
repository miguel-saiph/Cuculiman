using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fatguy : MonoBehaviour {

    [SerializeField] private GameObject firePrefab;
    [SerializeField] private int attackForce;
    private GameObject littleFire;
    private bool isVulnerable = false;
    private bool frenzy = true;

    [SerializeField] private int hp;
    [SerializeField] private float timeOfColor;
    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] private AudioClip damage;
    [SerializeField] private AudioClip noDamage;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
        if (littleFire)
        {
            if (!littleFire.GetComponent<SpriteRenderer>().isVisible)
            {
                littleFire.transform.position = new Vector2(GameObject.Find("JP").transform.position.x, littleFire.transform.position.y);
                littleFire.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                littleFire.GetComponent<Rigidbody2D>().gravityScale = 0.2f;

                if(hp <= 2 && frenzy)
                {
                    Instantiate(firePrefab, new Vector2(littleFire.transform.position.x + 0.5f, littleFire.transform.position.y), Quaternion.identity);
                    Instantiate(firePrefab, new Vector2(littleFire.transform.position.x - 0.5f, littleFire.transform.position.y), Quaternion.identity);
                    frenzy = false;
                }
            }
                
        }
	}

    private void Attack()
    {
        float distance = Mathf.Abs(transform.position.x - GameObject.Find("JP").transform.position.x);

        if (distance <= 2)
        {
            littleFire = Instantiate(firePrefab, transform.position, Quaternion.identity);
            littleFire.GetComponent<Rigidbody2D>().AddForce(Vector2.up * attackForce);
            isVulnerable = true;
        }
        
    }

    private void Invulnerability()
    {
        isVulnerable = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.tag == "Lemon")
        {
            if (isVulnerable)
            {
                Destroy(other.gameObject);
                GetComponent<AudioSource>().clip = damage;
                GetComponent<AudioSource>().Play();
                if (this.GetComponent<ReSkinAnimation>())
                {
                    this.GetComponent<ReSkinAnimation>().enabled = true;
                    Invoke("DisableReSkin", timeOfColor);
                }
                hp -= 1;
                if (hp == 0)
                {
                    Destroy(gameObject);
                    if (littleFire)
                    {
                        Destroy(littleFire);
                    }

                    if (explosionPrefab)
                    {
                        Vector3 enemyPosition = transform.GetChild(0).position;
                        Instantiate(explosionPrefab, enemyPosition, Quaternion.identity);
                    }
                }
            }
            else
            {
                Destroy(other.gameObject);
                GetComponent<AudioSource>().clip = noDamage;
                GetComponent<AudioSource>().Play();
            }
        }
    }

    private void DisableReSkin()
    {

        if (this.GetComponent<ReSkinAnimation>())
        {
            this.GetComponent<ReSkinAnimation>().enabled = false;
        }

    }
}
