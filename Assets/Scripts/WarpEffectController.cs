using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpEffectController : MonoBehaviour
{
    public Animator WarpEffectAnim;
    public void StartAnimisFinished()
    {
        WarpEffectAnim.SetBool("StartDone", true);
    }
    public void EndAnimisFinished(){
        Destroy(this.gameObject);
    }
}
