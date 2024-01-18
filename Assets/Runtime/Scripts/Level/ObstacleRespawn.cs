using UnityEngine;

public class ObstacleRespawn : MonoBehaviour
{
    [SerializeField] private Obstacle[] obstacleOptions;

    private void Start()
    {
        int index = Random.Range(0, obstacleOptions.Length);
        Instantiate(obstacleOptions[index], transform);
    }
}
