using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHoleProjectile : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.GetComponent<Rigidbody2D>().freezeRotation = true;
        Physics2D.IgnoreLayerCollision(17,14, true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
