using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class AnimateUFO : MonoBehaviour
{
    public GameObject deathExplosion;
    private Animator anim;
    private PlayerController cont;

    // Start is called before the first frame update
    void Start()
    {
        cont = GetComponent<PlayerController>();
        anim = GetComponent<Animator>();
    }

    void LateUpdate()
    {
        // Set the movement vector for use in FixedUpdate()
        anim.SetInteger("HoriInput", Math.Sign(cont.horizontalInput));
        anim.SetInteger("VertiInput", Math.Sign(cont.verticalInput));
    }
}
