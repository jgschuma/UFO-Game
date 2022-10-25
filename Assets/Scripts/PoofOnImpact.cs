using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoofOnImpact : MonoBehaviour
{
    public GameObject poof;
    public bool enabled = false;
    
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Terrain" && enabled)
        {
            Instantiate(poof, gameObject.transform.position, Quaternion.Euler(0, 0, 0));
            Destroy(gameObject);
        }
    }
}
