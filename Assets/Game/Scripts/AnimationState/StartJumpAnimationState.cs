using UnityEngine;

public class StartJumpAnimationState : StateMachineBehaviour
{
    private PlayerController player;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = animator.transform.parent.GetComponent<PlayerController>();
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       if (player != null)
       {
            player.enabled = true;
       }
    }
}
