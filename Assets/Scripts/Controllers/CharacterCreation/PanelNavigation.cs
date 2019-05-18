using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PanelNavigation : MonoBehaviour
{
    [Header("Panel navigation")]
    public Button leftPanelButton;
    public Button rightPanelButton;
    [Header("Panels")]
    public GameObject[] panels;
    public GameObject tooltipPanel;
    [Header("Warning panel")]
    public GameObject warningPanel;
    public Text warningText;
    public Button warningConfirmButton;
    public Button warningDenyButton;

    int currentPanelIndex;
    Player player;
    Vector2 tooltipOffset;

    private void Awake()
    {
        leftPanelButton.interactable = false;
        currentPanelIndex = 0;
        panels[currentPanelIndex].GetComponent<Animator>().SetInteger("Position", 0);
        for (int i = 1; i < panels.Length; i++)
            panels[i].GetComponent<Animator>().SetInteger("Position", 1);
        player = Player.Instance;
        tooltipOffset = new Vector2(2, 2);
    }
    
    void Update()
    {
        tooltipPanel.transform.position = Input.mousePosition + (Vector3)tooltipOffset;
    }

    public void MoveLeft()
    {
        panels[currentPanelIndex].GetComponent<Animator>().SetInteger("Position", 1);
        currentPanelIndex--;
        panels[currentPanelIndex].GetComponent<Animator>().SetInteger("Position", 0);
        UpdateNavigationButtons();
    }

    public void MoveRight()
    {
        panels[currentPanelIndex].GetComponent<Animator>().SetInteger("Position", -1);
        currentPanelIndex++;
        panels[currentPanelIndex].GetComponent<Animator>().SetInteger("Position", 0);
        UpdateNavigationButtons();
    }

    void UpdateNavigationButtons()
    {
        leftPanelButton.interactable = (currentPanelIndex <= 0) ? false : true;
        rightPanelButton.interactable = (currentPanelIndex >= panels.Length - 1) ? false : true;
    }

    public void ValidateCharacter()
    {
        string warningMessage = "";
        //if player lowered Knowledge after spending skill points
        //there may be negative amount, meaning there were more points spent
        //than new Knowledge value allows
        if (player.skillPointsToSpend < 0)
        {
            warningMessage = "Вы потратили больше очков навыков, чем позволяет текущее значение Знаний."
                +" Уменьшите уровень навыков, пока не получите 0 очков.";
            warningDenyButton.GetComponentInChildren<Text>().text = "OK";
            warningConfirmButton.gameObject.SetActive(false);
            warningText.text = warningMessage;
            warningPanel.SetActive(true);
            return;
        }
        //check if there're unspent points and make sure that player wants to save them for later
        if (player.statPointsToSpend > 0)
            warningMessage += "У вас остались не потраченные очки характеристик!\n";
        if (player.skillPointsToSpend > 0)
            warningMessage += "У вас остались не потраченные очки навыков!\n";
        if (player.featPointsToSpend > 0)
            warningMessage += "Вы можете взять еще одну черту!\n";
        if (warningMessage != "")
        {
            warningMessage += "\nВы уверены что хотите продолжить?";
            warningDenyButton.GetComponentInChildren<Text>().text = "НЕТ";
            warningConfirmButton.gameObject.SetActive(true);
            warningText.text = warningMessage;
            warningPanel.SetActive(true);
            return;
        }
        //if everything's ok, begin game
        GameStart();
    }

    public void GameStart()
    {
        SceneManager.LoadScene("GlobalMap", LoadSceneMode.Single);
    }

    public void CancelGameStart()
    {
        warningPanel.SetActive(false);
    }
}
