using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserLengthManager : MonoBehaviour
{
    public GameObject FirePoint;
    public GameObject LaserLine;
    public GameObject LaserImpact;
    public LayerMask layerMask;
    public float ImpactOffset;
    private Vector3 ImpactPosition;

    float Distance;
    Ray ray;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Create the raycastHit2D that we'll store our raycast data in
        RaycastHit2D raycastHit2D = Physics2D.Raycast(FirePoint.transform.position, Vector3.up, layerMask);
        //Physics2D.queriesHitTriggers = false;
        if(raycastHit2D){
            // Store the location the raycast hit and move the LaserImpact to the Impact location
            Vector3 HitLocation = raycastHit2D.point;
            LaserImpact.transform.position = HitLocation + new Vector3(0, ImpactOffset, 0);

            
            Distance = Vector3.Distance(FirePoint.transform.position, HitLocation);
            LaserLine.transform.localScale = new Vector3(1, (Distance/16f), 1);

            
            Vector3 middlePoint = (FirePoint.transform.position + HitLocation)/2f;
            LaserLine.transform.position = middlePoint;

            // Here we can check to see if an enemy was hit and we can have them take damage
                // Do the damage check
        }
    }   
}
