using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFOCollisionController : MonoBehaviour
{
    public GameObject TractorBeam;
    // Start is called before the first frame update
    void Start()
    {
        // If desired, uncomment this line to prevent the player from interacting with objects on the PlayerAttack Layer
        //Physics2D.IgnoreLayerCollision(14,17, true);
    }

    // Update is called once per frame
    void Update()
    {
        // If the player is not using the tractorBeam, disable collisions with item pickups
        // There might be a better solution for this, so I will be looking into it.
        if(TractorBeam.activeInHierarchy == false){
            Physics2D.IgnoreLayerCollision(14,15, true);
        }
        else{
            Physics2D.IgnoreLayerCollision(14,15, false);
        }
    }
}
