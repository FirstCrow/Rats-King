using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeCombo : StateMachineBehaviour
{
    PlayerController thisplayer;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        thisplayer = animator.GetComponent<PlayerController>();
        thisplayer.attackTimer = thisplayer.GetAnimationClipLength("player_attack_down");
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && thisplayer.attackcounter < 3 && thisplayer.attackTimer <= .15f)
        {
            Debug.Log("Repeating attack");
            animator.SetTrigger("repeatattack");
            thisplayer.attackcounter++;
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("repeatattack");
        
    }

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
