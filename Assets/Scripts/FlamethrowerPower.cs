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
    private Animator anim;

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

        anim = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        ChangeFirepoint();
        if (Input.GetButton("Fire2") && (OnCooldown == false)){
            StartCooldown();
            FindObjectOfType<AudioManager>().PlayInteractable("Flamethrower");

            GameObject FlameInstance = Instantiate(FirePrefab, FirePoint.transform.position, Quaternion.identity);
            FlameInstance.GetComponent<ProjectileDirection>().direction = 180 - FirePointRotation;
            FlameInstance.GetComponent<ProjectileDirection>().speed = MoveSpeed + playCon.GetSpeed();
            FlameInstance.GetComponent<ProjectileDirection>().despawnTime = DespawnTime;
        } else if (!Input.GetButton("Fire2")) {
            FindObjectOfType<AudioManager>().StopInteractable("Flamethrower");
        }
    }

    void ChangeFirepoint()
    {
        // Obtain the angle of the FirePoint for easy comparing
        //FirePointRotation = FirePoint.transform.eulerAngles.z;
        float horizInput = Input.GetAxis("Horizontal");
        FirePointRotation += (RotateSpeed * Time.deltaTime) * horizInput;
        if (FirePointRotation > MaxRotateAngle)
            FirePointRotation = MaxRotateAngle;
        else if (FirePointRotation < -MaxRotateAngle)
            FirePointRotation = -MaxRotateAngle;

        //Animate the flamethrower nozzle
        if (Math.Abs(FirePointRotation) < 7.5)
            anim.SetInteger("direction", 0);
        else if (Math.Abs(FirePointRotation) < 22.5)
            anim.SetInteger("direction", 15);
        else if (Math.Abs(FirePointRotation) < 37.5)
            anim.SetInteger("direction", 30);
        else if (Math.Abs(FirePointRotation) < 52.5)
            anim.SetInteger("direction", 45);
        else if (Math.Abs(FirePointRotation) < 67.5)
            anim.SetInteger("direction", 60);
        else if (Math.Abs(FirePointRotation) < 82.5)
            anim.SetInteger("direction", 75);
        else
            anim.SetInteger("direction", 90);

        if (FirePointRotation > -7.5)
            anim.SetInteger("direction", anim.GetInteger("direction") * -1);

        //Rotate FirePoint to tip of nozzle
        FirePoint.transform.localPosition = new Vector3(-7f * (float)Math.Sin((FirePointRotation+180) * Math.PI / 180), 7f * (float)Math.Cos((FirePointRotation+180) * Math.PI / 180), 0f);
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
