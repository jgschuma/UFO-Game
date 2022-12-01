using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WarpStart : MonoBehaviour
{
    public Collider2D WarpCollider;
    public Animator Anim;
    private WarpBox StartWarpBox;

    // Start is called before the first frame update
    void Start()
    {
        //WarpProjectile.RiftOpen += CanStartWarp;
        WarpPower.PowerWarp += PowerWarpController;
        Anim = GetComponent<Animator>();
        StartWarpBox = GetComponent<WarpBox>();
    }

    void Update(){
        if (Anim == null){
            Anim = GetComponent<Animator>();
        }
    }
    // This Function controls the start rift opening and closing with the WarpPickup
    public void CanStartWarp(bool CanWarp){
        if(Anim == null){
            Anim = GetComponent<Animator>();
        }
        if(CanWarp == true){ 
            Anim.SetBool("open", true);
        }
        else{
            Anim.SetBool("open", false);
        }
    }

    // This function will be triggered  in the WarpPower script when the player uses the warp power to go back to start
    void PowerWarpController(){
        if(Anim != null){
            Anim.SetBool("PowerWarp", true);
        }
    }

    // This function is called by the Animator when the StartRiftPowerWarp animation is done
    void PowerWarpDone(){
        Anim.SetBool("PowerWarp", false);
    }

    void OnDestroy(){}
}
