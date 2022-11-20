using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EntityHealth : MonoBehaviour
{
    public int health = 3;
    public float invincibilityPeriod = 2.5f;
    public float hurtPeriod = 1f;
    public float invincFlashTime = 0.05f;
    public string hurtTag;
    public bool invincible = false;

    [Header("ENEMY ONLY")]
    public int pointValue;
    [Header("UFO ONLY")]
    public int perHPScore;

    Coroutine lastRoutine = null;
    float invincibilityLeft = 0;
    Animator anim;

    private void OnEnable(){
        // This is being used here more as onGameEnd
        AustinEventManager.onGameOver += AddHealthPoints;
    }

    private void OnDisable(){
        AustinEventManager.onGameOver -= AddHealthPoints;
    }

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
            //Invincibility period just ended
            if (invincibilityLeft <= 0)
            {
                //Stop flickering
                StopCoroutine(lastRoutine);
                GetComponent<SpriteRenderer>().enabled = true;
/*                if (health <= 0)
                    enabled = false;*/
            }
/*            //Hurt period is over
            if (invincibilityPeriod - invincibilityLeft >= hurtPeriod)
            {
                anim.SetBool("hurt", false);
            }*/
        }
        //Hurt period is over
        if (invincibilityPeriod - invincibilityLeft >= hurtPeriod)
        {
            anim.SetBool("hurt", false);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        OnTrigger(other);
    }

    void OnTriggerStay2D(Collider2D other)
    {
        OnTrigger(other);
    }

    void OnTrigger(Collider2D other)
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
            /*            anim.SetBool("hurt", true);
                        invincibilityLeft = invincibilityPeriod;*/
            doDamage(other.gameObject.GetComponent<DoesDamage>().damage);

            if (gameObject.name == "UFO" && health > 0)
            {
                FindObjectOfType<AudioManager>().Play("PlayerHurt");
            }
            else if (gameObject.name == "UFO" && health == 0)
            {
                FindObjectOfType<AudioManager>().Play("PlayerDeath");
            }
            else if (gameObject.name != "UFO" && health > 0)
            {
                FindObjectOfType<AudioManager>().Play("EnemyHurt");
            }
            else if (gameObject.name != "UFO" && health == 0)
            {
                FindObjectOfType<AudioManager>().Play("EnemyDeath");
            }
        }
    }

    public void doDamage(int _damageAmount)
    {
        if (_damageAmount > 0)
        {
            invincibilityLeft = invincibilityPeriod;
            health -= _damageAmount;
            GetComponent<Animator>().SetInteger("health", Math.Max(health, 0));
            if (health <= 0)
            {
                if (gameObject.tag == "Enemy")
                {
                    AustinEventManager.ScorePoints(pointValue);
                }
                else if (gameObject.tag == "Player")
                {
                    AustinEventManager.GameOver(true);
                    /*The following code kills the player, comment it out for debugging purposes*/
                    transform.Find("UI").GetComponent<Timer>().timerOn = false;
                    gameObject.transform.Find("Main Camera").parent = null;
                    gameObject.transform.Find("UI").transform.Find("Health Meter").GetComponent<Animator>().SetInteger("health", 0);
                    gameObject.transform.Find("UI").parent = null;
                    Instantiate(GetComponent<AnimateUFO>().deathExplosion, transform.position, Quaternion.Euler(0, 0, 0));
                    Destroy(gameObject);
                }
            }
            if (health > 0)
                lastRoutine = StartCoroutine(InvincibilityFlash());
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

    void AddHealthPoints(bool endedDueToDeath){
        if (gameObject.name == "UFO"){
            AustinEventManager.ScorePoints(perHPScore * health);
            AustinEventManager.CalcFinished("healthCalc");
        }
    }
}
