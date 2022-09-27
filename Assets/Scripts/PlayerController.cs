using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //The UFO's movement values
    public float moveSpeed = 40f;
    public float moveAccel = 1f;
    public double decelRate = 2;

    // These variables deal with Player movement
    private float xSpeed, ySpeed;
    private Rigidbody2D UFO_Rigidbody;
    public float horizontalInput;
    public float verticalInput;
    Vector3 movement;


    // Start is called when the game starts
    private void Start()
    {
        UFO_Rigidbody = GetComponent<Rigidbody2D>();
        UFO_Rigidbody.freezeRotation = true;
        xSpeed = 0;
        ySpeed = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // Checks to see if the horzontal or vertical inputs are being pressed
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        //If no direction is being held, decelerate
        if (Math.Sign(horizontalInput) == 0)
            xSpeed = Math.Max(Math.Abs(xSpeed) - moveAccel, 0) * Math.Sign(xSpeed);
        //If direction held is opposite of direction headed, use increased decel rate
        else if (Math.Sign(xSpeed) == -Math.Sign(horizontalInput))
            //xSpeed = (float)(Math.Max(Math.Abs(xSpeed)-(moveAccel*decelRate), moveAccel*Math.Sign(horizontalInput)));
            xSpeed -= (float)((moveAccel * decelRate) * Math.Sign(xSpeed));
        //Otherwise, accelerate like normal
        else
            xSpeed = Math.Min(Math.Abs(xSpeed) + moveAccel, moveSpeed) * Math.Sign(horizontalInput);

        //If no direction is being held, decelerate
        if (Math.Sign(verticalInput) == 0)
            ySpeed = Math.Max(Math.Abs(ySpeed) - moveAccel, 0) * Math.Sign(ySpeed);
        //If direction held is opposite of direction headed, use increased decel rate
        else if (Math.Sign(ySpeed) == -Math.Sign(verticalInput))
            //ySpeed = (float)(Math.Max(Math.Abs(ySpeed)-(moveAccel*decelRate), moveAccel* Math.Sign(verticalInput)));
            ySpeed -= (float)((moveAccel * decelRate) * Math.Sign(ySpeed));
        //Otherwise, accelerate like normal
        else
            ySpeed = Math.Min(Math.Abs(ySpeed) + moveAccel, moveSpeed) * Math.Sign(verticalInput);

        //Accelerate the UFO
        if (Math.Pow(xSpeed, 2) + Math.Pow(ySpeed, 2) >= Math.Pow(moveSpeed, 2))
        {
            movement = new Vector3(xSpeed, ySpeed, 0).normalized;
            movement = new Vector3(movement.x * moveSpeed, movement.y * moveSpeed, 0);
        }
        else
        {
            movement = new Vector3(xSpeed, ySpeed, 0);
        }
    }

    // We use FixedUpdate to actually move the player to avoid frame rate causing a difference in move speed
    void FixedUpdate()
    {
        UFO_Rigidbody.MovePosition(transform.position + movement * Time.deltaTime);
    }

}
