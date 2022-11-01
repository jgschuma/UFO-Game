using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateConsoleScreens : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.gameObject.transform.Find("HoleStatic").GetComponent<Animator>().SetBool("itemFound", animator.GetBool("gotBlackHole"));
        animator.gameObject.transform.Find("WarpStatic").GetComponent<Animator>().SetBool("itemFound", animator.GetBool("gotWarp"));
        animator.gameObject.transform.Find("GunnerStatic").GetComponent<Animator>().SetBool("itemFound", animator.GetBool("gotGunner"));
        animator.gameObject.transform.Find("ShieldStatic").GetComponent<Animator>().SetBool("itemFound", animator.GetBool("gotShield"));
        animator.gameObject.transform.Find("FlameStatic").GetComponent<Animator>().SetBool("itemFound", animator.GetBool("gotFlame"));
        animator.gameObject.transform.Find("TwisterStatic").GetComponent<Animator>().SetBool("itemFound", animator.GetBool("gotTwister"));
        animator.gameObject.transform.Find("LaserStatic").GetComponent<Animator>().SetBool("itemFound", animator.GetBool("gotLaser"));
        animator.gameObject.transform.Find("BombStatic").GetComponent<Animator>().SetBool("itemFound", animator.GetBool("gotBomber"));
        animator.gameObject.transform.Find("MissileStatic").GetComponent<Animator>().SetBool("itemFound", animator.GetBool("gotMissile"));
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
