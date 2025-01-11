using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class UIAudioController : MonoBehaviour
{
    [SerializeField] private AudioClip btnClickSFX;
    [SerializeField] private AudioClip countdownSFX;
    [SerializeField] private AudioClip countdownEndSFX;

    private AudioSource audioSource;
    private AudioSource AudioSrc => audioSource is null ? audioSource = GetComponent<AudioSource>() : audioSource;

    void Start()
    {
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
    }

    private void PlaySFX(AudioClip clip)
    {
        AudioUtility.PlayAudioCue(AudioSrc, clip);
    }

    public void PlayButtonClickSFX() => PlaySFX(btnClickSFX);
    public void PlayCountdownSFX() => PlaySFX(countdownSFX);
    public void PlayCountdownEndSFX() => PlaySFX(countdownEndSFX);
}
