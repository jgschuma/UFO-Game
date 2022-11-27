using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileDestroyOnImpact : MonoBehaviour
{
    public bool hasDestroyAnimation = false;
    public bool breakOnHit = false;
    private bool destroyNextFrame = false;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (destroyNextFrame)
            Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Terrain" || 
            (breakOnHit && (other.gameObject.tag == "Player" && gameObject.tag != "PlayerAttack") ||
            (other.gameObject.tag == "Enemy" && gameObject.tag != "EnemyAttack")))
        {
            if (hasDestroyAnimation)
            {
                GetComponent<Animator>().SetTrigger("Destroy");
                GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;
            }
            else
            {
                destroyNextFrame = true;
            }
        }
    }
}
