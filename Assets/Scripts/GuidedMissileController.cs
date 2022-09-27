using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuidedMissileController : MonoBehaviour
{
    private GetControllerInput contInput;
    private ProjectileDirection projDirect;
    private Animator anim;
    private float horiInput, vertiInput;
    // Start is called before the first frame update
    void Start()
    {
        contInput = GetComponent<GetControllerInput>();
        projDirect = GetComponent<ProjectileDirection>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        horiInput = contInput.horizontalInput;
        vertiInput = contInput.verticalInput;

        //Change direction of missile based on input
        if (horiInput == 0 && vertiInput > 0)
            projDirect.direction = 0;
        else if (horiInput > 0 && vertiInput > 0)
            projDirect.direction = 45;
        else if (horiInput > 0 && vertiInput == 0)
            projDirect.direction = 90;
        else if (horiInput > 0 && vertiInput < 0)
            projDirect.direction = 135;
        else if (horiInput == 0 && vertiInput < 0)
            projDirect.direction = 180;
        else if (horiInput < 0 && vertiInput < 0)
            projDirect.direction = 225;
        else if (horiInput < 0 && vertiInput == 0)
            projDirect.direction = 270;
        else if (horiInput < 0 && vertiInput > 0)
            projDirect.direction = 315;

        //Animate the missile
        anim.SetInteger("Direction", (int)(projDirect.direction));
    }
}
