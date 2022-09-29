using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunnerPower : MonoBehaviour
{
    // variables on Gunner stats
    public int BulletDamage;
    public float CooldownDuration;
    public float BulletSpeed;
    private bool OnCooldown;
    private float BulletDirection;

    private 
    
    // Update is called once per frame
    void Update()
    {
        //Check what direction to shoot
        DirectionDetection();

        // MAKE SURE YOU SET ON COOLDOWN TO FALSE WHEN THE ITEM IS DROPPED TO AVOID BUGS
        if (Input.GetButton("Fire2") && OnCooldown == false){
            Debug.Log("Fire in direction: " + BulletDirection);
            StartCooldown();

        }
    }

    public void DirectionDetection(){
        // Need 8 directions of movement for bullet
            //Left = 1
            if (Input.GetAxis("Horizontal") < 0 && Input.GetAxis("Vertical") == 0){
                //Shoot left
                BulletDirection = 1;
            }

            //LeftUp = 2
            else if (Input.GetAxis("Horizontal") < 0 && Input.GetAxis("Vertical") > 0){
                //Shoot leftUp
                BulletDirection = 2;
            }

            //Up = 3
            else if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") > 0){
                //Shoot Up
                BulletDirection = 3;
            }

            //RightUp = 4
            else if (Input.GetAxis("Horizontal") > 0 && Input.GetAxis("Vertical") > 0){
                //Shoot RightUp
                BulletDirection = 4;
            }

            //Right = 5
            else if (Input.GetAxis("Horizontal") > 0 && Input.GetAxis("Vertical") == 0){
                //Shoot Right
                BulletDirection = 5;
            }

            //RightDown = 6
            else if (Input.GetAxis("Horizontal") < 0 && Input.GetAxis("Vertical") < 0){
                //Shoot RightDown
                BulletDirection = 6;
            }

            //Down = 7
            else if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") < 0){
                //Shoot Down
                BulletDirection = 7;
            }

            //LeftDown = 8
            else if (Input.GetAxis("Horizontal") < 0 && Input.GetAxis("Vertical") < 0){
                //Shoot leftDown
                BulletDirection = 8;
            }
            //if BulletDirection is null, set direction to right
            else if (BulletDirection == null){
                BulletDirection = 5;


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
}
