using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TwisterPower : MonoBehaviour
{
    public GameObject UFO;
    
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
        }
        //The twister is over, restore control
        else if (anim.GetCurrentAnimatorStateInfo(0).IsName("TwisterOver"))
        {
            anim.SetBool("twisterOn", false);
            UFO.GetComponent<BeamController>().enabled = true;
            contInput.enabled = true;
        }
        //Twister is in motion
        else
        {
            
        }
    }
}
