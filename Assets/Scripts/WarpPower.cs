using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WarpPower : MonoBehaviour
{
    public GameObject WarpPrefab;
    public Camera MainCam;
    public Transform WarpLocation;
    public bool WarpInProgress = false;
    //private bool isFinished;
    public GameObject Player;
    private Animator WarpAnim;

    public static event Action PowerWarp;
    
    void FixedUpdate(){
        // While holding the WarpPower pressing Fire2 allows the player to Warp to start
        if(Input.GetButton("Fire2") && WarpInProgress == false){
            WarpInProgress = true;
            GameObject WarpEffect = Instantiate(WarpPrefab, transform.position, Quaternion.identity);
            WarpEffect.GetComponent<Canvas>().worldCamera = MainCam;
            WarpAnim = WarpEffect.GetComponent<Animator>();
        }

        // Waits for the Screen cover animation to finish playing then warps the player
        if(WarpAnim != null && WarpAnim.GetBool("StartDone") && WarpInProgress == true){
            /* The PowerWarp Action tells the Start rift when to play the StartRiftPowerWarp Animation, which doesn't enable the warp box collider
                which will prevent the player from being able immediately trying to warp back to the warpPickup
            */
            PowerWarp?.Invoke();
            Player.transform.position = WarpLocation.position;
         
            WarpInProgress = false;
            WarpAnim.SetBool("TeleportDone", true);
        }
    }

}
