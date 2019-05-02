using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public GameObject backButton;
    public GameObject settingsButton;
    public GameObject quitButton;

    void Start()
    {
        backButton.GetComponent<Button>().onClick.AddListener(BackBtnOnClick);
        settingsButton.GetComponent<Button>().onClick.AddListener(SettingsBtnOnClick);
        quitButton.GetComponent<Button>().onClick.AddListener(QuitBtnOnClick);
    }
    
    void Update()
    {
        
    }

    void BackBtnOnClick()
    {
        gameObject.SetActive(!gameObject.activeInHierarchy);
        Time.timeScale = Time.timeScale == 0 ? 1 : 0;
    }

    void SettingsBtnOnClick()
    {

    }

    void QuitBtnOnClick()
    {
        Application.Quit();
    }
}
