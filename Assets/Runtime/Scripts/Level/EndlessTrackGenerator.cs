using System.Collections.Generic;
using UnityEngine;

public class EndlessTrackGenerator : MonoBehaviour
{
    [SerializeField] private PlayerController player;
    [SerializeField] private TrackSegment firstTrackPrefab;
    [SerializeField] private TrackSegment[] easyTrackPrefabs;
    [SerializeField] private TrackSegment[] hardTrackPrefabs;
    [SerializeField] private TrackSegment[] rewardTrackPrefabs;

    [Header("Endless Generation Parameters")]
    [SerializeField] private int initialTrackCount = 10;
    [SerializeField] private int minTracksInFrontOfPlayer = 3;
    [SerializeField] private float minDistanceToConsiderInsideTrack = 3f;

    [Header("Level Difficulty Parameters")]
    [Range(0, 1)]
    [SerializeField] private float hardTrackChance = 0.3f;
    [SerializeField] private int minTracksBeforeReward = 10;
    [SerializeField] private int maxTracksBeforeReward = 20;
    [SerializeField] private int minRewardTrackCount = 1;
    [SerializeField] private int maxRewardTrackCount = 2;

    private List<TrackSegment> currentSegments = new();
    private bool isSpawningRewardTracks = false;
    private int rewardTracksLeftToRespawn = 0;
    private int trackSpawnedAfterLastReward = 0;

    private void Start()
    {
        SpawnTrackSegment(firstTrackPrefab, null);
        SpawnTracks(initialTrackCount);
    }

    private void Update()
    {
        TrackGeneratorLoop();
    }

    private void TrackGeneratorLoop()
    {
        int trackPlayerIndex = GetTrackIndexFromPlayer();
        if (trackPlayerIndex < 0) return;

        SpawnTracksInFrontOfPlayer(trackPlayerIndex);

        DespawnTracksBehindOfPlayer(trackPlayerIndex);
    }

    private int GetTrackIndexFromPlayer()
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

        return trackPlayerIndex;
    }

    private void SpawnTracksInFrontOfPlayer(int trackPlayerIndex)
    {
        int tracksInFrontOfPlayer = currentSegments.Count - (trackPlayerIndex + 1);
        if (tracksInFrontOfPlayer < minTracksInFrontOfPlayer)
        {
            SpawnTracks(minTracksInFrontOfPlayer - tracksInFrontOfPlayer);
        }
    }

        private void DespawnTracksBehindOfPlayer(int trackPlayerIndex)
    {
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
            TrackSegment randomTrack = GetRandomTrack();
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
        UpdateRewardTracking();
        return trackInstance;
    }

    private void UpdateRewardTracking()
    {
        if (isSpawningRewardTracks)
        {
            rewardTracksLeftToRespawn--;
            if (rewardTracksLeftToRespawn <= 0)
            {
                isSpawningRewardTracks = false;
                trackSpawnedAfterLastReward = 0;
            }
        }
        else
        {
            trackSpawnedAfterLastReward++;
            int requiredTracksBeforeReward = Random.Range(minTracksBeforeReward, maxTracksBeforeReward + 1);
            if (trackSpawnedAfterLastReward >= requiredTracksBeforeReward)
            {
                isSpawningRewardTracks = true;
                rewardTracksLeftToRespawn = Random.Range(minRewardTrackCount, maxRewardTrackCount);
            }
        }
    }

    private TrackSegment GetRandomTrack()
    {
        TrackSegment[] trackList = null;
        if (isSpawningRewardTracks)
        {
            trackList = rewardTrackPrefabs;
        }
        else
        {
            trackList = Random.value <= hardTrackChance ? hardTrackPrefabs : easyTrackPrefabs;
        }
        return trackList[Random.Range(0, trackList.Length)];
    }
}
