using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TwisterPower : MonoBehaviour
{
    public GameObject UFO;
    public float startupDuration = 0.5f;
    public float activeDuration = 3f;
    public float winddownDuration = 0.5f;
    
/*    public float timer = 0f;*/
    public GetControllerInput contInput;
    public Animator anim;
    public GameObject twisterEffect;
    
    // Start is called before the first frame update
    void Start()
    {
        contInput = UFO.GetComponent<GetControllerInput>();
        anim = UFO.GetComponent<Animator>();
        //twisterEffect = (GameObject)UFO.transform.Find("Items/Twister Power/Twister Power").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if(contInput.GetButtonDown("Fire2") && anim.GetBool("twisterOn") == false)
        {
            anim.SetBool("twisterOn", true);
        }
/*        if (timer > 0)
            timer = Math.Max(0, timer - Time.deltaTime);

        //If twister is available, enter startup state
        if (contInput.GetButtonDown("Fire2") && anim.GetInteger("twisterState") == 0)
        {
            anim.SetInteger("twisterState", 1);
            timer = startupDuration;
        }
        //If windup has completed, enter active state
        else if (anim.GetInteger("twisterState") == 1 && timer == 0)
        {
            anim.SetInteger("twisterState", 2);
            twisterEffect.SetActive(true);
            timer = activeDuration;
        }
        //If active state has finished, enter winddown state
        else if (anim.GetInteger("twisterState") == 2 && timer == 0)
        {
            anim.SetInteger("twisterState", 3);
            timer = winddownDuration;
        }
        //If UFO has fully winded down, restor to neutral
        else if (anim.GetInteger("twisterState") == 3 && timer == 0)
        {
            anim.SetInteger("twisterState", 0);
        }*/
    }
}
