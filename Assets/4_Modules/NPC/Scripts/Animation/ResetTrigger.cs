using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NPC
{
    public class ResetTrigger : StateMachineBehaviour
    {
        [SerializeField] private string TriggerName;

        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (stateInfo.normalizedTime >= 1f)
            {
                animator.ResetTrigger(TriggerName);
            }
        }
    }
}
