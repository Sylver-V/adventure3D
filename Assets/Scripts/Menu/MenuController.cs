using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [Header("UI")]
    public GameObject menuUI;
    //public TMPro.TextMeshProUGUI playerNameText;

    public void PlayGame()
    {
        string sceneToLoad;

        if (SaveManager.Instance != null && SaveManager.Instance.Setup != null &&
            !string.IsNullOrEmpty(SaveManager.Instance.Setup.sceneName))
        {
            sceneToLoad = SaveManager.Instance.Setup.sceneName;
        }
        else
        {
            sceneToLoad = "SCN_Art_3D_Level01";
        }

        SceneManager.LoadScene(sceneToLoad);

        // desativa o menu imediatamente
        if (menuUI != null) menuUI.SetActive(false);
    }


    public void NewGame()
    {
        SaveManager.Instance.ResetSave();

        string sceneToLoad = SaveManager.Instance.Setup.sceneName;

        if (Application.CanStreamedLevelBeLoaded(sceneToLoad))
        {
            SceneManager.LoadScene(sceneToLoad);
        }
        else
        {
            Debug.LogError($"Cena '{sceneToLoad}' não está no Build Settings!");
        }

        // desativa o menu imediatamente
        if (menuUI != null) menuUI.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Jogo encerrado.");
    }
}
