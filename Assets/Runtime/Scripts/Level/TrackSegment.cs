using UnityEngine;

public class TrackSegment : MonoBehaviour
{
    [SerializeField] private Transform start;
    [SerializeField] private Transform end;

    private ObstacleSpawner[] obstaclesSpawners;

    public Transform Start => start;
    public Transform End => end;

    public ObstacleSpawner[] ObstaclesSpawners => obstaclesSpawners == null
        ? obstaclesSpawners = GetComponentsInChildren<ObstacleSpawner>()
        : obstaclesSpawners;
}
