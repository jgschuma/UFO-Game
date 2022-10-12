using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //The UFO's movement values
    public float moveSpeed = 1f;
    public float moveAccel = 1f;
    public double decelRate = 2;
    public float dashSpeed = 2f;
    public float dashCooldownTime = 2f;
    public float dashDecayTime = 0.5f;

    // These variables deal with Player movement
    public float xSpeed, ySpeed;
    public float currMaxSpeed;
    public float dashCooldown = 0;
    public float dashDecay = 0;
    private Rigidbody2D UFO_Rigidbody;
    private GetControllerInput contInput;
    public float horizontalInput;
    public float verticalInput;
    public Vector3 movement;
    BeamController beamControl;

    // Start is called when the game starts
    private void Start()
    {
        beamControl = GetComponent<BeamController>();
        UFO_Rigidbody = GetComponent<Rigidbody2D>();
        UFO_Rigidbody.freezeRotation = true;
        contInput = GetComponent<GetControllerInput>();
        xSpeed = 0;
        ySpeed = 0;
        currMaxSpeed = moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        // Checks to see if the horzontal or vertical inputs are being pressed
        horizontalInput = contInput.horizontalInput;
        verticalInput = contInput.verticalInput;

        //If the button is pressed, the UFO has no item, and the cooldown has worn off, DASH
        if (Input.GetButton("Fire2") && beamControl.hasItem == false && dashCooldown == 0 && (horizontalInput != 0 || verticalInput != 0))
        {
            xSpeed = dashSpeed * horizontalInput;
            ySpeed = dashSpeed * verticalInput;
            dashCooldown = dashCooldownTime;
            dashDecay = dashDecayTime;
            currMaxSpeed = dashSpeed;
        }
        else
        {
            //If speed is above normal but dash decay is kicking in, reduce currMaxSpeed
            if (dashDecay == 0 && currMaxSpeed > moveSpeed)
            {
                currMaxSpeed -= moveAccel;
                currMaxSpeed = Math.Min(currMaxSpeed, (float)Math.Sqrt(Math.Pow(xSpeed, 2) + Math.Pow(ySpeed, 2)));
                currMaxSpeed = Math.Max(currMaxSpeed, moveSpeed);
            }
            
            //If no direction is being held, decelerate
            if (horizontalInput == 0)
                xSpeed = Math.Max(Math.Abs(xSpeed) - moveAccel, 0) * Math.Sign(xSpeed);
            //If direction held is opposite of direction headed, use increased decel rate
            else if (Math.Sign(xSpeed) == -horizontalInput)
                xSpeed -= (float)((moveAccel * decelRate) * Math.Sign(xSpeed));
            //Otherwise, accelerate like normal
            else
                xSpeed = Math.Min(Math.Abs(xSpeed) + moveAccel, currMaxSpeed) * horizontalInput;

            //If no direction is being held, decelerate
            if (verticalInput == 0)
                ySpeed = Math.Max(Math.Abs(ySpeed) - moveAccel, 0) * Math.Sign(ySpeed);
            //If direction held is opposite of direction headed, use increased decel rate
            else if (Math.Sign(ySpeed) == -verticalInput)
                ySpeed -= (float)((moveAccel * decelRate) * Math.Sign(ySpeed));
            //Otherwise, accelerate like normal
            else
                ySpeed = Math.Min(Math.Abs(ySpeed) + moveAccel, currMaxSpeed) * verticalInput;
        }

        //Accelerate the UFO
        if (Math.Pow(xSpeed, 2) + Math.Pow(ySpeed, 2) >= Math.Pow(currMaxSpeed, 2))
        {
            movement = new Vector3(xSpeed, ySpeed, 0).normalized;
            movement = new Vector3(movement.x * currMaxSpeed, movement.y * currMaxSpeed, 0);
        }
        else
            movement = new Vector3(xSpeed, ySpeed, 0);
    }

    // We use FixedUpdate to actually move the player to avoid frame rate causing a difference in move speed
    void FixedUpdate()
    {
        UFO_Rigidbody.MovePosition(transform.position + movement * Time.deltaTime);
        if (dashCooldown > 0)
            dashCooldown = Math.Max(dashCooldown - Time.deltaTime, 0);
        if (dashDecay > 0)
            dashDecay = Math.Max(dashDecay - Time.deltaTime, 0);
    }
}
