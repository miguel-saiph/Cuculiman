using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderMovement : MonoBehaviour {

    private Rigidbody2D rb;
    private Animator anim;
    private Transform groundCheck;
    private float inputVertical;
    [SerializeField] private float climbSpeed;
    [SerializeField] private float distance;
    [SerializeField] private LayerMask whatIsLadder;
    private bool isClimbing;
    private Collider2D endLadder;
    private GameObject player;
    
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        groundCheck = transform.Find("GroundCheck");
        player = GameObject.Find("JP");
    }
	
	void FixedUpdate () {

        //var isOnTheFloor = Physics2D.OverlapBox(groundCheck.position, new Vector2(0.2f, 0.08f), 0, LayerMask.GetMask("Environment"));

        RaycastHit2D hitInfoUp = Physics2D.Raycast(groundCheck.position, Vector2.up, distance, whatIsLadder);
        RaycastHit2D hitInfoDown = Physics2D.Raycast(groundCheck.position, Vector2.down, distance, whatIsLadder);


        if (hitInfoUp.collider != null)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetAxisRaw("Vertical") > 0)
            {
                isClimbing = true;
            }
            
        } else if(hitInfoDown.collider != null)
        {
            if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetAxisRaw("Vertical") < 0)
            {
                isClimbing = true;
            }
        }
        else
        {
            isClimbing = false;
            if (Input.GetAxis("Horizontal") > 0.1f)
            {
                isClimbing = false;
            }
        }

        RaycastHit2D hitInfo2 = Physics2D.Raycast(groundCheck.position, Vector2.down, 0.5f, LayerMask.GetMask("Environment"));

        if (hitInfo2.collider != null)
        {
            endLadder = hitInfo2.collider;
            if (hitInfo2.collider.gameObject.name == "End")
            {
                //isClimbing = false;
                if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetAxisRaw("Vertical") < 0)
                {
                    Physics2D.IgnoreCollision(GetComponent<Collider2D>(), hitInfo2.collider, true);
                    isClimbing = true;
                    rb.gravityScale = 0;
                }
                
            }

        }
        
        if (isClimbing == true && (hitInfoUp.collider != null || hitInfoDown.collider != null))
        {
            inputVertical = Input.GetAxisRaw("Vertical");
            anim.SetFloat("climbingSpeed", Mathf.Abs(inputVertical));
            rb.velocity = new Vector2(rb.velocity.x, inputVertical * climbSpeed);
            rb.gravityScale = 0; 
        } else {
            rb.gravityScale = 1;

        }

        if (isClimbing && Mathf.Abs(Input.GetAxisRaw("Vertical")) < 0.1f)
        {
            anim.speed = 0;
        }
        else
        {
            anim.speed = 1;
        }

        // Abort climbing with jump button
        if (isClimbing && Input.GetButtonDown("Jump"))
        {
            isClimbing = false;
        }

        //if (isClimbing) Camera.main.GetComponent<Camera2D>().isYLocked = false;
        //else Camera.main.GetComponent<Camera2D>().isYLocked = true;

        anim.SetBool("isClimbing", isClimbing);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "RestoreCollision" && endLadder != null)
        {
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), endLadder, false);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        // To allow camera movement y while in the ladder
        //if (!GameObject.Find("StageManager").GetComponent<FiremanStage>().staticCamera)
        if (!GameManager.gm.staticCamera)
        {
            if (player.GetComponent<Rigidbody2D>().velocity.y == 2 || player.GetComponent<Rigidbody2D>().velocity.y == -2)
                Camera.main.GetComponent<Camera2D>().isYLocked = false;
        }
        
        
    }
}
