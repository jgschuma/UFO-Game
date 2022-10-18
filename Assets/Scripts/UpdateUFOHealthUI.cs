using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateUFOHealthUI : MonoBehaviour
{
    public Animator healthUI;
    EntityHealth healthUFO;

    void Start()
    {
        healthUFO = GetComponent<EntityHealth>();
    }
    
    // Update is called once per frame
    void Update()
    {
        healthUI.SetInteger("health", healthUFO.health);
    }
}
