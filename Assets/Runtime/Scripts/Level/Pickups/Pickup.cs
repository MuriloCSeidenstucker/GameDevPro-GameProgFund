using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Pickup : MonoBehaviour
{
    [SerializeField] private GameObject graphics;
    [SerializeField] private AudioClip pickupSFX;

    // private GameMode gameMode;
    private AudioSource audioSource;
    private float selfDestructionTime => pickupSFX.length;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponentInParent<PlayerController>();
        if (player)
        {
            StartCoroutine(OnHitCoroutine());
        }
    }

    private IEnumerator OnHitCoroutine()
    {
        // gameMode.CollectPickup();
        AudioUtility.PlayAudioCue(audioSource, pickupSFX);
        graphics.SetActive(false);
        yield return new WaitForSeconds(selfDestructionTime);
        Destroy(this.gameObject);
    }
}
