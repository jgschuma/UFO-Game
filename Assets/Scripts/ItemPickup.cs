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
    public string thisPower;
    // Start is called before the first frame update
    void Start()
    {
        // We freeze the pickup rotation so that it stays upright at all times
        ThisRigidBody.freezeRotation = true;

        /* These lines are neccessary because the objects are being moved to 
         * prefabs and will need to have their variables set on creation
         */
        TractorBeam = GameObject.Find("TractorBeam");
        ThisPower = GameObject.Find(thisPower);
    }

    // Whenever a pickup collides with an object, check to see if it is a player
    void OnTriggerEnter2D (Collider2D other)
    {
        if (other.CompareTag("Player") && TractorBeam.activeInHierarchy)
        {
            // If so, pickup the object
            Pickup(other);
        }
    }

    void Pickup(Collider2D player)
    {
        Debug.Log("Power up picked up");
        // Tell the TractorBeam we have an item and to start it's cooldown
        player.GetComponent<BeamController>().hasItem = true;
        player.GetComponent<BeamController>().StartCooldown();

        // Set the current Item so that it knows what to spawn when we drop an item
        player.GetComponent<BeamController>().currentItem = GameObject.Find(thisName);
        
        /* These lines activates the power on the UFO and set the power on the TractorBeam
         * so that it knows which power to disable on item drop
         */
        player.GetComponent<BeamController>().currentPower = ThisPower;
        ThisPower.SetActive(true);

        ThisPickup.SetActive(false);
    }
}
