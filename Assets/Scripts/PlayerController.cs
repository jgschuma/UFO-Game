using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //The UFO's move speed
    public float moveSpeed = 40f;

    // These variables deal with Player movement
    public float horizontalInput;
    public float verticalInput;
    public Rigidbody2D UFO_Rigidbody;
    Vector3 movement;
    public Animator anim;

    // /*These variables allow for the use of the tractorBeam
    // game object*/
    // public GameObject tractorBeam;
    // public bool hasItem;
    // public bool beamOnCooldown;
    // public float beamCoolDuration;



    // Start is called when the game starts
    private void Start()
    {

        UFO_Rigidbody.freezeRotation = true;

        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        // Checks to see if the horzontal or vertical inputs are being pressed
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        // Set the movement vector for use in FixedUpdate()

        anim.SetInteger("HoriInput", Math.Sign(horizontalInput));
        anim.SetInteger("VertiInput", Math.Sign(verticalInput));


        movement = new Vector3(horizontalInput, verticalInput, 0).normalized;

        // calls manager for the tractor beam
        //tractorBeamController();
    }

    // We use FixedUpdate to actually move the player to avoid frame rate causing a difference in move speed
    void FixedUpdate(){
        UFO_Rigidbody.MovePosition(transform.position + movement * moveSpeed* Time.deltaTime);
    }


    // // This method controls the TractorBeam game object
    // public void tractorBeamController()
    // {
    //     // If key is pressed and no item is held, activate the tractorbeam
    //     if (Input.GetButton("Fire1") && hasItem == false && beamOnCooldown == false)
    //     {
    //         tractorBeam.SetActive(true);
    //     } // If key is pressed and an Item is held, drop the item
    //     else if (Input.GetButton("Fire1") && hasItem == true && beamOnCooldown == false){
    //         Debug.Log("Item has been dropped");
    //         hasItem = false;
    //         tractorBeam.SetActive(false);
    //         StartCoroutine(BeamCooldown());
    //     } // Else keep the tractor beam off
    //     else
    //         tractorBeam.SetActive(false);
    // }

    // public IEnumerator BeamCooldown(){
    //     beamOnCooldown = true;

    //     yield return new WaitForSeconds(beamCoolDuration);

    //     beamOnCooldown = false;
    // }

}
