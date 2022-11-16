using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwisterTimer : StateMachineBehaviour
{
    float duration;
    
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        duration = animator.gameObject.transform.Find("Items").transform.Find("TwisterPower").GetComponent<TwisterPower>().twisterDuration;
        animator.SetBool("twisterActive", true);
        //Debug.Log("Twister duration: " + duration);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Debug.Log("Twister time elapsed: " + stateInfo.normalizedTime * stateInfo.length);
        if(stateInfo.normalizedTime * stateInfo.length >= duration)
        {
            animator.SetBool("twisterActive", false);
        }
    }
}
