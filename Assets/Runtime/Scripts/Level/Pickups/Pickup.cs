using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Pickup : MonoBehaviour
{
    [SerializeField] private GameObject graphics;
    [SerializeField] private AudioClip pickupSFX;

    private AudioSource audioSource;
    private float selfDestructionTime => pickupSFX.length;
    private bool wasCollected;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponentInParent<PlayerController>();
        if (player && wasCollected == false)
        {
            WhenCollecting();
        }
    }

    private void WhenCollecting()
    {
        AudioUtility.PlayAudioCue(audioSource, pickupSFX);
        graphics.SetActive(false);
        wasCollected = true;
        Destroy(this.gameObject, selfDestructionTime);
    }
}
