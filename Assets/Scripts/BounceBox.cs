using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BounceBox : MonoBehaviour
{
    public float recoil = 0.5f;
/
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
            if (contact.y == 0)
                normalAngle = Math.PI / 2 * Math.Sign(contact.x);
            else
                normalAngle = Math.Atan(contact.y / contact.x) + Math.PI / 2;
            Debug.Log("Normal: " + (normalAngle * 180 / Math.PI));
            Vector2 velocity = other.GetContact(0).relativeVelocity;
            //It's the player
            if (GetComponent<GetControllerInput>() != null && GetComponent<GetControllerInput>().enabled)
            {
                playerAngle = playCon.GetDirectionInRadians();
                Debug.Log("Player: " + playCon.GetDirectionInDegrees());
                newAngle = normalAngle - (playerAngle+Math.PI-normalAngle) % (Math.PI*2);
                Debug.Log("New a: " + (newAngle * 180 / Math.PI));
                //FINISH THIS SEGMENT
                Debug.Log("New x: " + playCon.movement.x * Math.Sin(newAngle));
                Debug.Log("New y: " + playCon.movement.y * Math.Cos(newAngle));
                //END OF UNFINISHED SEGMENT
            }
            //It's probably the twister
            else if (projDir != null && projDir.enabled)
            {
                Debug.Log(projDir.direction);
                newAngle = normalAngle - (projDir.GetDirectionInRadians() + Math.PI - normalAngle) % (Math.PI * 2);
                Debug.Log("New a: " + (newAngle * 180 / Math.PI));
                projDir.SetSpeedAndDirection(projDir.speed, (newAngle * 180 / Math.PI));
            }
        }
    }
}
