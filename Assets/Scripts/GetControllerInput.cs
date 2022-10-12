using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GetControllerInput : MonoBehaviour
{
    public float horizontalInput;
    public float verticalInput;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Checks to see if the horzontal or vertical inputs are being pressed
        horizontalInput = Math.Sign(Input.GetAxis("Horizontal"));
        verticalInput = Math.Sign(Input.GetAxis("Vertical"));
    }
}
