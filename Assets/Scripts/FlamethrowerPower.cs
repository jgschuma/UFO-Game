using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FlamethrowerPower : MonoBehaviour
{
    public GameObject FirePoint;
    public GameObject FirePointRotator;
    public PlayerController playCon;

    private bool OnCooldown;

    public float CooldownDuration;
    public float FirePointRotation;
    public float MaxRotateAngle;
    public float RotateSpeed;
    private float MaxRotateLeft;

    // These variables are related to defining the projectile
    public GameObject FirePrefab;
    public float MoveSpeed;
    public float DespawnTime;



    void Start(){
        // Allow DropPower to listen to the DeactivatePower event found in TractorBeam
        BeamController.DeactivatePower += DropPower;

        // Sets the maxAngle for the right for neater code
        MaxRotateLeft = 360 - MaxRotateAngle;

        OnCooldown = false;
    }
    // Update is called once per frame
    void Update()
    {
        ChangeFirepoint();
        if (Input.GetButton("Fire2") && (OnCooldown == false)){
            StartCooldown();

            GameObject FlameInstance = Instantiate(FirePrefab, FirePoint.transform.position, Quaternion.identity);
            FlameInstance.GetComponent<ProjectileDirection>().direction = 180 - FirePointRotation;
            FlameInstance.GetComponent<ProjectileDirection>().speed = MoveSpeed + playCon.GetSpeed();
            FlameInstance.GetComponent<ProjectileDirection>().despawnTime = DespawnTime;
        }
    }

    void ChangeFirepoint()
    {
        // Obtain the angle of the FirePoint for easy comparing
        FirePointRotation = FirePoint.transform.eulerAngles.z;
        float horizInput = Input.GetAxis("Horizontal");

        // Check to see if the FirePoints rotation is outside the allowed bounds
        if ((FirePointRotation > MaxRotateAngle) && (FirePointRotation < (MaxRotateLeft)))
        {
            // If outside bounds, check if Rotation is closer to right or left MaxRotatAngle
            if ((FirePointRotation - MaxRotateAngle) < ((MaxRotateLeft) - FirePointRotation)){
                // If closer to right set angle to the right
                FirePointRotator.transform.eulerAngles = new Vector3(0, 0, MaxRotateAngle);
            }
            // Otherwise set it closer to the left
            else{
                FirePointRotator.transform.eulerAngles = new Vector3(0, 0, (MaxRotateLeft));
            }
        }

        // When Right is being held, rotate FirePointRotator to the right
        if (horizInput > 0)
        {
            FirePointRotator.transform.Rotate(0, 0, RotateSpeed *Time.deltaTime);
        }
        // When Left is bein held, rotate FirePointRotator the the left
        else if (horizInput < 0 )
        {
            FirePointRotator.transform.Rotate(0, 0, -RotateSpeed *Time.deltaTime);
        }
    }

    public IEnumerator FlamethrowerCooldown(){
        OnCooldown = true;

        yield return new WaitForSeconds(CooldownDuration);

        OnCooldown = false;
    }

    public void StartCooldown(){
        StartCoroutine(FlamethrowerCooldown());
    }

    // When the tractorBeam drops its item, set the rotation back to zero
    void DropPower(){
        FirePointRotator.transform.eulerAngles = new Vector3(0, 0, 0);
    }
}
