using System.Collections;
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
        //Reduce iFrames if they exist
        if (invincibilityLeft > 0)
        {
            invincibilityLeft = Math.Max(0, invincibilityLeft - Time.deltaTime);
            if (invincibilityPeriod - invincibilityLeft >= hurtPeriod)
                anim.SetBool("hurt", false);
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        //If entity is invincible, ignore all collision and reduce iFrames
        if(invincibilityLeft > 0)
        {
            Debug.Log("Invincible: No damage");
            //Nothing for now
        }
        //If object is supposed to be hurt
        else if(hurtTag == other.gameObject.tag && invincibilityLeft == 0)
        {

            Debug.Log("How is this hitting me");
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
