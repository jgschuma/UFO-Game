using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlamethrowerPower : MonoBehaviour
{
    public GameObject FirePoint;
    public GameObject FirePointRotator;
    public GameObject FirePrefab;
    public float FirePointRotation;
    public float MaxRotateAngle;
    public float RotateSpeed;


    // Update is called once per frame
    void Update()
    {
        ChangeFirepoint();
    }

    void ChangeFirepoint()
    {
        FirePointRotation = FirePoint.transform.rotation.z;
        float horizInput = Input.GetAxis("Horizontal");
        if (horizInput > 0 && FirePointRotation < MaxRotateAngle)
        {
            FirePointRotator.transform.Rotate(0, 0, RotateSpeed *Time.deltaTime);
        }
        else if (horizInput < 0 && FirePointRotation < MaxRotateAngle)
        {
            FirePointRotator.transform.Rotate(0, 0, -RotateSpeed *Time.deltaTime);
        }
    }
}
