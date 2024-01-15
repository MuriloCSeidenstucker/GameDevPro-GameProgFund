using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private PlayerController player;

    private void Awake()
    {
        player = GetComponent<PlayerController>();
    }

    private void Update()
    {
        animator.SetBool("IsJumping", player.IsJumping);
    }

    public void StartRun()
    {
        animator.SetTrigger(PlayerAnimationConstants.StartRun);
    }

    public void OnPlayerDeath()
    {
        animator.SetTrigger(PlayerAnimationConstants.OnDeath);
    }
}
