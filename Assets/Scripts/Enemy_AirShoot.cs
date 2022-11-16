using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Enemy_AirShoot : MonoBehaviour
{
    [Tooltip("The player to follow.")]
    public GameObject player;
/*    [Tooltip("Speed of the player.")]
    public float speed;*/
/*    [Tooltip("Distance the enemy can wander away from their home base.")]
    public float wanderRadius;
    [Tooltip("Distance from which the enemy will be alerted to the player")]
    public float alertRadius;*/
    [Tooltip("Minimum distance from the player the enemy will keep")]
    public float shootingDistance;
    [Tooltip("Time to take a shot")]
    public float timeToFire;
    [Tooltip("Time between shooting and being ready to shoot again")]
    public float timeToArmShot;
    public bool isTrackingPlayer;
    public bool isShootingPlayer;
    public Rigidbody2D enemyRigidBody;
    public Transform firePoint;
    public GameObject bulletPrefab;
    public Animator anim;
    public float deathFlyBackwards = 3f;
    public float deathPopUp = 5f;
    public float deathFallAccel = 0.5f;
    public float deathMaxFallSpeed = 10f;
    
    private float deathFlyBackDirection = 1f;
    private Vector2 homebase;
    private float distanceToPlayer;
    private float distanceFromHome;
    public Vector3 direction;
    private bool allowFire;
    // Start is called before the first frame update
    void Start()
    {
        homebase = transform.position;
        allowFire = false;
        anim = GetComponent<Animator>();
        player = GameObject.Find("UFO");
        anim.SetBool("faceRight", GetComponent<SpriteRenderer>().flipX);
    }

    private void FixedUpdate() {
        //update distances
        distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
        //distanceFromHome = Vector2.Distance(transform.position, homebase);

        if (anim.GetInteger("health") == 0)
        {
            if (anim.GetBool("faceRight"))
                deathFlyBackDirection = -1;
            //Death fall
            deathPopUp = Math.Max(deathPopUp-deathFallAccel, -deathMaxFallSpeed);
            transform.position += new Vector3(deathFlyBackwards*deathFlyBackDirection, deathPopUp);
        }
        else
        {
            //Flip the sprite if the enemy is Agro'd
            if (anim.GetBool("isTargetingPlayer"))
            {
                anim.SetBool("faceRight", (player.transform.position.x > transform.position.x));

                // If the player is within alert radius, and inside shooting range
                if (distanceToPlayer <= shootingDistance && allowFire)
                {
                    //shoot at the player
                    StartCoroutine(shoot());
                }
            }
            else if (distanceToPlayer <= shootingDistance)
            {
                anim.SetBool("isTargetingPlayer", true);
                StartCoroutine(waitForNextShot());
            }
            // If outside of alert radius
            else
            {
                //go home
                anim.SetBool("isTargetingPlayer", false);
            }
        }
    }

    IEnumerator shoot() {
        anim.SetBool("isShootingPlayer", true);
        allowFire = false;
        yield return new WaitForSeconds(timeToFire);
        //Cancel coroutine if enemy is already dead
        if (anim.GetInteger("health") > 0 && !anim.GetBool("hurt"))
        {
            //shoot
            direction = player.transform.position - transform.position;
            float rotationZ = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
            //firePoint.transform.rotation = Quaternion.Slerp(firePoint.transform.rotation, Quaternion.Euler(0, 0, rotationZ), 100 * Time.deltaTime);
            bulletPrefab.GetComponent<ProjectileDirection>().direction = rotationZ;
            Instantiate(bulletPrefab, firePoint.position, Quaternion.Euler(0, 0, 0));
            StartCoroutine(waitForNextShot());
        }
        anim.SetBool("isShootingPlayer", false);
    }

    IEnumerator waitForNextShot()
    {
        yield return new WaitForSeconds(timeToArmShot);
        if (anim.GetBool("isTargetingPlayer"))
            allowFire = true;
    }

    private void OnDrawGizmos()
    {
        //RED -- Alert Area
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, shootingDistance);
    }
}
