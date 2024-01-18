using System.Collections.Generic;
using UnityEngine;

public class EndlessTrackGenerator : MonoBehaviour
{
    [SerializeField] private TrackSegment[] trackSegments;
    [SerializeField] private TrackSegment firstTrackPrefab;
    [SerializeField] private int initialTrackCount = 10;

    private List<TrackSegment> currentSegments = new();

    private void Start()
    {
        TrackSegment initialTrack = Instantiate(firstTrackPrefab, transform);
        currentSegments.Add(initialTrack);
        TrackSegment previousTrack = initialTrack;

        for (int i = 0; i < initialTrackCount; i++)
        {
            int randomIndex = Random.Range(0, trackSegments.Length);
            TrackSegment currentTrack = Instantiate(trackSegments[randomIndex], transform);
            currentTrack.transform.position = previousTrack.End.position
                + (currentTrack.transform.position - currentTrack.Start.position);

            foreach (var obstacleSpawner in currentTrack.ObstaclesSpawners)
            {
                obstacleSpawner.SpawnObstacle();
            }

            currentSegments.Add(currentTrack);
            previousTrack = currentTrack;
        }
    }
}
