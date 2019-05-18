﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InGameMenuController : MonoBehaviour
{
    public void BackToGame()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    public void ShowSettings()
    {

    }

    public void BackToMenu()
    {
        Time.timeScale = 1;
        //TODO: save progress
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }

    public void QuitGame()
    {
        Time.timeScale = 1;
        //TODO: save progress
        Application.Quit();
    }
}
