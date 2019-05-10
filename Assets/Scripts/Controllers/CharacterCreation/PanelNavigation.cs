using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelNavigation : MonoBehaviour
{
    [Header("Panel navigation")]
    public Button leftPanelButton;
    public Button rightPanelButton;
    [Header("Panels")]
    public GameObject[] panels;

    int currentPanel;

    private void Awake()
    {
        leftPanelButton.interactable = false;
        currentPanel = 0;
        panels[currentPanel].GetComponent<Animator>().SetInteger("Position", 0);
        for (int i = 1; i < panels.Length; i++)
            panels[i].GetComponent<Animator>().SetInteger("Position", 1);
    }
    
    void Update()
    {
        leftPanelButton.interactable = (currentPanel == 0) ? false : true;
        rightPanelButton.interactable = (currentPanel == panels.Length - 1) ? false : true;
    }

    public void MoveLeft()
    {
        panels[currentPanel].GetComponent<Animator>().SetInteger("Position", 1);
        currentPanel--;
        panels[currentPanel].GetComponent<Animator>().SetInteger("Position", 0);
    }

    public void MoveRight()
    {
        panels[currentPanel].GetComponent<Animator>().SetInteger("Position", -1);
        currentPanel++;
        panels[currentPanel].GetComponent<Animator>().SetInteger("Position", 0);
    }
}
