using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public Rigidbody2D ShieldRigidBody;

    // Start is called before the first frame update
    void Start()
    {
        ShieldRigidBody.freezeRotation = true;
    }

    void OnTriggerEnter2D (Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Pickup();
        }
    }

    void Pickup()
    {
        Debug.Log("Power up picked up");

    }
}
