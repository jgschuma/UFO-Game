﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EntityHealth : MonoBehaviour
{
    public int health = 3;
    public float invincibilityPeriod = 2.5f;
    public float hurtPeriod = 1f;
    public string hurtTag;

    float invincibilityLeft = 0;
    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        invincibilityLeft = Math.Max(0, invincibilityLeft - Time.deltaTime);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        //If entity is invincible, ignore all collision and reduce iFrames
        if(invincibilityLeft > 0)
        {
            if (invincibilityPeriod - invincibilityLeft > hurtPeriod)
                anim.SetBool("hurt", false);
        }
        //If object is supposed to be hurt
        else if(hurtTag == other.gameObject.tag && invincibilityLeft == 0)
        {
            anim.SetBool("hurt", true);
            invincibilityLeft = invincibilityPeriod;
            doDamage(other.gameObject.GetComponent<DoesDamage>().damage);
        }
    }

    public void doDamage(int _damageAmount)
    {
        health -= _damageAmount;
        if (health <= 0)
        {
            //Kill the entity
        }
    }
}
