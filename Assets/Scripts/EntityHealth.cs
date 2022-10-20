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
            //Nothing for now
        }
        //If object is supposed to be hurt
        else if(hurtTag == other.gameObject.tag && invincibilityLeft == 0)
        {
            doDamage(other.gameObject.GetComponent<DoesDamage>().damage);
            anim.SetBool("hurt", true);
            invincibilityLeft = invincibilityPeriod;
            other.gameObject.GetComponent<ProjectileDirection>().DestroyProjectile();
        }
    }

    public void doDamage(int _damageAmount)
    {
        health -= _damageAmount;
        if (health <= 0)
        {
            //Kill the entity
            if (gameObject.tag != "Player")
            {
                anim.SetBool("dead", true);
            }
            else
            {
                //Player has dies, PlayerDeath event here
            }
            this.enabled = false;
        }
    }
}
