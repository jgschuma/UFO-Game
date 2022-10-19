using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TwisterPower : MonoBehaviour
{
    public GameObject UFO;
    public float twisterSpeed = 400f;
    public float winddownDecel = 8f;

    public GetControllerInput contInput;
    public Animator anim;
    
    // Start is called before the first frame update
    void Start()
    {
        contInput = UFO.GetComponent<GetControllerInput>();
        anim = UFO.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //Start the Twister
        if(contInput.GetButtonDown("Fire2") && anim.GetBool("twisterOn") == false)
        {
            contInput.ResetDirections();
            UFO.GetComponent<BeamController>().enabled = false;
            contInput.enabled = false;
            anim.SetBool("twisterOn", true);
            //Maintain movement speed and direction and apply it to ProjectileDirection
            float xVelocity = UFO.GetComponent<PlayerController>().movement.x;
            float yVelocity = UFO.GetComponent<PlayerController>().movement.y;
            UFO.GetComponent<ProjectileDirection>().SetXAndYSpeed(xVelocity, yVelocity);
            UFO.GetComponent<ProjectileDirection>().enabled = true;
        }
        //Twister has picked up to full speed
        else if (anim.GetCurrentAnimatorStateInfo(0).IsName("TwisterActive"))
        {
            UFO.GetComponent<ProjectileDirection>().speed = twisterSpeed;
        }
        //Twister is winding down
        else if (anim.GetCurrentAnimatorStateInfo(0).IsName("TwisterWinddown") && UFO.GetComponent<ProjectileDirection>().speed != 0)
        {
            UFO.GetComponent<ProjectileDirection>().speed = Math.Max(UFO.GetComponent<ProjectileDirection>().speed - winddownDecel, 0);
        }
        //The twister is over, restore control
        else if (anim.GetCurrentAnimatorStateInfo(0).IsName("TwisterOver"))
        {
            anim.SetBool("twisterOn", false);
            UFO.GetComponent<ProjectileDirection>().enabled = false;
            UFO.GetComponent<BeamController>().enabled = true;
            contInput.enabled = true;
        }
    }
}
