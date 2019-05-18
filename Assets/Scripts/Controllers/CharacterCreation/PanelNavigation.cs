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

    int currentPanelIndex;
    Player player;

    private void Awake()
    {
        leftPanelButton.interactable = false;
        currentPanelIndex = 0;
        panels[currentPanelIndex].GetComponent<Animator>().SetInteger("Position", 0);
        for (int i = 1; i < panels.Length; i++)
            panels[i].GetComponent<Animator>().SetInteger("Position", 1);
        player = Player.Instance;
    }
    
    void Update()
    {
        leftPanelButton.interactable = (currentPanelIndex == 0) ? false : true;
        rightPanelButton.interactable = (currentPanelIndex == panels.Length - 1) ? false : true;
    }

    public void MoveLeft()
    {
        panels[currentPanelIndex].GetComponent<Animator>().SetInteger("Position", 1);
        currentPanelIndex--;
        panels[currentPanelIndex].GetComponent<Animator>().SetInteger("Position", 0);
    }

    public void MoveRight()
    {
        panels[currentPanelIndex].GetComponent<Animator>().SetInteger("Position", -1);
        currentPanelIndex++;
        panels[currentPanelIndex].GetComponent<Animator>().SetInteger("Position", 0);
    }

    public void BeginGame()
    {
        if (CanBegin())
            SceneManager.LoadScene("GlobalMap", LoadSceneMode.Single);
        //else show warning
    }

    bool CanBegin()
    {
        //check if all setup is ok
        return true;
    }
}
