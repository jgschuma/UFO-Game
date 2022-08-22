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
        //Assigns the horizontal and vertical axes to our controls
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
    }

    // Update is called once per frame
    void Update()
    {
        //transform.Translate(1, 0, 0);
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        //Move the UFO based on player input
        transform.Translate(new Vector3(horizontalInput, verticalInput, 0) * moveSpeed * Time.deltaTime);
    }

}
