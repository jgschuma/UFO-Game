using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableHandler : MonoBehaviour
{
    public int health = 3;
    public float invincibilityPeriod = 0.5f;

    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<IsExplosive>().explosive)
        {
            anim.SetInteger("health", 0);
        }
    }

    IEnumerator WaitInvincibilityPeriod()
    {
        yield return new WaitForSeconds(invincibilityPeriod);
        anim.SetBool("hurt", false);
    }
}
