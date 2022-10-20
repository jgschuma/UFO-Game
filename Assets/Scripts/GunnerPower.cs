using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GunnerPower : MonoBehaviour
{
    // variables on Gunner stats
    public float CooldownDuration;
    public float BulletSpeed;
    private bool OnCooldown;
    private int BulletDirection;
    public GameObject GunnerFirePoint;
    public GameObject GunnerFirePointRotator;
    public GameObject GunnerBulletPrefab;

    public float bulletOffset = 1f;
    
    void Start()
    {
        BeamController.DeactivatePower += DropPower;
    }

    void Awake()
    {
        BulletDirection = 90;
    }

    // Update is called once per frame
    void Update()
    {
        //Check what direction to shoot
        DirectionDetection();

        // Check to see if the Player is pressing the firebutton and fire the gunner
        if (Input.GetButton("Fire2") && OnCooldown == false){
            Debug.Log("Fire in direction: " + BulletDirection);
            StartCooldown();
            GameObject BulletInstance = Instantiate(GunnerBulletPrefab, GunnerFirePoint.transform.position, Quaternion.identity);
            BulletInstance.GetComponent<ProjectileDirection>().direction = BulletDirection;
            BulletInstance.GetComponent<Animator>().SetInteger("Direction", BulletDirection);
            BulletInstance.GetComponent<ProjectileDirection>().speed = BulletSpeed;
            //Offsets the bullets position slightly to achieve a gattling gun effect
            BulletInstance.transform.position += new Vector3((float)(bulletOffset * Math.Cos(BulletDirection * Math.PI / 180)), (float)(bulletOffset * -Math.Sin(BulletDirection * Math.PI / 180)), 0f);
            bulletOffset *= -1;
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

    /* If the TractorBeam drops an item, we set OnCooldown to false so we
    don't lock ourselves out if the gunner is on cooldown when we drop
    */
    private void DropPower()
    {
        OnCooldown = false;
    }
}

