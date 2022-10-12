using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public Rigidbody2D ThisRigidBody;
    public GameObject ThisPickup;
    public GameObject ThisPower;
    public GameObject TractorBeam;
    public string thisName;
    // Start is called before the first frame update
    void Start()
    {
        ThisRigidBody.freezeRotation = true;
    }

    void OnTriggerEnter2D (Collider2D other)
    {
        if (other.CompareTag("Player") && TractorBeam.activeInHierarchy)
        {
            Pickup(other);
        }
    }

    void Pickup(Collider2D player)
    {
        Debug.Log("Power up picked up");
        player.GetComponent<BeamController>().hasItem = true;
        player.GetComponent<BeamController>().StartCooldown();
        player.GetComponent<BeamController>().currentItem = GameObject.Find(thisName);
        player.GetComponent<BeamController>().currentPower = ThisPower;
        ThisPower.SetActive(true);

        ThisPickup.SetActive(false);
    }
}
