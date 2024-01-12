using UnityEngine;

public class IdleAnimationState : StateMachineBehaviour
{
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetTrigger(PlayerAnimationConstants.StartRun);
        }
    }
}
