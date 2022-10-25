using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileDestroyOnImpact : MonoBehaviour
{
    public bool hasDestroyAnimation = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Terrain" || 
            (other.gameObject.tag == "Player" && gameObject.tag != "PlayerAttack") ||
            (other.gameObject.tag == "Enemy" && gameObject.tag != "EnemyAttack"))
        {
            if (hasDestroyAnimation)
            {
                GetComponent<Animator>().SetTrigger("Destroy");
                GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;
            }
            else
            {
                //Debug.Log("DO IT NOW GOHAN");
                Destroy(gameObject);
            }
        }
    }
}
