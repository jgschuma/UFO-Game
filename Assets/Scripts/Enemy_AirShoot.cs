﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Enemy_AirShoot : MonoBehaviour
{
    [Tooltip("The player to follow.")]
    public GameObject player;
    [Tooltip("Speed of the player.")]
    public float speed;
    [Tooltip("Distance the enemy can wander away from their home base.")]
    public float wanderRadius;
    [Tooltip("Distance from which the enemy will be alerted to the player")]
    public float alertRadius;
    [Tooltip("Minimum distance from the player the enemy will keep")]
    public float shootingDistance;
    [Tooltip("Time in between shots")]
    public float rateOfFire;
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
        enemyRigidBody.freezeRotation = true;
        homebase = transform.position;
        allowFire = true;
        anim = GetComponent<Animator>();
        player = GameObject.Find("UFO");
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
                anim.SetBool("faceRight", (player.transform.position.x > transform.position.x));

            // If the player is within alert radius, and inside shooting range
            if (distanceToPlayer <= shootingDistance && distanceToPlayer <= alertRadius)
            {
                //aim at player by setting the firepoint to point at the player

                //shoot at the player
                anim.SetBool("isTargetingPlayer", true);
                anim.SetBool("isShootingPlayer", true);
                if (allowFire)
                {
                    StartCoroutine(shoot());
                }
            }

            // If the player is within the alert radius, outside shooting distance, and within wander radius
            if (distanceToPlayer <= alertRadius && distanceToPlayer > shootingDistance && distanceFromHome < wanderRadius)
            {
                //go towards player
                anim.SetBool("isTargetingPlayer", true);
                //transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
            }

            // If outside of wander radius or player is outside of alert radius
            if (distanceFromHome >= wanderRadius || distanceToPlayer > alertRadius)
            {
                //go home
                anim.SetBool("isTargetingPlayer", false);
                anim.SetBool("isShootingPlayer", false);
                //transform.position = Vector2.MoveTowards(this.transform.position, homebase, speed * Time.deltaTime);
            }
        }
    }

    IEnumerator shoot() {
        allowFire = false;
        yield return new WaitForSeconds(rateOfFire);
        direction = player.transform.position - firePoint.transform.position;
        float rotationZ = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
        firePoint.transform.rotation = Quaternion.Slerp(firePoint.transform.rotation, Quaternion.Euler(0, 0, rotationZ), 100 * Time.deltaTime);
        //shoot
        bulletPrefab.GetComponent<ProjectileDirection>().direction = rotationZ;
        Instantiate(bulletPrefab, firePoint.position, Quaternion.Euler(0,0,0));
        allowFire = true;
    }
}
