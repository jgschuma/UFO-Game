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
    public GameObject GunnerFirePoint;
    public GameObject GunnerFirePointRotator;
    public GameObject GunnerBulletPrefab;

    
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
            //BulletInstance.GetComponent<ProjectileDirection>().direction = 90;
            

        }
    }


    public void DirectionDetection(){
        // Need 8 directions of movement for bullet
            //Right = 0 
            if (Input.GetAxis("Horizontal") > 0 && Input.GetAxis("Vertical") == 0){
                //Shoot Right
                GunnerFirePointRotator.transform.localEulerAngles = new Vector3(0, 0, 0);
            }

            //RightUp = 4
            else if (Input.GetAxis("Horizontal") > 0 && Input.GetAxis("Vertical") > 0){
                //Shoot RightUp
                GunnerFirePointRotator.transform.localEulerAngles = new Vector3(0, 0, 45);
            }

            //Up = 3
            else if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") > 0){
                //Shoot Up
                GunnerFirePointRotator.transform.localEulerAngles = new Vector3(0, 0, 90);
            }

            //LeftUp = 2
            else if (Input.GetAxis("Horizontal") < 0 && Input.GetAxis("Vertical") > 0){
                //Shoot leftUp
                GunnerFirePointRotator.transform.localEulerAngles = new Vector3(0, 0, 135);
            }

            //Left = 1
            else if (Input.GetAxis("Horizontal") < 0 && Input.GetAxis("Vertical") == 0){
                //Shoot left
                GunnerFirePointRotator.transform.localEulerAngles = new Vector3(0, 0, 180);
            }

            //LeftDown = 8
            else if (Input.GetAxis("Horizontal") < 0 && Input.GetAxis("Vertical") < 0){
                //Shoot leftDown
                GunnerFirePointRotator.transform.localEulerAngles = new Vector3(0, 0, 225);
            }

            //Down = 7
            else if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") < 0){
                //Shoot Down
                GunnerFirePointRotator.transform.localEulerAngles = new Vector3(0, 0, 270);
            }

            //RightDown = 6
            else if (Input.GetAxis("Horizontal") > 0 && Input.GetAxis("Vertical") < 0){
                //Shoot RightDown
                GunnerFirePointRotator.transform.localEulerAngles = new Vector3(0, 0, 315);
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

