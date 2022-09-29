using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunnerPickupScript : MonoBehaviour
{
    public Rigidbody2D GunnerRigidBody;
    public GameObject GunnerPickup;
    public GameObject GunnerPower;
    public GameObject TractorBeam;
    public string thisName = "GunnerPickup";
    
    // Start is called before the first frame update
    void Start()
    {
        GunnerRigidBody.freezeRotation = true;
    }

    void OnTriggerEnter2D(Collider2D other){
        if (other.CompareTag("Player") && TractorBeam.activeInHierarchy){
            Pickup(other);
        }
    }

    void Pickup(Collider2D player){
        Debug.Log("Power up picked up");
        player.GetComponent<BeamController>().hasItem = true;
        player.GetComponent<BeamController>().StartCooldown();
        player.GetComponent<BeamController>().currentItem = GameObject.Find("GunnerPickup");
        player.GetComponent<BeamController>().currentPower = GunnerPower;
        GunnerPower.SetActive(true);

        GunnerPickup.SetActive(false);
    }
    
}
