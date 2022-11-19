using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Enemy_AirCharge : MonoBehaviour
{
    [Tooltip("The player to follow.")]
    public GameObject player;
    [Tooltip("Speed of the enemy.")]
    public float speed;
    [Tooltip("Distance the enemy can wander away from their home base.")]
    public float wanderRadius;
    [Tooltip("Distance from which the enemy will be alerted to the player")]
    public float alertRadius;
    [Tooltip("Distance to the left and right of homebase the enemy patrols")]
    public float patrolRange = 60f;
    public bool isTrackingPlayer;
    public Rigidbody2D enemyRigidBody;

    Animator anim;
    private bool atHome = true;
    private Vector2 homebase;
    private float distanceToPlayer;
    private float distanceFromHome;
    private Vector3 direction;

    void Awake() {
        player = GameObject.Find("UFO");
    }
    // Start is called before the first frame update
    void Start()
    {
        enemyRigidBody.freezeRotation = true;
        homebase = transform.position;
        anim = GetComponent<Animator>();
        anim.SetBool("faceRight", GetComponent<SpriteRenderer>().flipX);
    }

    private void FixedUpdate() {
        //update distances
        distanceToPlayer = Vector2.Distance(homebase, player.transform.position);
        distanceFromHome = Vector2.Distance(transform.position, homebase);

        //Enemy is dead, float downward
        if(anim.GetInteger("health") == 0)
        {
            gameObject.transform.Find("ContactDamage").gameObject.SetActive(false);
            transform.position -= new Vector3(0, 0.3f, 0);
        }
        //Enemy is not dead
        else if(!anim.GetBool("hurt"))
        {
            //Flip the sprite if the enemy is Agro'd
            if (anim.GetBool("isTargetingPlayer"))
                anim.SetBool("faceRight", (player.transform.position.x > transform.position.x));
            else
            {
                //Normal patrol pattern
                //Return to normal if it's reached home
                if (!atHome && distanceFromHome < 1)
                    atHome = true;
                //Turn around if beyond patrol range
                else if (distanceFromHome > patrolRange)
                    anim.SetBool("faceRight", !anim.GetBool("faceRight"));
                //Patrol left and right
                if(anim.GetBool("faceRight") && atHome)
                    transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
                else if (atHome)
                    transform.position -= new Vector3(speed * Time.deltaTime, 0, 0);
            }

            // If the player is within the alert radius and within wander radius
            if (Vector2.Distance(transform.position, player.transform.position) <= alertRadius && distanceFromHome < wanderRadius && distanceToPlayer < wanderRadius)
            {
                //go towards player
                isTrackingPlayer = true;
                atHome = false;
                anim.SetBool("isTargetingPlayer", true);
                transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
            }
            // If outside of wander radius or player is outside of alert radius
            //else if (distanceFromHome >= wanderRadius || distanceToPlayer > alertRadius)
            else if (!atHome)
            {
                //go home
                isTrackingPlayer = false;
                anim.SetBool("isTargetingPlayer", false);
                anim.SetBool("faceRight", transform.position.x < homebase.x);
                transform.position = Vector2.MoveTowards(transform.position, homebase, Math.Min(speed * Time.deltaTime, distanceFromHome));
            }
        }
    }

    private void OnDrawGizmos()
    {
        //BLUE -- The area the Angler is allowed to wander
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, wanderRadius);
        //GREEN -- The path the Angler will patrol
        Gizmos.color = Color.green;
        Gizmos.DrawLine(new Vector3(transform.position.x - patrolRange - 8, transform.position.y + 8, 0), new Vector3(transform.position.x + patrolRange + 8, transform.position.y + 8, 0));
        Gizmos.DrawLine(new Vector3(transform.position.x - patrolRange - 8, transform.position.y - 8, 0), new Vector3(transform.position.x + patrolRange + 8, transform.position.y - 8, 0));
        Gizmos.DrawLine(new Vector3(transform.position.x - patrolRange - 8, transform.position.y + 8, 0), new Vector3(transform.position.x - patrolRange - 8, transform.position.y - 8, 0));
        Gizmos.DrawLine(new Vector3(transform.position.x + patrolRange + 8, transform.position.y + 8, 0), new Vector3(transform.position.x + patrolRange + 8, transform.position.y - 8, 0));
        //RED -- The alert radius of the Angler
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, alertRadius);
    }
}
