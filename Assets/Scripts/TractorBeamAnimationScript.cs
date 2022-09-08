using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TractorBeamAnimationScript : MonoBehaviour
{
    public SpriteRenderer tractorBeam;

    // Start is called before the first frame update
    void Start()
    {
        tractorBeam = gameObject.GetComponent("SpriteRenderer") as SpriteRenderer;
    }

    // Update is called once per frame
    void Update()
    {
        while (Input.GetKeyDown("z"))
        {
            tractorBeam.enabled = true;
        }
        if (Input.GetKeyDown("z") == false)
        {
            tractorBeam.enabled = false;
        }
    }
}
