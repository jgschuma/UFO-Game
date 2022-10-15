using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public Rigidbody2D ThisRigidBody;
    public GameObject ThisPickup;
    public GameObject UFO;
    private GameObject ThisPower;
    private GameObject TractorBeam;
    public string thisName;
    public string thisPower;
    // Start is called before the first frame update

    // Awake is called when an item is instantiated
    void Awake()
    {
        ThisRigidBody.freezeRotation = true;
        TractorBeam = GameObject.Find("TractorBeam");
        ThisPower = GameObject.Find(thisPower.ToString());
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
        Debug.Log("The active power is " + thisPower);
        ThisPower.SetActive(true);

        Destroy(ThisPickup);
    }
}
