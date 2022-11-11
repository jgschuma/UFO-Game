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
    public float invincFlashTime = 0.05f;
    public string hurtTag;
    public bool invincible;

    Coroutine lastRoutine = null;
    float invincibilityLeft = 0;
    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        invincible = false;
    }

    void Update()
    {
        //Reduce iFrames if they exist
        if (invincibilityLeft > 0)
        {
            invincibilityLeft = Math.Max(0, invincibilityLeft - Time.deltaTime);
            //Invincibility period just ended
            if (invincibilityLeft <= 0)
            {
                //Stop flickering
                StopCoroutine(lastRoutine);
                GetComponent<SpriteRenderer>().enabled = true;
/*                if (health <= 0)
                    enabled = false;*/
            }
            //Hurt period is over
            if (invincibilityPeriod - invincibilityLeft >= hurtPeriod)
            {
                anim.SetBool("hurt", false);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //If entity is invincible, ignore all collision and reduce iFrames
        if (invincibilityLeft > 0 || invincible)
        {
            //Debug.Log("Invincible: No damage");
            //Nothing for now
        }
        //If object is supposed to be hurt
        else if ((hurtTag == other.gameObject.tag || other.gameObject.tag == "EnvironHazard") && invincibilityLeft == 0 && !invincible)
        {
            anim.SetBool("hurt", true);
            invincibilityLeft = invincibilityPeriod;
            doDamage(other.gameObject.GetComponent<DoesDamage>().damage);
            if(health > 0)
                lastRoutine = StartCoroutine(InvincibilityFlash());

            if (gameObject.name == "UFO" && health > 0){
                FindObjectOfType<AudioManager>().Play("PlayerHurt");
            } else if (gameObject.name == "UFO" && health == 0){
                FindObjectOfType<AudioManager>().Play("PlayerDeath");
            } else if (gameObject.name != "UFO" && health > 0){
                FindObjectOfType<AudioManager>().Play("EnemyHurt");
            } else if (gameObject.name != "UFO" && health == 0){
                FindObjectOfType<AudioManager>().Play("EnemyDeath");
            }
        }
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

    //Makes the entity flash when they're invincible
    IEnumerator InvincibilityFlash()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(invincFlashTime);
        GetComponent<SpriteRenderer>().enabled = true;
        yield return new WaitForSeconds(invincFlashTime);
        lastRoutine = StartCoroutine(InvincibilityFlash());
    }
}
