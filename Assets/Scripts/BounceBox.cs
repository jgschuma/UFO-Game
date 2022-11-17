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
            Debug.Log("Contact: " + contact);
            normalAngle = Math.Atan(contact.x / contact.y);
            if (contact.y < 0 || (contact.y == 0 && contact.x < 0))
                normalAngle += Math.PI;
            Debug.Log("Normal: " + (normalAngle * 180 / Math.PI));
            //Vector2 velocity = other.GetContact(0).relativeVelocity;
            //It's the player
            if (GetComponent<GetControllerInput>() != null && GetComponent<GetControllerInput>().enabled)
            {
                playerAngle = playCon.GetDirectionInRadians();
                newAngle = normalAngle - (playerAngle + Math.PI - normalAngle) % (Math.PI * 2);
                playCon.xSpeed = (float)(Math.Abs(playCon.xSpeed) * Math.Sin(newAngle)) * recoil;
                playCon.ySpeed = (float)(Math.Abs(playCon.ySpeed) * Math.Cos(newAngle)) * recoil;
            }
            //It's probably the twister
            else if (projDir != null && projDir.enabled)
            {
                Debug.Log("Curr a: " + projDir.direction);
                newAngle = normalAngle - (projDir.GetDirectionInRadians() + Math.PI - normalAngle) % (Math.PI * 2);
                Debug.Log("New a: " + (newAngle * 180 / Math.PI));
                projDir.SetSpeedAndDirection(projDir.speed, (newAngle * 180 / Math.PI));
            }
        }
    }
}
