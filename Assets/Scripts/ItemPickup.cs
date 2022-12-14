using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ItemPickup : MonoBehaviour
{
    public Rigidbody2D ThisRigidBody;
    public GameObject TractorBeam;
    public GameObject ThisPower;
    public GameObject DropPickup;

    //Stuff related to the item pickup text
    public Sprite pickupNameSprite;
    public Sprite discoveryText;
    public Sprite acquiredText;
    public GameObject pickupText;
    
    // Start is called before the first frame update
    void Start()
    {
        // We freeze the pickup rotation so that it stays upright at all times
        ThisRigidBody.freezeRotation = true;
    }

    // Whenever a pickup collides with an object, check to see if it is a player
    void OnTriggerEnter2D (Collider2D other)
    {
        if (other.CompareTag("Player") && TractorBeam.activeInHierarchy && other.GetComponent<BeamController>().hasItem == false)
        {
            // If so, pickup the object
            Pickup(other);
            FindObjectOfType<AudioManager>().Play("ItemPickup");
        }
    }

    void Pickup(Collider2D player)
    {
        //Debug.Log("Power up picked up");
        // Tell the TractorBeam we have an item and to start it's cooldown
        player.GetComponent<BeamController>().hasItem = true;
        player.GetComponent<BeamController>().StartCooldown();

        // Set the current Item so that it knows what to spawn when we drop an item
        player.GetComponent<BeamController>().currentItem = DropPickup;
        
        /* These lines activates the power on the UFO and set the power on the TractorBeam
         * so that it knows which power to disable on item drop
         */
        player.GetComponent<BeamController>().currentPower = ThisPower;
        ThisPower.SetActive(true);

        //Display the item pickup text
        pickupText.GetComponent<SpriteRenderer>().sprite = pickupNameSprite;
        //Check if it's a new discovery
        int itemID = int.Parse(pickupText.GetComponent<SpriteRenderer>().sprite.name.Substring(10))-2;
        //Debug.Log(itemID);
        if (!player.GetComponent<BeamController>().itemDiscovery[itemID])
        {
            pickupText.transform.Find("ItemSuffix").GetComponent<SpriteRenderer>().enabled = true;
            pickupText.transform.Find("ItemSuffix").GetComponent<SpriteRenderer>().sprite = discoveryText;
            player.GetComponent<BeamController>().itemDiscovery[itemID] = true;
        }
        else
            pickupText.transform.Find("ItemSuffix").GetComponent<SpriteRenderer>().enabled = false;
        Instantiate(pickupText, player.transform.position, Quaternion.Euler(0, 0, 0));

        // Deactivate the pickup
        //ThisPickup.SetActive(false);
        gameObject.SetActive(false);
    }
}
