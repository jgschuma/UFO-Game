using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_AirCharge : MonoBehaviour
{
    [Tooltip("The player to follow.")]
    public GameObject player;
    [Tooltip("Speed of the player.")]
    public float speed;
    [Tooltip("Distance the enemy can wander away from their home base.")]
    public float wanderRadius;
    [Tooltip("Distance from which the enemy will be alerted to the player")]
    public float alertRadius;
    [Tooltip("Distance enemy comes to before charging")]
    public float stoppingDistance;
    [Tooltip("Time in between shots")]
    public float chargeCooldown;
    [Tooltip("Time spent charging up before actually charging")]
    public float chargeupTime;
    [Tooltip("The distance the enemy will charge")]
    public float chargeDistance;
    public bool isTrackingPlayer;
    public bool isShootingPlayer;
    public Rigidbody2D enemyRigidBody;
    public Transform firePoint;

    private Vector2 homebase;
    private float distanceToPlayer;
    private float distanceFromHome;
    private Vector3 direction;
    private bool allowFire;
    private bool facingRight;
    // Start is called before the first frame update
    void Start()
    {
        enemyRigidBody.freezeRotation = true;
        homebase = transform.position;
        allowFire = true;
        facingRight = true;
        Physics.IgnoreLayerCollision(0, 8);

    }

    private void FixedUpdate() {
        //update distances
        distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
        distanceFromHome = Vector2.Distance(transform.position, homebase);


        // If the player is within alert radius, and inside charge range
        if (distanceToPlayer <= stoppingDistance && distanceToPlayer <= alertRadius){
            //if we face right and player is left, or we face left and player is right
            if(facingRight == true && player.transform.position.x < transform.position.x || facingRight == false && player.transform.position.x > transform.position.x){
                flip();
            }

            //shoot at the player
            isShootingPlayer = true;
            if (allowFire) {
                StartCoroutine(shoot());
            }
        }

        // If the player is within the alert radius, outside charge distance, and within wander radius
        if (distanceToPlayer <= alertRadius && distanceToPlayer > stoppingDistance && distanceFromHome < wanderRadius){
            //if we face right and player is left, or we face left and player is right
            if(facingRight == true && player.transform.position.x < transform.position.x || facingRight == false && player.transform.position.x > transform.position.x){
                flip();
            }
            //go towards player
            isTrackingPlayer = true;
            transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
        }

        // If outside of wander radius or player is outside of alert radius
        if (distanceFromHome >= wanderRadius || distanceToPlayer > alertRadius){
            //go home
            isTrackingPlayer = false;
            isShootingPlayer = false;
            transform.position = Vector2.MoveTowards(this.transform.position, homebase, speed * Time.deltaTime);
        }
    }

    void flip(){
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }

    IEnumerator shoot() {
        allowFire = false;
        direction = player.transform.position - firePoint.transform.position;
        float rotationZ = Mathf.Atan2 (direction.y, direction.x) * Mathf.Rad2Deg;
        firePoint.transform.rotation = Quaternion.Slerp(firePoint.transform.rotation, Quaternion.Euler(0, 0, rotationZ), 100 * Time.deltaTime);
        yield return new WaitForSeconds(chargeCooldown);
        //code for afer cooldown here
        allowFire = true;
    }
}
