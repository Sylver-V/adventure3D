using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayLevel : MonoBehaviour
{
    public TextMeshProUGUI uiTextName;

    private void Start()
    {
        SaveManager.Instance.FileLoaded += OnLoad;

        if (SaveManager.Instance.Setup != null)
        {
            OnLoad(SaveManager.Instance.Setup);
        }
    }


    public void OnLoad(SaveSetup setup)
    {
        uiTextName.text = "Play " + (setup.lastLevel + 1);
    }

    private void OnDestroy()
    {
        SaveManager.Instance.FileLoaded -= OnLoad;
    }
}
