using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public Rigidbody2D enemyRigidBody;
    public Transform firePoint;
    public GameObject bulletPrefab;


    private Vector2 homebase;
    private float distanceToPlayer;
    private float distanceFromHome;
    private Vector2 direction;
    private bool allowFire;
    private bool facingRight;
    // Start is called before the first frame update
    void Start()
    {
        enemyRigidBody.freezeRotation = true;
        homebase = transform.position;
        allowFire = true;
        facingRight = true;
    }

    private void FixedUpdate() {
        //update distances
        distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
        distanceFromHome = Vector2.Distance(transform.position, homebase);

        // If the player is within alert radius, and inside shooting range
        if (distanceToPlayer <= shootingDistance && distanceToPlayer <= alertRadius){
            //if we face right and player is left, or we face left and player is right
            if(facingRight == true && player.transform.position.x < transform.position.x || facingRight == false && player.transform.position.x > transform.position.x){
                flip();
            }
            //aim at player by setting the firepoint to point at the player

            //shoot at the player
            if (allowFire) {
                StartCoroutine(shoot());
            }
        }

        // If the player is within the alert radius, outside shooting distance, and within wander radius
        if (distanceToPlayer <= alertRadius && distanceToPlayer > shootingDistance && distanceFromHome < wanderRadius){
            //if we face right and player is left, or we face left and player is right
            if(facingRight == true && player.transform.position.x < transform.position.x || facingRight == false && player.transform.position.x > transform.position.x){
                flip();
            }
            //go towards player
            transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
        }

        // If outside of wander radius or player is outside of alert radius
        if (distanceFromHome >= wanderRadius || distanceToPlayer > alertRadius){
            //go home
            transform.position = Vector2.MoveTowards(this.transform.position, homebase, speed * Time.deltaTime);
        }
    }

    void flip(){
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }

    IEnumerator shoot() {
        allowFire = false;
        yield return new WaitForSeconds(rateOfFire);
        GameObject newBullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        //newBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(bulletSpeed * Time.fixedDeltaTime, 0f);

        allowFire = true;
    }
}
