using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class AttackReset : StateMachineBehaviour
{
    [SerializeField] private string triggerName;

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger(triggerName);    
    }
}
