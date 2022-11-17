using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BounceBox : MonoBehaviour
{
    public float recoil = 0.5f;

    PlayerController playCon;
    ProjectileDirection projDir;
    
    // Start is called before the first frame update
    void Start()
    {
        playCon = GetComponent<PlayerController>();
        projDir = GetComponent<ProjectileDirection>();
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Terrain")
        {
            double normalAngle, playerAngle, newAngle;

            Vector2 contact = other.GetContact(0).normal;
            //Debug.Log("Contact: " + contact);
            normalAngle = Math.Atan(contact.x / contact.y);
            if (contact.y < 0 || (contact.y == 0 && contact.x < 0))
                normalAngle += Math.PI;
            //Debug.Log("Normal: " + (normalAngle * 180 / Math.PI));

            //It's the player
            if (GetComponent<GetControllerInput>() != null && GetComponent<GetControllerInput>().enabled)
            {
                playerAngle = playCon.GetDirectionInRadians();
                //Debug.Log("Curr a: " + playerAngle * 180 / Math.PI);
                if (NinetyDegreeTest(normalAngle, playerAngle))
                {
                    newAngle = normalAngle - (playerAngle + Math.PI - normalAngle) % (Math.PI * 2);
                    playCon.xSpeed = (float)(Math.Abs(playCon.xSpeed) * Math.Sin(newAngle)) * recoil;
                    playCon.ySpeed = (float)(Math.Abs(playCon.ySpeed) * Math.Cos(newAngle)) * recoil;
            }
        }
            //It's probably the twister
            else if (projDir != null && projDir.enabled)
            {
                //Debug.Log("Curr a: " + projDir.direction);
                if (NinetyDegreeTest(normalAngle, projDir.direction * Math.PI / 180))
                {
                    newAngle = normalAngle - (projDir.GetDirectionInRadians() + Math.PI - normalAngle) % (Math.PI * 2);
                    //Debug.Log("New a: " + (newAngle * 180 / Math.PI));
                    projDir.SetSpeedAndDirection(projDir.speed, (newAngle * 180 / Math.PI));
                }
            }
        }
    }

    /*Verifies that a decision is valid by comparing the angle of the player vs the 
     * normal of the surface.If the difference in the angle is 90 degrees or less,
     * return false to stop the redirection from proceeding*/
    private bool NinetyDegreeTest(double _angle1, double _angle2)
    {
        double angleDifference = Math.Abs(_angle1 - _angle2) % (Math.PI*2);
        angleDifference = Math.Min(angleDifference, (Math.PI * 2) - angleDifference);
        //Debug.Log("Angle difference: " + angleDifference * 180 / Math.PI);
        if (angleDifference > Math.PI/2)
        {
            //Debug.Log("RETURN TRUE");
            return true;
        }
        //Debug.Log("RETURN FALSE");
        return false;
    }
}
