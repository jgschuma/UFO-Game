using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPower : MonoBehaviour
{
    public GameObject ShieldEffect;
    public GameObject UFO;
    //public EntityHealth entHeal;

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
            UFO.GetComponent<EntityHealth>().invincible = true;
        }
        else{
            ShieldEffect.SetActive(false);
            UFO.GetComponent<EntityHealth>().invincible = false;
        }
    }
}
