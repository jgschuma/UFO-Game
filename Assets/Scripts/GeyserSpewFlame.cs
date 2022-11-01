using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GeyserSpewFlame : MonoBehaviour
{
    public GameObject flamePrefab;
    public float flameDelay = 2f;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpewFlame());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpewFlame()
    {
        yield return new WaitForSeconds(flameDelay);
        //Debug.Log("Dir: " + -transform.rotation.eulerAngles.z);
        flamePrefab.GetComponent<ProjectileDirection>().direction = -transform.rotation.eulerAngles.z;
        Instantiate(flamePrefab, transform.position, Quaternion.Euler(0, 0, 0));
        StartCoroutine(SpewFlame());
    }
}
