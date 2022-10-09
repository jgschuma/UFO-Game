using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public Rigidbody2D ShieldRigidBody;
    public GameObject ShieldPickup;
    public GameObject ShieldPower;
    public GameObject TractorBeam;
    // Start is called before the first frame update
    void Start()
    {
        ShieldRigidBody.freezeRotation = true;
        //ShieldPickup = GameObject.Find("ShieldPickup");
        //ShieldPower = GameObject.Find("ShieldPower");
        TractorBeam = GameObject.Find("TractorBeam");
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
        BeamController beamCont = player.GetComponent<BeamController>();
        Debug.Log("Power up picked up");
        beamCont.hasItem = true;
        beamCont.StartCooldown();
        beamCont.currentItem = GameObject.Find("ShieldPickup");
        beamCont.currentPower = ShieldPower;
        ShieldPower.SetActive(true);
        ShieldPickup.SetActive(false);
    }
}
