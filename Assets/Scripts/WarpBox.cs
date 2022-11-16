using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpBox : MonoBehaviour
{
    public GameObject WarpPrefab;
    public Camera MainCam;
    public Transform WarpLocation;

    private bool PlayerCollision;
    private bool WarpInProgress = false;
    private bool isFinished;
    private GameObject Player;
    private Animator WarpAnim;

    // Update is called once per frame
    void Update()
    {
        
        if(Input.GetButton("Fire3") && PlayerCollision == true && WarpInProgress == false)
        {
            //Debug.Log("THEY DID THE WARP THING :O");
            WarpInProgress = true;
            // Disable player movement
            // Play warp effect
            GameObject WarpEffect = Instantiate(WarpPrefab, transform.position, Quaternion.identity);
            WarpEffect.GetComponent<Canvas>().worldCamera = MainCam;
            WarpAnim = WarpEffect.GetComponent<Animator>();
        }
        if(WarpAnim != null && WarpAnim.GetBool("StartDone") && WarpInProgress == true){  
                // Teleport
                Player.transform.position = WarpLocation.position;

                // Enable player movement;
                WarpInProgress = false;
                WarpAnim.SetBool("TeleportDone", true);
            }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            if(Player == null){
                Player = other.gameObject;
            }
            PlayerCollision = true;
            //Debug.Log("Player can do the warp thing :)");
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            PlayerCollision = false;
            //Debug.Log("Player can't do the warp thing :(");
        }
    }
}
