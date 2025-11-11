using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class EndGame : MonoBehaviour
{
    [Header("Objetos de Fim de Jogo")]
    public List<GameObject> endGameObjects;

    [Header("Configuração de Level")]
    public int currentLevel = 1;

    private bool _endGame = false;

    private void Awake()
    {
        // Garante que todos os objetos de fim de jogo começam desativados
        endGameObjects.ForEach(i => i.SetActive(false));
    }

    private void OnTriggerEnter(Collider other)
    {
        Player p = other.transform.GetComponent<Player>();

        if (!_endGame && p != null)
        {
            ShowEndGame();
        }
    }

    private void ShowEndGame()
    {
        _endGame = true;

        // Ativa e anima os objetos de fim de jogo
        foreach (var i in endGameObjects)
        {
            i.SetActive(true);
            i.transform.DOScale(0, .2f).SetEase(Ease.OutBack).From();
        }

        // Salva progresso
        SaveManager.Instance.SaveLastLevel(currentLevel);

        // Música de vitória
        SoundManager.Instance.PlayMusicByType(MusicType.COMPLETE);

        // Efeito sonoro opcional
        SFXPool.Instance.Play(SFXType.Chest);

        // Voltar para o Menu após alguns segundos
        StartCoroutine(ReturnToMenu());
    }

    private IEnumerator ReturnToMenu()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("SCN_Menu"); // nome da cena do menu
    }
}
