using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WarpStart : MonoBehaviour
{
    public Collider2D WarpCollider;
    private Animator Anim;
    private WarpBox StartWarpBox;

    // Start is called before the first frame update
    void Start()
    {
        WarpProjectile.RiftOpen += CanStartWarp;
        EnableStartRift.WarpActive += FirstRiftOpen;
        WarpPower.PowerWarp += PowerWarpController;
        Anim = GetComponent<Animator>();
        StartWarpBox = GetComponent<WarpBox>();
    }

    void FirstRiftOpen(){
        //Anim.SetBool("open", true);
    }

    void CanStartWarp(bool CanWarp){
        if(CanWarp == true){
            Anim.SetBool("open", true);
        }
        else{
            Anim.SetBool("open", false);
        }
    }

    void PowerWarpController(){
        Anim.SetBool("PowerWarp", true);
    }

    void PowerWarpDone(){
        Anim.SetBool("PowerWarp", false);
    }
}
