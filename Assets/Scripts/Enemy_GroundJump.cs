using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_GroundJump : MonoBehaviour
{
    [Header("Patrollng")]
    public float moveSpeed;
    public Transform groundCheckPoint;
    public Transform wallCheckPoint;
    public float circleRadius;

    public LayerMask groundLayer;
    private bool touchingGround;
    private bool touchingWall;

    [Header("Jump Attack")]
    public float jumpHeight;
    public Transform player;
    public Transform feetPlantCheck;
    public Vector2 boxSize;
    private bool isGrounded;

    [Header("Other")]
    public Rigidbody2D rb;
    private float moveDirection = 1;
    private bool facingRight = true;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        touchingGround = Physics2D.OverlapCircle(groundCheckPoint.position, circleRadius, groundLayer);
        touchingWall = Physics2D.OverlapCircle(wallCheckPoint.position, circleRadius, groundLayer);
        isGrounded = Physics2D.OverlapBox(feetPlantCheck.position, boxSize, 0, groundLayer);
        //Patrolling();
        if(Input.GetKeyDown(KeyCode.Space)){
            JumpAttack();
        }
    }

    void Patrolling(){
        if(!touchingGround || touchingWall){
            flip();
        }
        rb.velocity = new Vector2(moveSpeed*moveDirection, rb.velocity.y);
    }

    void JumpAttack(){
        float distanceFromPlayer = player.position.x - transform.position.x;
        if (isGrounded){
            rb.AddForce(new Vector2(distanceFromPlayer, jumpHeight), ForceMode2D.Impulse);
        }
    }

    void flip(){
        moveDirection *= -1;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(groundCheckPoint.position, circleRadius);
        Gizmos.DrawWireSphere(wallCheckPoint.position, circleRadius);
    }
}
