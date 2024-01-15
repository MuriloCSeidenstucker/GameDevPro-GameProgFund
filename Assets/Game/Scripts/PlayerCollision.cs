using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class PlayerCollision : MonoBehaviour
{
    [SerializeField] private PlayerAnimationController animator;
    private PlayerController player;

    private void Awake()
    {
        player = GetComponent<PlayerController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Obstacle obstacle = other.GetComponent<Obstacle>();
        if (obstacle != null && player != null)
        {
            player.OnPlayerDeath();
            if (animator != null)
            {
                animator.OnPlayerDeath();
            }
            Debug.Log($"Jogador colidiu com: {obstacle.name} e morreu");
        }
    }
}
