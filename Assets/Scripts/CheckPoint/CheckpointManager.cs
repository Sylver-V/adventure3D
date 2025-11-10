using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.Core.Singleton;

public class CheckpointManager : Singleton<CheckpointManager>
{
    public int lastCheckpointKey = 0;

    public List<CheckpointBase> checkpoints;

    private void Start()
    {
        SaveManager.Instance.FileLoaded += OnFileLoaded;
    }

    private void OnFileLoaded(SaveSetup setup)
    {
        LoadCheckpoint(setup.checkpointKey);
    }


    public bool HasCheckpoint()
    {
        return lastCheckpointKey > 0;
    }

    public void SaveCheckpoint(int i)
    {
        if (i > lastCheckpointKey)
        {
            lastCheckpointKey = i;
            SaveManager.Instance.SaveLastLevel(SaveManager.Instance.lastLevel, lastCheckpointKey);
        }
    }


    public Vector3 GetPositionFromLastCheckpoint()
    {
        var checkpoint = checkpoints.Find(i => i.key == lastCheckpointKey);
        return checkpoint.transform.position;
    }

    public void LoadCheckpoint(int key)
    {
        var checkpoint = checkpoints.Find(i => i.key == key);
        if (checkpoint != null && Player.Instance != null)
        {
            Player.Instance.transform.position = checkpoint.transform.position;
            Debug.Log("Jogador movido para o checkpoint " + key);
        }
        else
        {
            Debug.LogWarning("Checkpoint ou Player não encontrado para a key: " + key);
        }
    }


}
