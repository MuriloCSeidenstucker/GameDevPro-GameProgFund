using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private PlayerController player;

    public bool IsGameStartAnimFinished = false;

    private void Awake()
    {
        player = GetComponent<PlayerController>();
    }

    private void Update()
    {
        animator.SetBool(PlayerAnimationConstants.IsJumping, player.IsJumping);
        animator.SetBool(PlayerAnimationConstants.IsRolling, player.IsRolling);
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("StartRun") && IsGameStartAnimFinished == false)
        {
            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
            {
                IsGameStartAnimFinished = true;
            }
        }
    }

    public void Die()
    {
        animator.SetTrigger(PlayerAnimationConstants.DieTrigger);
    }

    public void StartGameAnim()
    {
        animator.SetTrigger(PlayerAnimationConstants.StartGameTrigger);
    }
}
