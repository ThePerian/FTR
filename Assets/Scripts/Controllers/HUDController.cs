using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    public Image healthBar;
    public Image radiationBar;
    public Image fatigueBar;
    public GameObject menuPanel;
    public GameObject inventoryPanel;
    public GameObject characterPanel;

    Player player;
    float originalBarSize;

    void Start()
    {
        player = Player.Instance;
        originalBarSize = healthBar.rectTransform.rect.width;
    }
    
    void Update()
    {
        healthBar.rectTransform.SetSizeWithCurrentAnchors(
            RectTransform.Axis.Horizontal,
            originalBarSize * player.CurrentHealth / player.MaxHealth);

        radiationBar.rectTransform.SetSizeWithCurrentAnchors(
            RectTransform.Axis.Horizontal,
            originalBarSize * player.CurrentRadiation / player.MaxRadiation);

        fatigueBar.rectTransform.SetSizeWithCurrentAnchors(
            RectTransform.Axis.Horizontal,
            originalBarSize * player.currentTravelDistance / player.maxTravelDistance);

        ManagePlayerInput();
    }

    public void ShowCharacterList()
    {
        characterPanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void ShowInventory()
    {
        inventoryPanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void ShowInGameMenu()
    {
        menuPanel.SetActive(true);
        Time.timeScale = 0;
    }

    void ManagePlayerInput()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //Esc can be pressed both when in menu and when in game
            //so we need to change it's behaviour accordingly
            menuPanel.SetActive(!menuPanel.activeInHierarchy);
            Time.timeScale = Time.timeScale == 0 ? 1 : 0;
        }
    }
}
