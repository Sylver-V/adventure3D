using UnityEngine;

public class PauseMenuController : MonoBehaviour
{
    [Header("Referência ao Menu")]
    public GameObject menuUI;

    private bool isPaused = false;


    protected void Awake()
    {
        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(menuUI);
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void SaveButton()
    {
        if (SaveManager.Instance != null)
        {
            SaveManager.Instance.SaveGame();
            Debug.Log("Save manual feito pelo Pause Menu.");
        }
        else
        {
            Debug.LogWarning("SaveManager não encontrado!");
        }
    }


    public void Pause()
    {
        menuUI.SetActive(true);  
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void Resume()
    {
        menuUI.SetActive(false); 
        Time.timeScale = 1f; 
        isPaused = false;
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Jogo encerrado.");
    }
}
