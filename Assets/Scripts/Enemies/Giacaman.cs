using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Giacaman : MonoBehaviour {

    [SerializeField] private float jumpForceX = 100f;
    [SerializeField] private float jumpForceY = 100f;
    [SerializeField] private GameObject normalBullet;
    [SerializeField] private GameObject earthBullet;
    [SerializeField] private AudioClip fireShoot;
    [SerializeField] private AudioClip earthFlame;

    private Transform pj;
    private Animator anim;

    private bool isOnTheFloor;
    private Transform groundCheck;
    public LayerMask whatIsGround;
    public Vector2 groundCheckSize;

    private bool lookRight = false;
    private Vector2 bulletPosition;
    private bool isJumping = false;
    [SerializeField] private float jumpCooldown = 0.5f;
    private string lastAttack;
    private int repeatCounter = 0;
    private string[] attacks = new string[5];

    // Use this for initialization
    void Start()
    {

        pj = GameObject.Find("JP").transform;
        anim = GetComponent<Animator>();
        groundCheck = transform.Find("GroundCheck");

        attacks[0] = "Jump";
        attacks[1] = "Jump";
        attacks[2] = "FireShoot";
        attacks[3] = "FireShoot";
        attacks[4] = "EarthAttack";
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //GroundCheck
        isOnTheFloor = Physics2D.OverlapBox(groundCheck.position, groundCheckSize, 0, whatIsGround);

        anim.SetBool("jump", !isOnTheFloor);

        float distance = transform.position.x - pj.transform.position.x;
 
        //Flip
        if (isOnTheFloor && !lookRight && distance < 0)
        {
            Flip();
        }
        if (isOnTheFloor && lookRight && distance > 0)
        {
            Flip();
        }

    }

    void Update()
    {

        //To avoid mega-jumps
        if (GetComponent<Rigidbody2D>().velocity.y > 6)
        {
            //Invoke("ResetVelocity", 0.12f);
        }

        //Interaction
        if (isOnTheFloor && !isJumping)
        {
            int random = 0;

            // Decides the attack. It doesn't let making the same attack more than 2 times
            do
            {
                /*
                random = Random.Range(0, 5);
                int attack = 0;
                if (random == 0 || random == 1) attack = 1;
                if (random == 2 || random == 3) attack = 2;
                if (random == 4) attack = 3;
                if (lastAttack == attack) repeatCounter++;
                else repeatCounter = 0;
                lastAttack = attack;
                */
                random = Random.Range(0, 5);
                if (lastAttack == attacks[random]) repeatCounter++;
                else repeatCounter = 0;
                //Debug.Log("Attack: " + attacks[random] + " LastAttack: " + lastAttack + " Repeat: " + repeatCounter);
                lastAttack = attacks[random];
                
            } while (repeatCounter >= 1);

            if(attacks[random] == "Jump")
            {
                GetComponent<Rigidbody2D>().AddForce(new Vector2(jumpForceX * -transform.localScale.x, jumpForceY));

            } else if(attacks[random] == "FireShoot")
            {
                anim.SetTrigger("shoot");
                //if(!anim.GetCurrentAnimatorStateInfo(0).IsName("Earthattack"))
                    //GetComponent<AudioSource>().PlayOneShot(fireShoot);
            }
            else if (attacks[random] == "EarthAttack")
            {
                anim.SetTrigger("earth");
                GetComponent<AudioSource>().PlayOneShot(earthFlame);
            }

            isJumping = true;
            
            Invoke("ResetJump", jumpCooldown);
        }

        if (isOnTheFloor && !isJumping)
        {
            GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        }

    }

    void Flip()
    {
        var s = transform.localScale;
        s.x *= -1;
        transform.localScale = s;
        lookRight = !lookRight;
    }

    //Called from an animation event
    private void NormalShoot()
    {
        
        bulletPosition = new Vector2(transform.position.x - 0.4f * transform.localScale.x, GetComponent<SpriteRenderer>().bounds.center.y);
        GameObject bullet = Instantiate(normalBullet, bulletPosition, transform.rotation);

        //Adjust the movement of the bullet
        bullet.GetComponent<Bullet>().Speed *= transform.localScale.x;
        
    }

    //Called from an animation event
    private void EarthShoot()
    {

        bulletPosition = new Vector2(transform.position.x - 0.5f * transform.localScale.x, transform.position.y);
        GameObject bullet = Instantiate(earthBullet, bulletPosition, transform.rotation);

        //Adjust the movement of the bullet
        bullet.GetComponent<EarthFlame>().Speed *= transform.localScale.x;
    }

    public void Enrage()
    {
        jumpCooldown = jumpCooldown - 0.2f;
    }

    private void ResetVelocity()
    {
        GetComponent<Rigidbody2D>().velocity = Vector3.zero;
    }

    private void ResetJump()
    {
        isJumping = false;
    }
}
