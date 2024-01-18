using System.Collections.Generic;
using UnityEngine;

public class EndlessTrackGenerator : MonoBehaviour
{
    [SerializeField] private TrackSegment[] trackSegments;
    private List<TrackSegment> currentSegments = new();

    private void Start()
    {
        TrackSegment initialTrack = Instantiate(trackSegments[0], transform);
        currentSegments.Add(initialTrack);
        TrackSegment previousTrack = initialTrack;

        foreach (var trackPrefab in trackSegments)
        {
            TrackSegment currentTrack = Instantiate(trackPrefab, transform);

            currentTrack.transform.position = previousTrack.End.position
                + (currentTrack.transform.position - currentTrack.Start.position);

            currentSegments.Add(currentTrack);
            previousTrack = currentTrack;
        }
    }
}
