using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class ProjectileDirection : MonoBehaviour
{
    public double direction = 0;
    public float despawnTime = 3;
    public bool despawn = true;
    public bool hasDestroyAnimation = false;
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
        movement = new Vector3((float)(Math.Sin(GetDirectionInRadians()) * speed), (float)(Math.Cos(GetDirectionInRadians()) * speed), 0).normalized;
    }

    void FixedUpdate()
    {
        proj_Rigidbody.MovePosition(transform.position + movement * speed * Time.deltaTime);
    }

    public void SetDirectionInRadians(double _r)
    {
        direction = _r * 180 / Math.PI;
    }

    //Sets speed and direction using an x-vector and a y-vector
    public void SetXAndYSpeed(float _x, float _y)
    {
        speed = (float)Math.Sqrt(Math.Pow(_x, 2) + Math.Pow(_y, 2));
        SetDirectionInRadians(Math.Atan(_x / _y));
        if (_y < 0)
            direction += 180;
    }

    public void SetSpeedAndDirection(float _s, double _d)
    {
        speed = _s;
        direction = _d;
    }

    public double GetDirectionInRadians()
    {
        return direction * Math.PI / 180;
    }
    
    IEnumerator CountdownTimer()
    {
        yield return new WaitForSeconds(despawnTime);
        if (hasDestroyAnimation)
        {
            GetComponent<Animator>().SetTrigger("Destroy");
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;
        }
        else
            Destroy(gameObject);
    }
}
