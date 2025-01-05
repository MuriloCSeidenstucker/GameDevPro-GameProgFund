using UnityEngine;

public static class AudioUtility
{
    public static void PlayAudioCue(AudioSource source, AudioClip clip)
    {
        if (source.outputAudioMixerGroup is null)
        {
            Debug.LogError("Erro: Todo AudioSource deve ter um AudioMixerGroup assinalado");
        }
        else
        {
            source.clip = clip;
            source.loop = false;
            source.Play();
        }
    }

    public static void PlayMusic(AudioSource source, AudioClip clip)
    {
        if (source.outputAudioMixerGroup is null)
        {
            Debug.LogError("Erro: Todo AudioSource deve ter um AudioMixerGroup assinalado");
        }
        else
        {
            source.clip = clip;
            source.loop = true;
            source.Play();
        }
    }
}
