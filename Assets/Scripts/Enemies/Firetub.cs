using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firetub : MonoBehaviour {

    [SerializeField] private GameObject flamePrefab;
    private Animator anim;
    private bool canAttack = true;
    private float attackCooldown = 2;

    private Transform player;



	// Use this for initialization
	void Start () {

        anim = GetComponent<Animator>();
        player = FindObjectOfType<JpControl>().transform;
	}
	
	// Update is called once per frame
	void Update () {

        float distance = Mathf.Abs(transform.position.x - player.position.x);
        Debug.Log(distance);
        if (canAttack && distance <= 2)
        {
            anim.SetTrigger("attack");
            canAttack = false;
            Invoke("AttackCooldown", attackCooldown);
        }
            
	}

    protected void DropFlame()
    {
        Instantiate(flamePrefab, new Vector2(0, 0), Quaternion.identity);
    }

    private void AttackCooldown()
    {
        canAttack = true;
    }
}
