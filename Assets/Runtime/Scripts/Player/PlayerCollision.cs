using UnityEngine;

[RequireComponent(typeof(PlayerController))]
[RequireComponent(typeof(PlayerAnimationController))]
public class PlayerCollision : MonoBehaviour
{
    [SerializeField] private GameMode gameMode;
    private PlayerController playerController;
    private PlayerAnimationController animationController;
    private Pickup lastPickup;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
        animationController = GetComponent<PlayerAnimationController>();
        Debug.Log(gameMode.CollectedPickups);
    }

    private void OnTriggerEnter(Collider other)
    {
        Obstacle obstacle = other.GetComponent<Obstacle>();
        if (obstacle != null)
        {
            playerController.Die();
            animationController.Die();
            gameMode.OnGameOver();
            obstacle.PlayCollisionFeedback(other);
        }

        Pickup pickup = other.GetComponent<Pickup>();
        if (pickup != null && pickup != lastPickup)
        {
            gameMode.CollectPickup();
            Debug.Log(gameMode.CollectedPickups);
            lastPickup = pickup;
        }
    }
}
