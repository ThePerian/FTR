using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InGameMenu : MonoBehaviour
{
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
