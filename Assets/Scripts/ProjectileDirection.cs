using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class ProjectileDirection : MonoBehaviour
{
    public double direction = 0;
    public float despawnTime = 3;
    public bool despawn = true;
    public float speed = 1f;
    private Rigidbody2D proj_Rigidbody;
    Vector3 movement;
    
    // Start is called before the first frame update
    void Start()
    {
        proj_Rigidbody = GetComponent<Rigidbody2D>();
        if (despawn)
            StartCoroutine(CountdownTimer());
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

    //Sets speed and direction using an x-vector and a y-vector
    public void setSpeedAndDirection(float _x, float _y)
    {
        speed = (float)Math.Sqrt(Math.Pow(_x, 2) + Math.Pow(_y, 2));
        direction = Math.Atan(_x / _y) * (180 / Math.PI);
        if (_y < 0)
            direction += 180;
    }

    double ConvertToRadians(double _degrees)
    {
        return _degrees * Math.PI / 180;
    }
    
    IEnumerator CountdownTimer()
    {
        yield return new WaitForSeconds(despawnTime);
        Destroy(gameObject);
    }
}
