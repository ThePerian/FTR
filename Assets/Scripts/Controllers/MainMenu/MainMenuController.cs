﻿using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public GameObject quitConfirmationCanvas;
    public Animator mainMenuButtonsAnimator;
    public Animator settingsWindowAnimator;

    bool isSettingsShown = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && isSettingsShown)
            HideSettings();
    }

    public void NewGame()
    {
        SceneManager.LoadScene("CharacterCreation", LoadSceneMode.Single);
    }

    public void ContinueGame()
    {
        //TODO: add proper loading
        SceneManager.LoadScene("GlobalMap", LoadSceneMode.Single);
    }

    public void ShowSettings()
    {
        mainMenuButtonsAnimator.SetBool("isHidden", true);
        settingsWindowAnimator.SetBool("isHidden", false);
        isSettingsShown = true;
    }

    public void HideSettings()
    {
        settingsWindowAnimator.SetBool("isHidden", true);
        mainMenuButtonsAnimator.SetBool("isHidden", false);
        isSettingsShown = false;
    }

    public void QuitGame()
    {
        quitConfirmationCanvas.SetActive(true);
    }

    public void QuitConfirmed()
    {
        Application.Quit();
    }

    public void QuitDenied()
    {
        quitConfirmationCanvas.SetActive(false);
    }
}
