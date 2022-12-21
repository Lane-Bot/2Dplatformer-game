using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetFloatBehaviour : StateMachineBehaviour
{
    public string floatName;
    public bool updateOnstateEnter, updateOnstateExit;
    public bool updateonstateMachineEnter , updateonstateMachineExit;   
    public float valueOnEnter , valueOnExit ;   
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
      if (updateOnstateEnter)
        {
            animator.SetFloat(floatName, valueOnEnter);
        }
    }
    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (updateOnstateExit)
        {
            animator.SetFloat(floatName, valueOnExit);
        }

    }
    // OnStateMachineEnter is called when entering a state machine via its Entry Node
    override public void OnStateMachineEnter(Animator animator, int stateMachinePathHash)
    {
        if (updateonstateMachineEnter)
            animator.SetFloat(floatName, valueOnEnter);
    }

    // OnStateMachineExit is called when exiting a state machine via its Exit Node
    override public void OnStateMachineExit(Animator animator, int stateMachinePathHash)
    {
        if (updateonstateMachineExit)
            animator.SetFloat(floatName, valueOnExit);
    }
}
