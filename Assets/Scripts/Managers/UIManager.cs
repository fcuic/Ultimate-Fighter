using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public GameObject GameOverPanel;
    public GameObject LevelComplete;
    public GameObject GameOverMusic;
    public GameObject LevelCompleteMusic;
    public AudioClip CoinSound;
    private void Start()
    {
        GameOverPanel.SetActive(false);
        instance = this;

    }
    public void OnClickRetry() 
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void OnClickMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }
    public void OnClickExit()
    {
        Time.timeScale = 1;
        Application.Quit();
    }
    //Events

    public void OnGameOver() 
    {
        GameOverPanel.SetActive(true);
        GameOverMusic.SetActive(true);
        Time.timeScale = 0;
    }
    public void OnLevelComplete()
    {
        LevelComplete.SetActive(true);
        LevelCompleteMusic.SetActive(true);
        Time.timeScale = 0;
    }
    public void OnCoinPickup() 
    {
        AudioManager.Instance.PlaySFX(CoinSound);
    }
}
