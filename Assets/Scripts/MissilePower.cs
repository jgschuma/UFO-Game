using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MissilePower : MonoBehaviour
{
    public GameObject MissilePrefab;
    public GameObject MissileSpawnPoint;
    public GameObject MainCamera;
    public GameObject UFO;
    private GameObject LiveMissile;
    public Animator satelliteDish;
    private bool HasMissile;
    public float CameraHangTime;

    public float BufferTime;
    private bool IsBuffer;

    
    public float CooldownTime;
    public bool OnCooldown;
    

    void Start(){
        GuidedMissileController.MissileCollision += OnDestroy;
        BeamController.DeactivatePower += DropPower;
        HasMissile = false;
        IsBuffer = false;
        OnCooldown = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Check if a missile exists and if we are on cooldown
        if (UFO.GetComponent<GetControllerInput>().GetButtonDown("Fire2") && HasMissile == false && OnCooldown == false)
        {
            LiveMissile = Instantiate(MissilePrefab, MissileSpawnPoint.transform.position, MissileSpawnPoint.transform.rotation);
            HasMissile = true;
            // Prevent the user from blowing up the missile immediately
            StartCoroutine(BlowUpBuffer());
            DisableUFOControls();
        }
        else if (HasMissile == true){
            MainCamera.transform.position = LiveMissile.transform.position + new Vector3(0, 0, -10);
            //If UFO is hurt while controlling missile, restore control to UFO and disable controls to missile
            if (UFO.GetComponent<Animator>().GetBool("hurt"))
            {
                RestoreUFOControls();
                LiveMissile.GetComponent<GetControllerInput>().enabled = false;
            }
            else if (UFO.GetComponent<GetControllerInput>().GetButtonDown("Fire2") && IsBuffer == false)
            {
                LiveMissile.GetComponent<GuidedMissileController>().RemoteDetonate();
            }
        }
    }

    void OnDestroy(){
        FindObjectOfType<AudioManager>().Play("Explosion");
        HasMissile = false;
        CameraWait();
    }

    // The camera will hang for a bit after the missile blows up so the user can see the effect
    // Cooldown is set here because we don't want the cooldown to come off before the hang time
    public IEnumerator CameraHang(){
        OnCooldown = true;
        yield return new WaitForSeconds(CameraHangTime);
        RestoreUFOControls();
        StartCoroutine(Cooldown());
    }

    public void CameraWait(){
        StartCoroutine(CameraHang());
    }

    public IEnumerator BlowUpBuffer(){
        IsBuffer = true;
        yield return new WaitForSeconds(BufferTime);
        IsBuffer = false;
    }

    public IEnumerator Cooldown(){

        yield return new WaitForSeconds(CooldownTime);

        OnCooldown = false;
    }


    /* If the TractorBeam drops an item, we set OnCooldown to false so we
    don't lock ourselves out if the missile is on cooldown when we drop it
    */
    private void DropPower()
    {
        OnCooldown = false;
    }

    void DisableUFOControls()
    {
        UFO.GetComponent<GetControllerInput>().ResetDirections();
        UFO.GetComponent<BeamController>().enabled = false;
        UFO.GetComponent<GetControllerInput>().enabled = false;
        satelliteDish.SetBool("missileActive", true);
    }
    void RestoreUFOControls()
    {
        MainCamera.transform.position = UFO.transform.position + new Vector3(0, 0, -10);
        UFO.GetComponent<BeamController>().enabled = true;
        UFO.GetComponent<GetControllerInput>().enabled = true;
        satelliteDish.SetBool("missileActive", false);
    }
}
