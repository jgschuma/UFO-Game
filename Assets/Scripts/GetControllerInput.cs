using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
    }
}
