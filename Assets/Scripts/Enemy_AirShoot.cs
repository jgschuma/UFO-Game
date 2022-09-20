using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_AirShoot : MonoBehaviour
{
    public GameObject player;
    public float speed;
    public float wanderRadius;
    public float alertRadius;
    public float shootingDistance;

    private Vector2 homebase;
    private float distanceToPlayer;
    private float distanceFromHome;
    private Vector2 direction;
    // Start is called before the first frame update
    void Start()
    {
        homebase = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //update distances
        distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
        distanceFromHome = Vector2.Distance(transform.position, homebase);

        // If the player is within alert radius, and inside shooting range
        if (distanceToPlayer <= shootingDistance && distanceToPlayer <= alertRadius){
            //shoot at the player
            shoot();
        }

        // If the player is within the alert radius, outside shooting distance, and within wander radius
        if (distanceToPlayer <= alertRadius && distanceToPlayer > shootingDistance && distanceFromHome < wanderRadius){
            //go towards player
            transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
        }

        // If outside of wander radius or player is outside of alert radius
        if (distanceFromHome >= wanderRadius || distanceToPlayer > alertRadius){
            //go home
            transform.position = Vector2.MoveTowards(this.transform.position, homebase, speed * Time.deltaTime);
        }
         
    }

    void shoot(){

    }
}
