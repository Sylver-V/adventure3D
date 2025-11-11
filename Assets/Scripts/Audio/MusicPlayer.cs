using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public MusicType musicType;
    public AudioSource audioSource;

    private MusicSetup _current_MusicSetup;

    private void Start()
    {
        Play();
    }

    private void Play()
    {
        if (SoundManager.Instance == null)
        {
            Debug.LogError("SoundManager não encontrado na cena!");
            return;
        }

        _current_MusicSetup = SoundManager.Instance.GetMusicByType(musicType);

        if (_current_MusicSetup == null || _current_MusicSetup.audioClip == null)
        {
            Debug.LogWarning($"Nenhum MusicSetup configurado para {musicType}");
            return;
        }

        if (audioSource == null)
        {
            Debug.LogError("AudioSource não atribuído no MusicPlayer!");
            return;
        }

        audioSource.clip = _current_MusicSetup.audioClip;
        audioSource.Play();
    }

}
