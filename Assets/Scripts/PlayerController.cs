using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //The UFO's move speed
    public float moveSpeed = 40f;

    public float horizontalInput;
    public float verticalInput;

    public Rigidbody2D UFO_Rigidbody;
    Vector3 movement;

    public GameObject tractorBeam;



    // Start is called when the game starts
    private void Start()
    {
        UFO_Rigidbody.freezeRotation = true;
        tractorBeam.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        //Checks to see if the horzontal or vertical inputs are being pressed
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        //Move the UFO based on player input
        movement = new Vector3(horizontalInput, verticalInput, 0).normalized;

        tractorBeamController();
    }

    void FixedUpdate(){
        UFO_Rigidbody.MovePosition(transform.position + movement * moveSpeed* Time.deltaTime);
    }


    // This method controls the TractorBeam game object
    public void tractorBeamController()
    {
        if (Input.GetButton("Fire1"))
        {
            tractorBeam.SetActive(true);
        }
        else
            tractorBeam.SetActive(false);
    }
}
