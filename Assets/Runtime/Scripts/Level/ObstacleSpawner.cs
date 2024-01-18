using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private Obstacle[] obstacleOptions;

    private Obstacle currentObstacle;

    public void SpawnObstacle()
    {
        int index = Random.Range(0, obstacleOptions.Length);
        Obstacle prefab = obstacleOptions[index];
        currentObstacle = Instantiate(prefab, transform);
        currentObstacle.transform.localPosition = Vector3.zero;
        currentObstacle.transform.rotation = Quaternion.identity;
    }
}
