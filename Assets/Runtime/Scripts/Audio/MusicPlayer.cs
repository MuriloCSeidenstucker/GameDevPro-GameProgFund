using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicPlayer : MonoBehaviour
{
    [SerializeField] private AudioClip startMenuMusic;
    [SerializeField] private AudioClip mainTrackMusic;
    [SerializeField] private AudioClip gameOverTrackMusic;

    private AudioSource audioSource;
    private AudioSource AudioSrc => audioSource == null ? audioSource = GetComponent<AudioSource>() : audioSource;

    private void PlayMusic(AudioClip clip)
    {
        AudioUtility.PlayMusic(AudioSrc, clip);
    }

    public void StopMusic() => AudioSrc.Stop();
    public void PlayStartMenuMusic() => PlayMusic(startMenuMusic);
    public void PlayMainTrackMusic() => PlayMusic(mainTrackMusic);
    public void PlayGameOverTrackMusic() => PlayMusic(gameOverTrackMusic);
}
