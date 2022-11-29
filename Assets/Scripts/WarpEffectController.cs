using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WarpEffectController : MonoBehaviour
{
    
    public Animator WarpEffectAnim;
    private bool FinishAnim = false;

    void Start(){
        WarpBox.RiftDisabled += WarpAndPickup;
    }
    void Update(){
        if(FinishAnim == true ){
            FinishAnim = false;
            WarpEffectAnim.Play("WarpEffectEnd");
        }
    }

    public void StartAnimisFinished()
    {
        WarpEffectAnim.SetBool("StartDone", true);
    }
    public void EndAnimisFinished(){
        Destroy(this.gameObject);
    }

    /* If the player manages to pick up the Warp power at the same time they teleport, we'll cancel the teleport animation
    // This prevents the player from getting stuck with a warpEffect covering their screen 
    */
    void WarpAndPickup(){
        FinishAnim = true;
    }
}
