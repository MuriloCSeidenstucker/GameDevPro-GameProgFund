using UnityEngine;

public class RollAnimationState : StateMachineBehaviour
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        AnimatorClipInfo[] clips = animator.GetNextAnimatorClipInfo(layerIndex);
        if (clips.Length > 0)
        {
            AnimatorClipInfo rollClip = clips[0];
            PlayerController player = animator.transform.parent.GetComponent<PlayerController>();
            if (player != null)
            {
                float multiplier = rollClip.clip.length / player.RollDuration;
                animator.SetFloat(PlayerAnimationConstants.RollMultiplier, multiplier);
            }
        }
    }
}
