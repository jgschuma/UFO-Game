using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Enemy_GroundJump : MonoBehaviour
{
    [Header("Patrollng")]
    public float moveSpeed;
    public Transform groundCheckPoint;
    public Transform wallCheckPoint;
    public float checksCircleRadius;

    public LayerMask groundLayer;
    private bool touchingGround;
    private bool touchingWall;

    [Header("Jump Attack")]
    public float jumpHeight;
    public float jumpCooldown;
    private bool allowJump;
    public Transform player;
    private bool isGrounded;

    [Header("For Seeing Player")]
    public Transform alertCenter;
    public Vector2 alertZone;
    public LayerMask playerLayer;
    private bool canSeePlayer;

    [Header("For death behavior")]
    public float hurtKnockback = 3f;
    public float hurtPopUp= 5f;
    public float deathFallAccel = 0.5f;
    public float deathMaxFallSpeed = 10f;
    private float deathFlyBackDirection = 1f;

    [Header("Other")]
    public Rigidbody2D rb;
    private float moveDirection = 1;
    //private bool facingRight = true;

    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        allowJump = true;
        Physics2D.IgnoreLayerCollision(0,15, true);
        //Physics2D.IgnoreLayerCollision(0,0, true);
        player = GameObject.Find("UFO").transform;
        anim = GetComponent<Animator>();
        anim.SetBool("faceRight", GetComponent<SpriteRenderer>().flipX);
        if (!anim.GetBool("faceRight"))
            moveDirection = -1;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (anim.GetInteger("health") == 0)
        {
            StopCoroutine(JumpAttack());
            //Doesn't make enemy fly in right direction 100% of the time, needs fixing
            if (anim.GetBool("faceRight"))
                deathFlyBackDirection = -1;
            //Death fall
            hurtPopUp = Math.Max(hurtPopUp - deathFallAccel, -deathMaxFallSpeed);
            transform.position += new Vector3(hurtKnockback * deathFlyBackDirection, hurtPopUp);
        }
        else if (!anim.GetBool("hurt"))
        {
            touchingGround = Physics2D.OverlapCircle(groundCheckPoint.position, checksCircleRadius, groundLayer);
            touchingWall = Physics2D.OverlapCircle(wallCheckPoint.position, checksCircleRadius, groundLayer);
            if (!canSeePlayer)
            {
                canSeePlayer = Physics2D.OverlapBox(alertCenter.position, alertZone, 0, playerLayer);
            }

            //Frog is on ground
            if (isGrounded)
            {
                anim.SetBool("inAir", false);
                //Frog can't see player --> it's patrolling
                if (!canSeePlayer)
                {
                    anim.SetBool("targetingPlayer", false);
                    Patrolling();
                }
                //Frog can see player and can jump --> start JumpAttackCoroutine
                else if (allowJump)
                {
                    StartCoroutine(JumpAttack());
                }
                //Frog can see player but can't jump --> it's prepping to jump, make sure it keeps facing the right way
                else if((player.position.x-transform.position.x) * moveDirection < 0)
                {
                    flip();
                }
            }
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        Vector2 contact = other.GetContact(0).normal;
        if (other.gameObject.tag == "Terrain")
        {
            Debug.Log("Frog contact: " + contact);
            //Frog has just touched the ground
            if (contact.y > 0)
            {
                isGrounded = true;
                //Try to target player again
                canSeePlayer = Physics2D.OverlapBox(alertCenter.position, alertZone, 0, playerLayer);
            }
        }
/*        else if (other.gameObject.name == "ShieldPower")
        {
            Debug.Log("It works?");
            rb.velocity = (new Vector2(hurtKnockback * -Math.Sign(contact.x), hurtPopUp));
        }*/
    }

/*    void OnTriggerEnter2D(Collision2D other)
    {
*//*        Vector2 contact = other.GetContact(0).normal;
        if (other.gameObject.name == "ShieldPower")
        {
            rb.velocity = (new Vector2(hurtKnockback * Math.Sign(contact.x), hurtPopUp));
        }*//*
    }*/

    void Patrolling(){
        if(!Physics2D.OverlapCircle(new Vector2 (groundCheckPoint.transform.position.x, groundCheckPoint.transform.position.y), checksCircleRadius, groundLayer) || touchingWall){
            flip();
        }
        rb.velocity = new Vector2(moveSpeed*moveDirection, rb.velocity.y);
    }

    IEnumerator JumpAttack(){
        allowJump = false;
        anim.SetBool("targetingPlayer", true);
        yield return new WaitForSeconds(jumpCooldown);
        //jump!
        float distanceFromPlayer = player.position.x - transform.position.x;
        if (isGrounded){
            rb.AddForce(new Vector2(distanceFromPlayer, jumpHeight), ForceMode2D.Impulse);
            anim.SetBool("inAir", true);
            isGrounded = false;
        }
        allowJump = true;
    }

    void flip(){
        moveDirection *= -1;
        //facingRight = !facingRight;
        anim.SetBool("faceRight", !anim.GetBool("faceRight"));
    }

    private void OnDrawGizmos() {
        //BLUE -- For checking if about to patroll into a wall or off a cliff
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(groundCheckPoint.position, checksCircleRadius);
        Gizmos.DrawWireSphere(wallCheckPoint.position, checksCircleRadius);

        //RED -- Alert Area
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(alertCenter.position, alertZone);
    }
}
