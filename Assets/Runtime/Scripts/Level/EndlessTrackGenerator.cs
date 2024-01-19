using System.Collections.Generic;
using UnityEngine;

public class EndlessTrackGenerator : MonoBehaviour
{
    [SerializeField] private PlayerController player;
    [SerializeField] private TrackSegment[] trackSegments;
    [SerializeField] private TrackSegment firstTrackPrefab;

    [Header("Endless Generation Parameters")]
    [SerializeField] private int initialTrackCount = 10;
    [SerializeField] private int minTracksInFrontPlayer = 3;

    private List<TrackSegment> currentSegments = new();

    private void Start()
    {
        SpawnTrackSegment(firstTrackPrefab, null);
        SpawnTracks(initialTrackCount);
    }

    private void Update()
    {
        int trackPlayerIndex = -1;
        for (int i = 0; i < currentSegments.Count; i++)
        {
            if (player.transform.position.z >= currentSegments[i].Start.position.z
                && player.transform.position.z <= currentSegments[i].End.position.z)
            {
                trackPlayerIndex = i;
            }
        }

        if (currentSegments.Count - (trackPlayerIndex + 1) < minTracksInFrontPlayer)
        {
            Debug.Log(currentSegments.Count - (trackPlayerIndex + 1));
        }
    }

    private void SpawnTracks(int trackCount)
    {
        TrackSegment previousTrack = currentSegments.Count > 0
            ? currentSegments[currentSegments.Count - 1]
            : null;

        for (int i = 0; i < trackCount; i++)
        {
            int randomIndex = Random.Range(0, trackSegments.Length);
            TrackSegment randomTrack = trackSegments[randomIndex];
            previousTrack = SpawnTrackSegment(randomTrack, previousTrack);
        }
    }

    private TrackSegment SpawnTrackSegment(TrackSegment randomTrack, TrackSegment previousTrack)
    {
        TrackSegment trackInstance = Instantiate(randomTrack, transform);

        if (previousTrack != null)
        {
            trackInstance.transform.position = previousTrack.End.position
            + (trackInstance.transform.position - trackInstance.Start.position);
        }
        else
        {
            trackInstance.transform.localPosition = Vector3.zero;
        }

        foreach (var obstacleSpawner in trackInstance.ObstaclesSpawners)
        {
            obstacleSpawner.SpawnObstacle();
        }

        currentSegments.Add(trackInstance);
        return trackInstance;
    }
}
