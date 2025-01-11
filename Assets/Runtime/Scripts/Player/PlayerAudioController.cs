using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlayerAudioController : MonoBehaviour
{
    [SerializeField] private AudioClip jumpSFX;
    [SerializeField] private AudioClip rollSFX;

    private AudioSource audioSource;
    private AudioSource AudioSource => audioSource is null ? audioSource = GetComponent<AudioSource>() : audioSource;

    void Start()
    {
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
    }

    private void PlaySFX(AudioClip clip)
    {
        AudioUtility.PlayAudioCue(AudioSource, clip);
    }

    public void PlayJumpSFX() => PlaySFX(jumpSFX);
    public void PlayRollSFX() => PlaySFX(rollSFX);
}
