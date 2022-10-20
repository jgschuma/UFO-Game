using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_GroundStationaryShoot: MonoBehaviour
{
    [Tooltip("The player to follow.")]
    public GameObject player;
    [Tooltip("Speed of the player.")]
    public float speed;
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


    private float distanceToPlayer;

    private Vector3 direction;
    private bool allowFire;
    private bool facingRight;
    // Start is called before the first frame update
    void Start()
    {
        enemyRigidBody.freezeRotation = true;
        allowFire = true;
        facingRight = true;
        player = GameObject.Find("UFO");
    }

    private void FixedUpdate() {
        //update distances
        distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);


        // If the player is within alert radius, and inside shooting range
        if (distanceToPlayer <= shootingDistance && distanceToPlayer <= alertRadius){
            //if we face right and player is left, or we face left and player is right
            if(facingRight == true && player.transform.position.x < transform.position.x || facingRight == false && player.transform.position.x > transform.position.x){
                flip();
            }
            //aim at player by setting the firepoint to point at the player

            //shoot at the player
            isShootingPlayer = true;
            if (allowFire) {
                StartCoroutine(shoot());
            }
        }

        // If the player is within the alert radius, and outside shooting distance
        if (distanceToPlayer <= alertRadius && distanceToPlayer > shootingDistance){
            //if we face right and player is left, or we face left and player is right
            if(facingRight == true && player.transform.position.x < transform.position.x || facingRight == false && player.transform.position.x > transform.position.x){
                flip();
            }
            //go towards player
            isTrackingPlayer = true;
        }

        // If player is outside of alert radius
        if (distanceToPlayer > alertRadius){
            //go home
            isTrackingPlayer = false;
            isShootingPlayer = false;
        }
    }

    void flip(){
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }

    IEnumerator shoot() {
        // allowFire = false;
        // direction = player.transform.position - firePoint.transform.position;
        // float rotationZ = Mathf.Atan2 (direction.y, direction.x) * Mathf.Rad2Deg;
        // firePoint.transform.rotation = Quaternion.Slerp(firePoint.transform.rotation, Quaternion.Euler(0, 0, rotationZ), 100 * Time.deltaTime);
        // yield return new WaitForSeconds(rateOfFire);
        // //shoot
        // Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        // allowFire = true;
        allowFire = false;
        direction = player.transform.position - firePoint.transform.position;
        float rotationZ = Mathf.Atan2 (direction.x, direction.y) * Mathf.Rad2Deg;
        firePoint.transform.rotation = Quaternion.Slerp(firePoint.transform.rotation, Quaternion.Euler(0, 0, rotationZ), 100 * Time.deltaTime);
        yield return new WaitForSeconds(rateOfFire);
        //shoot
        bulletPrefab.GetComponent<ProjectileDirection>().direction = rotationZ;
        Instantiate(bulletPrefab, firePoint.position, Quaternion.Euler(0,0,0));
        allowFire = true;
    }
}
