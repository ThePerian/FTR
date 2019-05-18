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

    public void ToggleCharacterList()
    {
        //we don't need both panels active
        if (inventoryPanel.activeInHierarchy)
            inventoryPanel.SetActive(false);
        //stop time only if it wasn't stopped by other panel earlier
        else if (!menuPanel.activeInHierarchy)
            Time.timeScale = Time.timeScale == 0 ? 1 : 0;
        //using one button to switch inventory screen on and off
        characterPanel.SetActive(!characterPanel.activeInHierarchy);
    }

    public void ToggleInventory()
    {
        //we don't need both panels active
        if (characterPanel.activeInHierarchy)
            characterPanel.SetActive(false);
        //stop time only if it wasn't stopped by other panel earlier
        else if (!menuPanel.activeInHierarchy)
            Time.timeScale = Time.timeScale == 0 ? 1 : 0;
        //using one button to switch inventory screen on and off
        inventoryPanel.SetActive(!inventoryPanel.activeInHierarchy);
    }

    public void ToggleInGameMenu()
    {
        //button can be pressed both when in menu and when in game
        //so we need to change it's behaviour accordingly
        menuPanel.SetActive(!menuPanel.activeInHierarchy);
        if (!characterPanel.activeInHierarchy && !inventoryPanel.activeInHierarchy)
            Time.timeScale = Time.timeScale == 0 ? 1 : 0;
    }

    void ManagePlayerInput()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleInGameMenu();
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            ToggleInventory();
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            ToggleCharacterList();
        }
    }
}
