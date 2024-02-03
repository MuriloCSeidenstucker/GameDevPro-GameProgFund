using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicPlayer : MonoBehaviour
{
    [SerializeField] private AudioClip startMenuMusic;
    [SerializeField] private AudioClip mainTrackMusic;

    private AudioSource audioSource;
    private AudioSource AudioSrc => audioSource is null ? audioSource = GetComponent<AudioSource>() : audioSource;

    private void PlayMusic(AudioClip clip)
    {
        AudioSrc.clip = clip;
        AudioSrc.loop = true;
        AudioSrc.Play();
    }

    public void StopMusic() => AudioSrc.Stop();
    public void PlayStartMenuMusic() => PlayMusic(startMenuMusic);
    public void PlayMainTrackMusic() => PlayMusic(mainTrackMusic);
}
