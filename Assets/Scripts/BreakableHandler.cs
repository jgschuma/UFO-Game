using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableHandler : MonoBehaviour
{
    public int health = 3;

    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<IsExplosive>() != null)
        {
            if (other.gameObject.name == "LaserImpact" && !anim.GetBool("hurt"))
            {
                anim.SetInteger("health", anim.GetInteger("health") - 1);
                anim.SetBool("hurt", true);
            }
            else if (other.gameObject.name != "LaserImpact")
                anim.SetInteger("health", 0);
        }
    }
}
