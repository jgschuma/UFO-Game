using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ItemManager : MonoBehaviour
{
    public GameObject Gunner;
    public GameObject Missile;
    public GameObject Shield;
    public GameObject Twister;
    // Start is called before the first frame update
    void Start()
    {
        ItemPickup.OnPickup += PowerEnabler;
    }

    private void PowerEnabler(string thisName)
    {
        Debug.Log("The item just picked up was " + thisName);
    }
}
