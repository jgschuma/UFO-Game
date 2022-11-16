using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WarpProjectile : MonoBehaviour
{
    private Animator Anim;
    private Collider2D[] WarpZone;
    public static event Action<bool> RiftOpen;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody2D>().freezeRotation = true;
        Anim = GetComponentInChildren(typeof(Animator)) as Animator;

        /* This Tells the UFO to not pick up the rift when the tractorbeam touches warp rift 
        but instead when it touches the warpProjectile.
        */
        Physics2D.IgnoreLayerCollision(15,19, true);

    }

    // When the Projectile is on the ground open the rift
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Terrain")){
            //Debug.Log("Warp Puck hit the enviromnet");
            // Create the rift above the puck
            Anim.SetBool("open", true);
            // Also open the start rift
            RiftOpen?.Invoke(true);
        }
    }
    // When the projectile leaves the ground, close the rift
    void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Terrain")){
            //Debug.Log("Warp Puck left the environment");
            // Create the rift above the puck
            Anim.SetBool("open", false);
            // Also open the start rift
            RiftOpen?.Invoke(false);
        }
    }
}
