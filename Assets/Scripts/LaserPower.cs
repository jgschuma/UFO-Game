using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserPower : MonoBehaviour
{
    public GameObject Laser;
    public SpriteRenderer arch1;
    public SpriteRenderer arch2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButton("Fire2")){
            Laser.SetActive(true);
            arch1.enabled = true;
            arch2.enabled = true;
        }
        else{
            Laser.SetActive(false);
            arch1.enabled = false;
            arch2.enabled = false;
        }
    }
}
