using System.Collections.Generic;
using UnityEngine;

public class EndlessTrackGenerator : MonoBehaviour
{
    [SerializeField] private PlayerController player;
    [SerializeField] private TrackSegment[] trackSegments;
    [SerializeField] private TrackSegment firstTrackPrefab;

    [Header("Endless Generation Parameters")]
    [SerializeField] private int initialTrackCount = 10;
    [SerializeField] private int minTracksInFrontOfPlayer = 3;
    [SerializeField] private float minDistanceToConsiderInsideTrack = 3f;

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
            TrackSegment track = currentSegments[i];
            if (player.transform.position.z >= (track.Start.position.z + minDistanceToConsiderInsideTrack) &&
                player.transform.position.z <= track.End.position.z)
            {
                trackPlayerIndex = i;
                break;
            }
        }

        if (trackPlayerIndex < 0) return;

        int tracksInFrontOfPlayer = currentSegments.Count - (trackPlayerIndex + 1);
        if (tracksInFrontOfPlayer < minTracksInFrontOfPlayer)
        {
            SpawnTracks(minTracksInFrontOfPlayer - tracksInFrontOfPlayer);
        }

        for (int i = 0; i < trackPlayerIndex; i++)
        {
            TrackSegment track = currentSegments[i];
            Destroy(track.gameObject);
        }

        currentSegments.RemoveRange(0, trackPlayerIndex);
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
