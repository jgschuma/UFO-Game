using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamController : MonoBehaviour
{
    /*These variables allow for the use of the tractorBeam
    game object*/
    public GameObject tractorBeam;
    public bool hasItem;
    public bool beamOnCooldown;
    public float beamCoolDuration;
    public GameObject currentItem;
    public GameObject ItemSpawn;
    public GameObject currentPower;
    // Start is called before the first frame update
    void Start()
    {
        tractorBeam.SetActive(false);
        hasItem = false;
    }

    // Update is called once per frame
    void Update()
    {
        // calls manager for the tractor beam
        tractorBeamController();
    }

    // This method controls the TractorBeam game object
    public void tractorBeamController()
    {
        // If key is pressed and no item is held, activate the tractorbeam
        if (Input.GetButton("Fire1") && hasItem == false && beamOnCooldown == false)
        {
            tractorBeam.SetActive(true);
        } // If key is pressed and an Item is held, drop the item
        else if (Input.GetButton("Fire1") && hasItem == true && beamOnCooldown == false){
            if(currentPower != null){
                currentPower.SetActive(false);
            }
            currentItem.SetActive(true);
            currentItem.transform.position = ItemSpawn.transform.position;
            Debug.Log("Item has been dropped");
            hasItem = false;
            StartCooldown();
         // Else keep the tractor beam off
        }else
            tractorBeam.SetActive(false);
    }

    public IEnumerator BeamCooldown(){
        beamOnCooldown = true;

        yield return new WaitForSeconds(beamCoolDuration);

        beamOnCooldown = false;
    }

    public void StartCooldown(){
        StartCoroutine(BeamCooldown());
    }
}
