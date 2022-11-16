using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_GroundStationaryShoot: MonoBehaviour
{
    [Tooltip("The player to follow.")]
    public GameObject player;
/*    [Tooltip("Distance from which the enemy will be alerted to the player")]
    public float alertRadius;*/
    [Tooltip("Minimum distance from the player the enemy will keep")]
    public float shootingDistance;
    [Tooltip("Time to take a shot")]
    public float timeToFire;
    [Tooltip("Time between shooting and being ready to shoot again")]
    public float timeToArmShot;
    public bool isTrackingPlayer;
    public bool isShootingPlayer;
    public Transform firePoint;
    public GameObject bulletPrefab;
    public Animator anim;


    private float distanceToPlayer;

    private Vector3 direction;
    private bool allowFire;
    // Start is called before the first frame update
    void Start()
    {
        allowFire = false;
        player = GameObject.Find("UFO");
        anim = GetComponent<Animator>();
        anim.SetBool("faceRight", GetComponent<SpriteRenderer>().flipX);
    }

    private void FixedUpdate() {
        //update distances
        distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

        //Enemy is dead, turn everything off
        if(anim.GetInteger("health") == 0)
        {
            GetComponent<EntityHealth>().enabled = false;
            GetComponent<Enemy_GroundStationaryShoot>().enabled = false;
        }
        //If not hurt
        else if(!anim.GetBool("hurt"))
        {
            //If awake
            if (anim.GetBool("awake"))
            {
                //Set facing right relative to player position
                //Mushroom is rightside up
                if(transform.rotation.eulerAngles.z % 360 == 0)
                    anim.SetBool("faceRight", player.transform.position.x > transform.position.x);
                //Mushroom is on right wall
                else if(transform.rotation.eulerAngles.z % 360 == 90)
                    anim.SetBool("faceRight", player.transform.position.y > transform.position.y);
                //Mushroom is upside down
                else if (transform.rotation.eulerAngles.z % 360 == 180)
                    anim.SetBool("faceRight", player.transform.position.x < transform.position.x);
                //Mushroom is on left wall
                else if (transform.rotation.eulerAngles.z % 360 == 270)
                    anim.SetBool("faceRight", player.transform.position.x < transform.position.x);

                //Shoot at the player if in range
                if (distanceToPlayer <= shootingDistance && allowFire)
                {
                    StartCoroutine(shoot());
                }
                //Go to sleep if out of range
                else if (distanceToPlayer > shootingDistance)
                    anim.SetBool("awake", false);
            }
            //If asleep but player is close enough, wake up
            else if (distanceToPlayer <= shootingDistance)
            {
                anim.SetBool("awake", true);
                StartCoroutine(waitForNextShot());
            }
        }
    }

    IEnumerator shoot() {
        anim.SetBool("shootingPlayer", true);
        allowFire = false;
        //firePoint.transform.rotation = Quaternion.Slerp(firePoint.transform.rotation, Quaternion.Euler(0, 0, rotationZ), 100 * Time.deltaTime);
        yield return new WaitForSeconds(timeToFire);
        //Cancel coroutine if enemy is already dead
        if (anim.GetInteger("health") > 0 && !anim.GetBool("hurt"))
        {
            //shoot
            direction = player.transform.position - firePoint.transform.position;
            float rotationZ = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
            bulletPrefab.GetComponent<ProjectileDirection>().direction = rotationZ;
            Instantiate(bulletPrefab, firePoint.position, Quaternion.Euler(0, 0, 0));
            StartCoroutine(waitForNextShot());
        }
/*        else
            allowFire = true;*/
        anim.SetBool("shootingPlayer", false);
    }

    IEnumerator waitForNextShot()
    {
        yield return new WaitForSeconds(timeToArmShot);
        if(anim.GetBool("awake"))
            allowFire = true;
    }

    private void OnDrawGizmos()
    {
        //RED -- Alert Area
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, shootingDistance);
    }
}
