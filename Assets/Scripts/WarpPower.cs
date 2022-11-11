using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpPower : MonoBehaviour
{
    public GameObject WarpPrefab;
    public Camera MainCam;
    public Transform WarpLocation;
    private bool WarpInProgress = false;
    private bool isFinished;
    public GameObject Player;
    private Animator WarpAnim;
    // This does nothing as of yet
    
    void Update(){
        if(Input.GetButton("Fire2") && WarpInProgress == false){
            WarpInProgress = true;
            GameObject WarpEffect = Instantiate(WarpPrefab, transform.position, Quaternion.identity);
            WarpEffect.GetComponent<Canvas>().worldCamera = MainCam;
            WarpAnim = WarpEffect.GetComponent<Animator>();
        }

        if(WarpAnim != null && WarpAnim.GetBool("StartDone") && WarpInProgress == true){
            Player.transform.position = WarpLocation.position;

            WarpInProgress = false;
            WarpAnim.SetBool("TeleportDone", true);
        }
    }

}
