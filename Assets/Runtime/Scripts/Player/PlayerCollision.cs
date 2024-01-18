using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class PlayerCollision : MonoBehaviour
{
    [SerializeField] private PlayerAnimationController animController;
    [SerializeField] private GameMode gameMode;
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
            gameMode.OnGameOver();
        }
    }
}
