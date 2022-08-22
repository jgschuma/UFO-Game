using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //The UFO's move speed
    public float moveSpeed = 40f;

    public float horizontalInput;
    public float verticalInput;


    // Start is called when the game starts
    private void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        //Checks to see if the horzontal or vertical inputs are being pressed
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        //Move the UFO based on player input
        transform.Translate(new Vector3(horizontalInput, verticalInput, 0) * moveSpeed * Time.deltaTime);
    }

}
