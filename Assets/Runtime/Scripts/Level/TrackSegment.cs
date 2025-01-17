using Unity.VisualScripting;
using UnityEngine;

public class TrackSegment : MonoBehaviour
{
    [SerializeField] private Transform start;
    [SerializeField] private Transform end;

    [SerializeField] private ObstacleSpawner[] obstacleSpawners;
    [SerializeField] private PickupSpawner[] pickupSpawners;
    [SerializeField] private DecorationSpawner decorationSpawner;

    public Transform Start => start;
    public Transform End => end;

    public float Length => Vector3.Distance(End.position, Start.position);
    public float SqrLength => (End.position - Start.position).sqrMagnitude;

    public ObstacleSpawner[] ObstacleSpawners => obstacleSpawners;
    public PickupSpawner[] PickupSpawners => pickupSpawners;
    public DecorationSpawner DecorationSpawner => decorationSpawner;
}
