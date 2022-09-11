using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //The UFO's move speed
    public float moveSpeed = 40f;

    public float horizontalInput;
    public float verticalInput;

    public Rigidbody2D rigidbody;
    Vector3 movement;
    public Animator anim;

    public GameObject tractorBeamFront;
    public GameObject tractorBeamBack;



    // Start is called when the game starts
    private void Start()
    {
        rigidbody.freezeRotation = true;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //Checks to see if the horzontal or vertical inputs are being pressed
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        anim.SetInteger("HoriInput", Math.Sign(horizontalInput));
        anim.SetInteger("VertiInput", Math.Sign(verticalInput));


        //Move the UFO based on player input
        //transform.Translate(new Vector2(horizontalInput, verticalInput) * moveSpeed * Time.deltaTime);
        movement = new Vector3(horizontalInput, verticalInput, 0).normalized;
    }

    void FixedUpdate(){
        rigidbody.MovePosition(transform.position + movement * moveSpeed* Time.deltaTime);
    }


    /*void TractorBeamAnim(){
        while(Input.GetKeyDown(Fire1) == true){
            TractorBeamBack.spriteRenderer.enabled = true;
            TractorBeamFront.spriteRenderer.enabled = true;
        }

    }*/

}
