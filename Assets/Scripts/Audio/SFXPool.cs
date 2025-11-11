using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.Core.Singleton;

public class SFXPool : Singleton<SFXPool>
{
    private List<AudioSource> _audioSourceList;

    public int poolSize = 10;

    private int _index = 0;


    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }


    private void Start()
    {
        CreatePool();
    }

    private void CreatePool()
    {
        _audioSourceList = new List<AudioSource>();

        for(int i = 0; i < poolSize; i++)
        {
            CreateAudioSourceItem();
        }
    }

    private void CreateAudioSourceItem()
    {
        GameObject go = new GameObject("SFX_Pool");
        go.transform.SetParent(gameObject.transform);
        _audioSourceList.Add(go.AddComponent<AudioSource>());
    }

    public void Play(SFXType sfxType)
    {
        if (sfxType == SFXType.NONE) return;

        if (SoundManager.Instance == null)
        {
            Debug.LogWarning("SoundManager não encontrado!");
            return;
        }

        var sfx = SoundManager.Instance.GetSFXByType(sfxType);
        if (sfx == null || sfx.audioClip == null)
        {
            Debug.LogWarning($"SFX não configurado para {sfxType}");
            return;
        }

        _audioSourceList[_index].PlayOneShot(sfx.audioClip);

        _index++;
        if (_index >= _audioSourceList.Count) _index = 0;
    }



}
