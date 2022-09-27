using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class ProjectileDirection : MonoBehaviour
{
    public double direction = 0;
    public float speed = 30f;
    private Rigidbody2D proj_Rigidbody;
    Vector3 movement;
    
    // Start is called before the first frame update
    void Start()
    {
        proj_Rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        movement = new Vector3((float)(Math.Sin(ConvertToRadians(direction)) * speed), (float)(Math.Cos(ConvertToRadians(direction)) * speed), 0).normalized;
    }

    void FixedUpdate()
    {
        proj_Rigidbody.MovePosition(transform.position + movement * speed * Time.deltaTime);
    }

    double ConvertToRadians(double _degrees)
    {
        return _degrees * Math.PI / 180;
    }
}
