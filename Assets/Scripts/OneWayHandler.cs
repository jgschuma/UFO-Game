using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayHandler : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Yowhaddup " + other.gameObject.name + "?");
        if (other.gameObject.GetComponent<breaksOneWays>() != null)
        {
            transform.parent.GetComponent<Animator>().SetTrigger("destroy");
            FindObjectOfType<AudioManager>().PlayInteractable("Explosion");
        }
    }
}