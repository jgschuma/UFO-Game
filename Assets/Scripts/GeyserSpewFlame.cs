using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GeyserSpewFlame : MonoBehaviour
{
    public GameObject flamePrefab;
    public float flameDelay = 2f;

    private GameObject player;
    private float deactivateDistance = 250f;
    private float distanceToPlayer;
    private Coroutine lastCoroutine;
    
    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(SpewFlame());
        player = GameObject.Find("UFO");
    }

    // Update is called once per frame
    void Update()
    {
/*        player = GameObject.Find("UFO");
        if (player != null)
        {*/
            distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
            if (distanceToPlayer <= deactivateDistance && lastCoroutine == null)
                lastCoroutine = StartCoroutine(SpewFlame());
            else if (distanceToPlayer > deactivateDistance && lastCoroutine != null)
            {
                StopCoroutine(lastCoroutine);
                lastCoroutine = null;
            }
/*        }*/
    }

    IEnumerator SpewFlame()
    {
        yield return new WaitForSeconds(flameDelay);
        //Debug.Log("Dir: " + -transform.rotation.eulerAngles.z);
        flamePrefab.GetComponent<ProjectileDirection>().direction = -transform.rotation.eulerAngles.z;
        Instantiate(flamePrefab, transform.position, Quaternion.Euler(0, 0, 0));
        lastCoroutine = StartCoroutine(SpewFlame());
    }
}
