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
    public Transform feetPlantCheck;
    public Vector2 boxSize;
    private bool isGrounded;

    [Header("For Seeing Player")]
    public Transform alertCenter;
    public Vector2 alertZone;
    public LayerMask playerLayer;
    private bool canSeePlayer;

    [Header("For death behavior")]
    public float deathFlyBackwards = 3f;
    public float deathPopUp = 5f;
    public float deathFallAccel = 0.5f;
    public float deathMaxFallSpeed = 10f;
    private float deathFlyBackDirection = 1f;

    [Header("Other")]
    public Rigidbody2D rb;
    private float moveDirection = 1;
    private bool facingRight = true;

    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        allowJump = true;
        Physics2D.IgnoreLayerCollision(0,15, true);
        //Physics2D.IgnoreLayerCollision(0,0, true);
        player = GameObject.Find("UFO").transform;
        anim = GetComponent<Animator>();
        anim.SetBool("faceRight", facingRight);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (anim.GetInteger("health") == 0)
        {
            StopCoroutine(JumpAttack());
            if (anim.GetBool("faceRight"))
                deathFlyBackDirection = -1;
            //Death fall
            deathPopUp = Math.Max(deathPopUp - deathFallAccel, -deathMaxFallSpeed);
            transform.position += new Vector3(deathFlyBackwards * deathFlyBackDirection, deathPopUp);
        }
        else
        {
            touchingGround = Physics2D.OverlapCircle(groundCheckPoint.position, checksCircleRadius, groundLayer);
            touchingWall = Physics2D.OverlapCircle(wallCheckPoint.position, checksCircleRadius, groundLayer);
            isGrounded = Physics2D.OverlapBox(feetPlantCheck.position, boxSize, 0, groundLayer);
            canSeePlayer = Physics2D.OverlapBox(alertCenter.position, alertZone, 0, playerLayer);

            //Frog is on ground
            if (isGrounded)
            {
                anim.SetBool("inAir", false);
                if (!canSeePlayer)
                {
                    anim.SetBool("targetingPlayer", false);
                    Patrolling();
                }
                if (canSeePlayer)
                {
                    if (allowJump)
                    {
                        StartCoroutine(JumpAttack());
                    }
                }
            }
        }
    }

    void Patrolling(){
        if(!touchingGround || touchingWall){
            flip();
        }
        rb.velocity = new Vector2(moveSpeed*moveDirection, rb.velocity.y);
    }

    IEnumerator JumpAttack(){
        allowJump = false;
        float distanceFromPlayer = player.position.x - transform.position.x;
        anim.SetBool("targetingPlayer", true);
        //set the enemy to face the correct position
        if ((distanceFromPlayer < 0 && facingRight) || (distanceFromPlayer > 0 && !facingRight)){
            flip();
        }
        yield return new WaitForSeconds(jumpCooldown);
        //jump!
        if (isGrounded){
            rb.AddForce(new Vector2(distanceFromPlayer, jumpHeight), ForceMode2D.Impulse);
            anim.SetBool("inAir", true);
        }
        allowJump = true;
    }

    void flip(){
        moveDirection *= -1;
        facingRight = !facingRight;
        anim.SetBool("faceRight", !anim.GetBool("faceRight"));
    }

    private void OnDrawGizmos() {
        //BLUE -- For checking if about to patroll into a wall or off a cliff
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(groundCheckPoint.position, checksCircleRadius);
        Gizmos.DrawWireSphere(wallCheckPoint.position, checksCircleRadius);

        //GREEN -- For checking if grounded in order to jump
        Gizmos.color = Color.green;
        Gizmos.DrawCube(feetPlantCheck.position, boxSize);

        //RED -- Alert Area
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(alertCenter.position, alertZone);
    }
}
