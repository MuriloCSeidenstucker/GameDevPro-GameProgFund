using System.Collections.Generic;
using UnityEngine;

public class EndlessTrackGenerator : MonoBehaviour
{
    [SerializeField] private TrackSegment[] trackSegments;
    [SerializeField] private int initialTrackCount = 10;

    private List<TrackSegment> currentSegments = new();
    private int easyTrackIndex = 1;
    private int hardTrackIndex = 2;

    private void Start()
    {
        TrackSegment initialTrack = Instantiate(trackSegments[0], transform);
        currentSegments.Add(initialTrack);
        TrackSegment previousTrack = initialTrack;

        for (int i = 0; i < initialTrackCount; i++)
        {
            int randomIndex = Random.Range(easyTrackIndex,hardTrackIndex + 1);
            TrackSegment currentTrack = Instantiate(trackSegments[randomIndex], transform);

            currentTrack.transform.position = previousTrack.End.position
                + (currentTrack.transform.position - currentTrack.Start.position);

            currentSegments.Add(currentTrack);
            previousTrack = currentTrack;
        }
    }
}
