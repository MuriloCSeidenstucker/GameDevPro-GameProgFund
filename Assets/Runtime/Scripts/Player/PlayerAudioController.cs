using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlayerAudioController : MonoBehaviour
{
    [SerializeField] private AudioClip jumpSFX;
    [SerializeField] private AudioClip rollSFX;

    private AudioSource audioSource;
    private AudioSource AudioSrc => audioSource is null ? audioSource = GetComponent<AudioSource>() : audioSource;

    private void PlaySFX(AudioClip clip)
    {
        AudioSrc.clip = clip;
        AudioSrc.loop = false;
        AudioSrc.Play();
    }

    public void PlayJumpSFX() => PlaySFX(jumpSFX);
    public void PlayRollSFX() => PlaySFX(rollSFX);
}
