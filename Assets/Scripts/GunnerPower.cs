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
    public GameObject GunnerBulletPrefab;
    private Animator gunnerUnitAnim;

    public float bulletOffset = 1f;
    
    void Start()
    {
        // Allow DropPower to listen for the DeactivatePower event
        BeamController.DeactivatePower += DropPower;
        gunnerUnitAnim = transform.Find("FirePoint").GetComponent<Animator>();
    }

    void Awake()
    {
        // Set the Bullet direction to be the same as the default fire direction.
        BulletDirection = 90;
    }

    // Update is called once per frame
    void Update()
    {
        //Check what direction to shoot
        DirectionDetection();

        // Check to see if the Player is pressing the firebutton and fire the gunner
        if (Input.GetButton("Fire2") && OnCooldown == false){
            // Stop the gun from firing until the cooldown has finished
            StartCooldown();
            // Spawn a bullet then define it's direction, Rotation, and Speed respectively
            GameObject BulletInstance = Instantiate(GunnerBulletPrefab, GunnerFirePoint.transform.position, Quaternion.identity);
            BulletInstance.GetComponent<ProjectileDirection>().direction = BulletDirection;
            BulletInstance.GetComponent<Animator>().SetInteger("Direction", BulletDirection);
            BulletInstance.GetComponent<ProjectileDirection>().speed = BulletSpeed;
            //Offsets the bullets position slightly to achieve a gattling gun effect
            BulletInstance.transform.position += new Vector3((float)(bulletOffset * Math.Cos(BulletDirection * Math.PI / 180)), (float)(bulletOffset * -Math.Sin(BulletDirection * Math.PI / 180)), 0f);
            bulletOffset *= -1;
            FindObjectOfType<AudioManager>().PlayOverlapping("GunnerShooting");
        }
    }


    public void DirectionDetection(){
        // Need 8 directions of movement for bullet
        //Right
        if (Input.GetAxis("Horizontal") > 0 && Input.GetAxis("Vertical") == 0){
            // Shoot Right
            BulletDirection = 90;
        }
            
        //RightUp
        else if (Input.GetAxis("Horizontal") > 0 && Input.GetAxis("Vertical") > 0){
            // Shoot RightUp
            BulletDirection = 45;
        }

        //Up
        else if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") > 0){
            // Shoot UP
            BulletDirection = 0;                
        }

        //LeftUp 
        else if (Input.GetAxis("Horizontal") < 0 && Input.GetAxis("Vertical") > 0){
            //Shoot leftUp
            BulletDirection = 315;
        }  

        //Left
        else if (Input.GetAxis("Horizontal") < 0 && Input.GetAxis("Vertical") == 0){
            //Shoot left
            BulletDirection = 270;
        }

        //LeftDown
        else if (Input.GetAxis("Horizontal") < 0 && Input.GetAxis("Vertical") < 0){
            //Shoot leftDown
            BulletDirection = 225;
        }

        //Down
        else if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") < 0){
            //Shoot Down
            BulletDirection = 180;
        }

        //RightDown
        else if (Input.GetAxis("Horizontal") > 0 && Input.GetAxis("Vertical") < 0){
            //Shoot RightDown
            BulletDirection = 135;
        }

        //Update gunner unit direction
        if(Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            GunnerFirePoint.transform.localPosition = new Vector3(8f * (float)Math.Sin(BulletDirection * Math.PI / 180), 8f * (float)Math.Cos(BulletDirection * Math.PI / 180), 0f);
            gunnerUnitAnim.SetInteger("direction", BulletDirection);
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
    don't lock ourselves out if the gunner is on cooldown when we drop it
    */
    private void DropPower()
    {
        OnCooldown = false;
    }
}

