﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserLengthManager : MonoBehaviour
{
    public GameObject FirePoint;
    public GameObject LaserLine;
    public GameObject LaserImpact;
    public LayerMask layerMask;
    public float ImpactOffset;
    public float MaxDistance;

    float Distance;
    Ray ray;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Create the raycastHit2D that we'll store our raycast data in
        RaycastHit2D raycastHit2D = Physics2D.Raycast(FirePoint.transform.position, Vector3.up, Mathf.Infinity, layerMask);//layerMask);
        //Physics2D.queriesHitTriggers = false;
        if(raycastHit2D && Vector3.Distance(raycastHit2D.point, FirePoint.transform.position) < MaxDistance){
            // Store the location the raycast hit and move the LaserImpact to the Impact location
            Vector3 HitLocation = raycastHit2D.point;
            LaserImpact.transform.position = HitLocation + new Vector3(0, ImpactOffset, 0);

            
            Distance = Vector3.Distance(FirePoint.transform.position, HitLocation);
            LaserLine.transform.localScale = new Vector3(1, (Distance/16f), 1);

            
            Vector3 middlePoint = (FirePoint.transform.position + HitLocation)/2f;
            LaserLine.transform.position = middlePoint;
        }
        else{
            Vector3 HitLocation = FirePoint.transform.position + new Vector3(0, MaxDistance, 0);
            LaserImpact.transform.position = HitLocation + new Vector3(0, ImpactOffset, 0);

            
            Distance = Vector3.Distance(FirePoint.transform.position, HitLocation);
            LaserLine.transform.localScale = new Vector3(1, (Distance/16f), 1);

            
            Vector3 middlePoint = (FirePoint.transform.position + HitLocation)/2f;
            LaserLine.transform.position = middlePoint;
        }
    }   
}
