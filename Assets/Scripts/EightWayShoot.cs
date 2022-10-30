using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EightWayShoot : MonoBehaviour
{
    // variables on projectile stats
    [Tooltip("How many seconds between firing")]
    public float CooldownDuration;
    public float ProjectileSpeed;
    private bool OnCooldown;
    private int ProjectileDirection;
    [Tooltip("Where does the projectile instantiate")]
    public GameObject ShootFirePoint;
    [Tooltip("The GameObject Rotating our firePoint (usually it's parent)")]
    public GameObject ShootFirePointRotator;
    [Tooltip("The prefab to instantiate")]
    public GameObject ProjectilePrefab;

    public float ProjectileOffset = 1f;
    
    void Start()
    {
        // Allows DropPower to listen to the DeactivatePower Event
        BeamController.DeactivatePower += DropPower;
    }

    void Awake()
    {
        // Set ProjectileDirection to the right to match firepoint direction default
        ProjectileDirection = 90;
    }

    // Update is called once per frame
    void Update()
    {
        //Check what direction to shoot
        DirectionDetection();

        // Check to see if the Player is pressing the firebutton and fire the projectile
        if (Input.GetButton("Fire2") && OnCooldown == false){
            // Stop the power from firing until the cooldown has finished
            StartCooldown();
            // Spawn a Projectile then define it's direction, Rotation, and Speed respectively
            GameObject ProjectileInstance = Instantiate(ProjectilePrefab, ShootFirePoint.transform.position, Quaternion.identity);
            ProjectileInstance.GetComponent<ProjectileDirection>().direction = ProjectileDirection;

            // The gunner power's bullets require an animation change to rotate, others do not need this. Prevents warnings.
            if(string.Equals("GunnerPower", this.gameObject.name)){
                ProjectileInstance.GetComponent<Animator>().SetInteger("Direction", ProjectileDirection);
            }
            ProjectileInstance.GetComponent<ProjectileDirection>().speed = ProjectileSpeed;
            
            //Offsets the Projectile position slightly to achieve a gattling gun effect
            ProjectileInstance.transform.position += new Vector3((float)(ProjectileOffset * Math.Cos(ProjectileDirection * Math.PI / 180)), (float)(ProjectileOffset * -Math.Sin(ProjectileDirection * Math.PI / 180)), 0f);
            ProjectileOffset *= -1;
        }
    }


    public void DirectionDetection(){
        // Need 8 directions of movement for Projectile
            //Right
            if (Input.GetAxis("Horizontal") > 0 && Input.GetAxis("Vertical") == 0){
                // Shoot Right
                ShootFirePointRotator.transform.localEulerAngles = new Vector3(0, 0, 0);
                ProjectileDirection = 90;
            }

            //RightUp
            else if (Input.GetAxis("Horizontal") > 0 && Input.GetAxis("Vertical") > 0){
                // Shoot RightUp
                ShootFirePointRotator.transform.localEulerAngles = new Vector3(0, 0, 45);
                ProjectileDirection = 45;
            }

            //Up
            else if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") > 0){
                // Shoot UP
                ShootFirePointRotator.transform.localEulerAngles = new Vector3(0, 0, 90);
                ProjectileDirection = 0;                
            }

            //LeftUp 
            else if (Input.GetAxis("Horizontal") < 0 && Input.GetAxis("Vertical") > 0){
                //Shoot leftUp
                ShootFirePointRotator.transform.localEulerAngles = new Vector3(0, 0, 135);
                ProjectileDirection = 315;
            }  

            //Left
            else if (Input.GetAxis("Horizontal") < 0 && Input.GetAxis("Vertical") == 0){
                //Shoot left
                ShootFirePointRotator.transform.localEulerAngles = new Vector3(0, 0, 180);
                ProjectileDirection = 270;
            }

            //LeftDown
            else if (Input.GetAxis("Horizontal") < 0 && Input.GetAxis("Vertical") < 0){
                //Shoot leftDown
                ShootFirePointRotator.transform.localEulerAngles = new Vector3(0, 0, 225);
                ProjectileDirection = 225;
            }

            //Down
            else if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") < 0){
                //Shoot Down
                ShootFirePointRotator.transform.localEulerAngles = new Vector3(0, 0, 270);
                ProjectileDirection = 180;
            }

            //RightDown
            else if (Input.GetAxis("Horizontal") > 0 && Input.GetAxis("Vertical") < 0){
                //Shoot RightDown
                ShootFirePointRotator.transform.localEulerAngles = new Vector3(0, 0, 315);
                ProjectileDirection = 135;
            }
    }
    

    public IEnumerator ShootCooldown(){
        OnCooldown = true;

        yield return new WaitForSeconds(CooldownDuration);

        OnCooldown = false;
    }

    public void StartCooldown(){
        StartCoroutine(ShootCooldown());
    }

    /* If the TractorBeam drops an item, we set OnCooldown to false so we
    don't lock ourselves out if the item is on cooldown when we drop it
    */
    private void DropPower()
    {
        OnCooldown = false;
    }
}
