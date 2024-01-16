using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class PlayerCollision : MonoBehaviour
{
    [SerializeField] private PlayerAnimationController animController;
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
            if (animController != null)
            {
                animController.OnPlayerDeath();
            }
            Debug.Log($"Jogador colidiu com: {obstacle.name} e morreu");
        }
    }
}
