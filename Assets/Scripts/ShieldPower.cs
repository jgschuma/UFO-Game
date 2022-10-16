using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPower : MonoBehaviour
{
    public GameObject ShieldEffect;
    // Start is called before the first frame update
    void Start()
    {
        //ShieldEffect = GameObject.Find("ShieldEffect");
    }

    // Update is called once per frame
    void Update()
    {
        // If the fire button is held down, the shield will appear
        if(Input.GetButton("Fire2")){
            ShieldEffect.SetActive(true);
        }
        else{
            ShieldEffect.SetActive(false);
        }
    }
}
