using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public Rigidbody2D ShieldRigidBody;
    public GameObject ShieldPickup;
    public string thisName = "ShieldPickup";

    // Start is called before the first frame update
    void Start()
    {
        ShieldRigidBody.freezeRotation = true;
    }

    void OnTriggerEnter2D (Collider2D other)
    {
        if (other.CompareTag("Player") && Input.GetButton("Fire1"))
        {
            Pickup(other);
        }
    }

    void Pickup(Collider2D player)
    {
        Debug.Log("Power up picked up");
        player.GetComponent<BeamController>().hasItem = true;
        player.GetComponent<BeamController>().StartCooldown();
        player.GetComponent<BeamController>().currentItem = GameObject.Find("ShieldPickup");
        ShieldPickup.SetActive(false);
        
    }
}
