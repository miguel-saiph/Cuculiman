using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firetub : MonoBehaviour {

    [SerializeField] private float speed;
    [SerializeField] private GameObject flamePrefab;
    [SerializeField] private float triggerDistance;
    [SerializeField] private LayerMask whatIsWall;
    private Animator anim;
    private bool canAttack = true;
    private float attackCooldown = 2;

    private bool facingRight = false;

    private Transform player;



	// Use this for initialization
	void Start () {

        anim = GetComponent<Animator>();
        player = FindObjectOfType<JpControl>().transform;
	}

    private void Awake()
    {
        if (facingRight)
        {
            Flip();
        }
    }

    // Update is called once per frame
    void FixedUpdate () {

        //If there is and invisible wall in front of him, he flips
        if (Physics2D.Raycast(transform.position, Vector2.left * transform.localScale.x, 0.25f, whatIsWall).transform != null)
        {
            Flip();
        }

        transform.Translate(Vector2.right * speed * Time.deltaTime);

        float distance = Mathf.Abs(transform.position.x - player.position.x);

        //if (canAttack && distance <= triggerDistance && player.position.y <= transform.position.y)
        if (canAttack && player.position.y <= transform.position.y)
        {
            anim.SetTrigger("attack");
            canAttack = false;
            Invoke("AttackCooldown", attackCooldown);
        }
            
	}

    protected void DropFlame()
    {
        Instantiate(flamePrefab, new Vector2(transform.position.x, transform.position.y - 0.3f), Quaternion.identity);
    }

    private void AttackCooldown()
    {
        canAttack = true;
    }

    void Flip()
    {

        speed *= -1;
        var s = transform.localScale;
        s.x *= -1;
        transform.localScale = s;
        facingRight = !facingRight;
    }
}
