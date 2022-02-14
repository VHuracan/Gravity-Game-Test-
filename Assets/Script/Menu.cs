using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] private MapMover _mapMover;
    [SerializeField] private Text progress;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject endMenu;
    [SerializeField] private GameObject playButton;

    private void Awake()
    {
        Time.timeScale = 0;
    }
    

    void Update()
    {
        progress.text = "Progress: "+ Convert.ToInt32(Mathf.Abs(_mapMover.transform.position.x));
    }

    public void StartButton()
    {
        Time.timeScale = 1;
        playButton.SetActive(false);
    }
    
    public void GameOver()
    {
        Time.timeScale = 0;
        endMenu.SetActive(true);
    }
    
    public void RestartGame()
    {
        Time.timeScale = 0;
        SceneManager.LoadScene(0);
    }
    
    public void Pause()
    {
        if (Time.timeScale != 1)
        {
            Time.timeScale = 1;
            pauseMenu.SetActive(false);
        }
        else
        {
            Time.timeScale = 0;
            pauseMenu.SetActive(true);
        }
    }
}
