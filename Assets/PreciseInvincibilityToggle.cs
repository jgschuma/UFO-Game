using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreciseInvincibilityToggle : StateMachineBehaviour
{
    public int toggleFrame = 1;
    public bool toggleInvincibility = false;

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Debug.Log("Current frame of animation: " + stateInfo.normalizedTime * stateInfo.length * 60);
        if (stateInfo.normalizedTime * stateInfo.length * 60 >= toggleFrame)
        {
            //Debug.Log("TOGGLE");
            animator.gameObject.GetComponent<EntityHealth>().invincible = toggleInvincibility;
        }
    }
}
