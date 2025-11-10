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
        _current_MusicSetup = SoundManager.Instance.GetMusicByType(musicType);

        audioSource.clip = _current_MusicSetup.audioClip;
        audioSource.Play();
    }
}
