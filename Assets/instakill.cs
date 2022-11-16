using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class instakill : MonoBehaviour
{
    public Collider2D col;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col){
        Debug.Log("Death");
        AustinEventManager.PlayerDeath();
    }
}
