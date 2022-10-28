using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EntityHealth : MonoBehaviour
{
    public int pointValue;
    public int health = 3;
    public float invincibilityPeriod = 2.5f;
    public float hurtPeriod = 1f;
    public string hurtTag;
    public bool invincible = false;

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

    void OnTriggerStay2D(Collider2D other)
    {
        //If entity is invincible, ignore all collision and reduce iFrames
        if(invincibilityLeft > 0 || invincible)
        {
            //Debug.Log("Invincible: No damage");
            //Nothing for now
        }
        //If object is supposed to be hurt
        else if(hurtTag == other.gameObject.tag && invincibilityLeft == 0)
        {

            //Debug.Log("How is this hitting me");
            anim.SetBool("hurt", true);
            invincibilityLeft = invincibilityPeriod;
            doDamage(other.gameObject.GetComponent<DoesDamage>().damage);
        }
/*        //Destroys projectile if it's not supposed to pierce enemies (basically just the GunnerBullet)
        if (other.gameObject.GetComponent<DoesDamage>().breakOnHit)
            Destroy(other.gameObject);*/
    }

    public void doDamage(int _damageAmount)
    {
        health -= _damageAmount;
        GetComponent<Animator>().SetInteger("health", Math.Max(health, 0));
        if (health <= 0)
        {
            //Kill the entity
            AustinEventManager.ScorePoints(pointValue);
        }
    }
}
