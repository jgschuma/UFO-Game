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


    // Start is called when the game starts
    private void Start()
    {

        UFO_Rigidbody.freezeRotation = true;

<<<<<<< HEAD
        anim = GetComponent<Animator>();
=======
        //anim = GetComponent<Animator>();

>>>>>>> 4aa52268c6ba1f8fd6755f20a43c5a999b972fff
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

    }

    // We use FixedUpdate to actually move the player to avoid frame rate causing a difference in move speed
    void FixedUpdate(){
        UFO_Rigidbody.MovePosition(transform.position + movement * moveSpeed* Time.deltaTime);
    }

}
