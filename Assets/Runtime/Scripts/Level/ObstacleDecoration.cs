using UnityEngine;

public class ObstacleDecoration : MonoBehaviour
{
    [SerializeField] private AudioClip collisionSFX;
    [SerializeField] private Animation collisionAnimation;

    private AudioSource audioSource;
    private AudioSource AudioSrc => audioSource is null ? audioSource = GetComponent<AudioSource>() : audioSource;

    public void PlayCollisionFeedback()
    {
        AudioUtility.PlayAudioCue(AudioSrc, collisionSFX);
        collisionAnimation?.Play();
    }
}
