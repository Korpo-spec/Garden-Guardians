using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateBlendValue : StateMachineBehaviour
{
    
    EntityAttack entityAttack;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        entityAttack = animator.GetComponent<EntityAttack>();
        //entityAttack.ResumeMovement();
        Debug.Log("Blend updated");
        float blend = animator.GetFloat("Blend");
        int maxCombo = animator.GetInteger("MaxCombo"); 
        blend += 0.5f;
        if (blend > 1 || maxCombo <= blend*2)
        {
            blend = 0;
        }
        
        entityAttack.StartCoroutine(Transition(animator, animator.GetAnimatorTransitionInfo(layerIndex).duration/2, animator.GetFloat("Blend"), blend));
    }

    private IEnumerator Transition(Animator animator,float duration,float currentBlendValue ,float nextBlendValue)
    {
        float time = 0;
        float scaledDuration = 1f / duration;
        
        while (time < 1)
        {
            time += Time.deltaTime * scaledDuration;
            animator.SetFloat("Blend",Mathf.Lerp(currentBlendValue, nextBlendValue, time));
            yield return null;
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

     // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       
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
