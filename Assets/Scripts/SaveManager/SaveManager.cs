using Ebac.Core.Singleton;
using System;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveManager : Singleton<SaveManager>
{
    [SerializeField] private SaveSetup _saveSetup;
    private string _path = Application.streamingAssetsPath + "/save.txt";

    public int lastLevel;
    public Action<SaveSetup> FileLoaded;

    public SaveSetup Setup => _saveSetup;

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);

        SceneManager.sceneLoaded += OnSceneLoaded;
    }


    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (_saveSetup != null)
        {
            StartCoroutine(ApplySaveWhenReady());
        }
    }


    private void CreateNewSave()
    {
        _saveSetup = new SaveSetup();
        _saveSetup.lastLevel = 0;
        _saveSetup.playerName = "Victor";
    }

    private IEnumerator Start()
    {
        Load();

        yield return new WaitUntil(() =>
            Cloth.ClothManager.Instance != null &&
            Player.Instance != null &&
            CheckpointManager.Instance != null);

        ApplySave();
    }

    #region SAVE

    [NaughtyAttributes.Button]
    public void Save()
    {
        string setupToJson = JsonUtility.ToJson(_saveSetup, true);
        Debug.Log(setupToJson);
        SaveFile(setupToJson);
    }

    public void SaveItems()
    {
        _saveSetup.coins = Items.ItemManager.Instance.GetItemByType(Items.ItemType.COIN).soInt.value;
        _saveSetup.healthPacks = Items.ItemManager.Instance.GetItemByType(Items.ItemType.LIFE_PACK).soInt.value;

        if (Player.Instance != null && Player.Instance.healthBase != null)
        {
            _saveSetup.currentLife = Player.Instance.healthBase.CurrentLife;
        }

        Save();

    }

    public void SaveGame()
    {
        if (Player.Instance != null)
        {
            // salva posição
            _saveSetup.playerPosition = Player.Instance.transform.position;

            // salva vida atual
            if (Player.Instance.healthBase != null)
            {
                _saveSetup.currentLife = Player.Instance.healthBase.CurrentLife;
            }
        }

        // salva itens e packs
        SaveItems();

        // salva cena atual
        _saveSetup.sceneName = SceneManager.GetActiveScene().name;

        Save();
        Debug.Log("Jogo salvo manualmente pelo menu.");
    }


    public void SaveName(string text)
    {
        _saveSetup.playerName = text;
        Save();
    }

    public void SaveLastLevel(int level, int checkpointKey = -1)
    {
        _saveSetup.lastLevel = level;
        _saveSetup.sceneName = SceneManager.GetActiveScene().name;

        if (checkpointKey >= 0)
        {
            _saveSetup.checkpointKey = checkpointKey;
        }

        SaveItems();
        Save();
    }

    public void SaveCloth(Cloth.ClothType type)
    {
        _saveSetup.clothType = type;
        Save();
    }

    private void ApplySave()
    {
        if (_saveSetup == null)
        {
            Debug.LogWarning("SaveSetup está nulo. Nada para aplicar.");
            return;
        }

        // aplica checkpoint só quando Player já existe
        if (CheckpointManager.Instance != null && Player.Instance != null)
        {
            CheckpointManager.Instance.LoadCheckpoint(_saveSetup.checkpointKey);
        }

        // aplica itens
        Items.ItemManager.Instance.GetItemByType(Items.ItemType.COIN).soInt.value = Mathf.RoundToInt(_saveSetup.coins);
        Items.ItemManager.Instance.GetItemByType(Items.ItemType.LIFE_PACK).soInt.value = Mathf.RoundToInt(_saveSetup.healthPacks);

        // aplica cloth
        if (_saveSetup.clothType != Cloth.ClothType.NONE && Cloth.ClothManager.Instance != null)
        {
            var setup = Cloth.ClothManager.Instance.GetSetupByType(_saveSetup.clothType);
            if (setup != null && Player.Instance != null)
            {
                Player.Instance.ChangeTexture(setup, 0);
            }
        }

        if (Player.Instance != null && Player.Instance.healthBase != null)
        {
            Player.Instance.healthBase.SetLife(_saveSetup.currentLife);
        }

        if (Player.Instance != null)
        {
            // aplica posição salva
            if (_saveSetup.playerPosition != Vector3.zero)
            {
                Player.Instance.transform.position = _saveSetup.playerPosition;
            }

            // aplica vida
            if (Player.Instance.healthBase != null)
            {
                Player.Instance.healthBase.SetLife(_saveSetup.currentLife);
            }
        }



        Debug.Log("Save aplicado com sucesso. Checkpoint: " + _saveSetup.checkpointKey);
    }

    [NaughtyAttributes.Button]
    public void Load()
    {
        if (!File.Exists(_path))
        {
            Debug.LogWarning("Arquivo de save não encontrado. Criando novo...");
            CreateNewSave();
            Save();
            return;
        }

        string fileLoaded = File.ReadAllText(_path);
        _saveSetup = new SaveSetup();
        JsonUtility.FromJsonOverwrite(fileLoaded, _saveSetup);

        lastLevel = _saveSetup.lastLevel;
        FileLoaded?.Invoke(_saveSetup);

        if (CheckpointManager.Instance != null)
        {
            CheckpointManager.Instance.LoadCheckpoint(_saveSetup.checkpointKey);
        }
    }

    #endregion

    private void SaveFile(string json)
    {
        Debug.Log(_path);
        File.WriteAllText(_path, json);
    }

    public void ResetSave()
    {
        _saveSetup = new SaveSetup();
        _saveSetup.lastLevel = 0;
        _saveSetup.sceneName = "SCN_Art_3D_Level01";
        _saveSetup.coins = 0;
        _saveSetup.healthPacks = 0;
        _saveSetup.clothType = 0;
        _saveSetup.checkpointKey = 0;
        _saveSetup.playerName = "Victor";

        if (Player.Instance != null && Player.Instance.healthBase != null)
        {
            _saveSetup.currentLife = Player.Instance.healthBase.startLife;
        }
        else
        {
            // fallback: assume 10 como padrão
            _saveSetup.currentLife = 10f;
        }


        // chama o método privado Save() de dentro da classe
        Save();
    }

    public IEnumerator ApplySaveWhenReady()
    {
        yield return new WaitUntil(() =>
            Cloth.ClothManager.Instance != null &&
            Player.Instance != null &&
            CheckpointManager.Instance != null);

        ApplySave();
    }

}

[System.Serializable]
public class SaveSetup
{
    public int lastLevel;
    public int checkpointKey;
    public float coins;
    public float healthPacks;
    public float currentLife;

    public Cloth.ClothType clothType;

    public Vector3 playerPosition;

    public string sceneName;
    public string playerName;

}
