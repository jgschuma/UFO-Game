using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserLengthManager : MonoBehaviour
{
    public GameObject FirePoint;
    public GameObject LaserLine;
    public GameObject LaserImpact;
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
        RaycastHit2D raycastHit2D = Physics2D.Raycast(FirePoint.transform.position, Vector3.up);
        if(raycastHit2D){
            Vector3 HitLocation = raycastHit2D.point;
            LaserImpact.transform.position = HitLocation + new Vector3(0, ImpactOffset, 0);

            
            Distance = Vector3.Distance(FirePoint.transform.position, HitLocation);
            LaserLine.transform.localScale = new Vector3(1, (Distance/16f), 1);

            
            Vector3 middlePoint = (FirePoint.transform.position + HitLocation)/2f;
            LaserLine.transform.position = middlePoint;

            // LaserImpact.transform.position = HitLocation;
        }
    }   
}
