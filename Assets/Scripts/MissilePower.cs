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
    private bool HasMissile = false;
    

    void Start(){
        GuidedMissileController.MissileCollision += OnDestroy;
        HasMissile = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire2") && HasMissile == false)
        {
            LiveMissile = Instantiate(MissilePrefab, MissileSpawnPoint.transform.position, MissileSpawnPoint.transform.rotation);
            HasMissile = true;
        }
        else if (HasMissile == true){
            MainCamera.transform.position = LiveMissile.transform.position + new Vector3(0, 0, -10);
        }
    }

    void OnDestroy(){
        HasMissile = false;
        MainCamera.transform.position = UFO.transform.position + new Vector3(0, 0, -10);
    }
}
