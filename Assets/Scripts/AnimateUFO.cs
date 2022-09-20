using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class AnimateUFO : MonoBehaviour
{
    private Animator anim;
    private PlayerController cont;

    // Start is called before the first frame update
    void Start()
    {
        cont = GetComponent<PlayerController>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Set the movement vector for use in FixedUpdate()
        anim.SetInteger("HoriInput", Math.Sign(cont.horizontalInput));
        anim.SetInteger("VertiInput", Math.Sign(cont.verticalInput));
    }
}
