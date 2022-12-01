using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WarpProjectile : MonoBehaviour
{
    public Animator Anim;
    public GameObject WarpRiftStart;
    private Collider2D[] WarpZone;
    //public static event Action<bool> RiftOpen;

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

    
    void OnTriggerEnter2D(Collider2D other)
    {
        OnTrigger(other);
    }
    void OnTriggerStay2D(Collider2D other)
    {
        OnTrigger(other);
    }
    //When the Projectile is on the ground open the rift
    void OnTrigger(Collider2D other)
    {
        if (other.CompareTag("Terrain") || other.CompareTag("ItemPickup"))
        {
            Debug.Log("Warp Puck is touching " + other.gameObject.tag);
            // Create the rift above the puck
            Anim.SetBool("open", true);
            // Also open the start rift
            if(WarpRiftStart.GetComponent<WarpStart>().Anim.GetBool("open") == false){
                WarpRiftStart.GetComponent<WarpStart>().CanStartWarp(true);
            }
            //RiftOpen?.Invoke(true); // Problem spot???
        }
    }

    // When the projectile leaves the ground, close the rift
    void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Terrain") || other.CompareTag("ItemPickup"))
        {
            Debug.Log("Warp Puck left " + other.gameObject.tag);
            // Close the rift above the puck
            Anim.SetBool("open", false);
            // Also close the start rift
            //RiftOpen?.Invoke(false);
            WarpRiftStart.GetComponent<WarpStart>().CanStartWarp(false);
        }
    }
}
