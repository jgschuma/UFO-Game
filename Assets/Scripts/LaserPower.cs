using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserPower : MonoBehaviour
{
    public GameObject Laser;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButton("Fire2")){
            Laser.SetActive(true);
        }else{
            Laser.SetActive(false);
        }
    }
}
