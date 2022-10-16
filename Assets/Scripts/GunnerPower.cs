using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GunnerPower : MonoBehaviour
{
    // variables on Gunner stats
    public int BulletDamage;
    public float CooldownDuration;
    public float BulletSpeed;
    private bool OnCooldown;
    private float BulletDirection;
    public GameObject GunnerFirePoint;
    public GameObject GunnerFirePointRotator;
    public GameObject GunnerBulletPrefab;

    
    void Start()
    {
        BeamController.DeactivatePower += DropPower;
    }

    // Update is called once per frame
    void Update()
    {
        //Check what direction to shoot
        DirectionDetection();

        // MAKE SURE YOU SET ON COOLDOWN TO FALSE WHEN THE ITEM IS DROPPED TO AVOID BUGS
        if (Input.GetButton("Fire2") && OnCooldown == false){
            Debug.Log("Fire in direction: " );
            StartCooldown();
            //GunnerBulletPrefab.direction = GunnerFirePoint.transform.rotation.z;
            var BulletInstance = Instantiate(GunnerBulletPrefab, GunnerFirePoint.transform.position, GunnerFirePointRotator.transform.rotation);
            BulletInstance.GetComponent<ProjectileDirection>().direction = BulletDirection;
        }
    }


    public void DirectionDetection(){
        // Need 8 directions of movement for bullet
            //Right
            if (Input.GetAxis("Horizontal") > 0 && Input.GetAxis("Vertical") == 0){
                // Shoot Right
                GunnerFirePointRotator.transform.localEulerAngles = new Vector3(0, 0, 0);
                BulletDirection = 90;
            }

            //RightUp
            else if (Input.GetAxis("Horizontal") > 0 && Input.GetAxis("Vertical") > 0){
                // Shoot RightUp
                GunnerFirePointRotator.transform.localEulerAngles = new Vector3(0, 0, 45);
                BulletDirection = 45;
            }

            //Up
            else if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") > 0){
                // Shoot UP
                GunnerFirePointRotator.transform.localEulerAngles = new Vector3(0, 0, 90);
                BulletDirection = 0;
                
            }

            //LeftUp 
            else if (Input.GetAxis("Horizontal") < 0 && Input.GetAxis("Vertical") > 0){
                //Shoot leftUp
                GunnerFirePointRotator.transform.localEulerAngles = new Vector3(0, 0, 135);
                BulletDirection = 315;
            }  

            //Left
            else if (Input.GetAxis("Horizontal") < 0 && Input.GetAxis("Vertical") == 0){
                //Shoot left
                GunnerFirePointRotator.transform.localEulerAngles = new Vector3(0, 0, 180);
                BulletDirection = 270;
            }

            //LeftDown
            else if (Input.GetAxis("Horizontal") < 0 && Input.GetAxis("Vertical") < 0){
                //Shoot leftDown
                GunnerFirePointRotator.transform.localEulerAngles = new Vector3(0, 0, 225);
                BulletDirection = 225;
            }

            //Down
            else if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") < 0){
                //Shoot Down
                GunnerFirePointRotator.transform.localEulerAngles = new Vector3(0, 0, 270);
                BulletDirection = 180;
            }

            //RightDown
            else if (Input.GetAxis("Horizontal") > 0 && Input.GetAxis("Vertical") < 0){
                //Shoot RightDown
                GunnerFirePointRotator.transform.localEulerAngles = new Vector3(0, 0, 315);
                BulletDirection = 135;
            }
    }
    

    public IEnumerator GunnerCooldown(){
        OnCooldown = true;

        yield return new WaitForSeconds(CooldownDuration);

        OnCooldown = false;
    }

    public void StartCooldown(){
        StartCoroutine(GunnerCooldown());
    }

    private void DropPower()
    {
        OnCooldown = false;
    }
}

