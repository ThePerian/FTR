using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    public Button charListButton;
    public Button inventoryButton;
    public Button menuButton;
    public GameObject charListScene;
    public GameObject inventoryScene;
    public GameObject menuCanvas;
    public Image healthBar;
    public Image radiationBar;
    public Image fatigueBar;

    Player player;
    float originalBarSize;

    void Start()
    {
        charListButton.onClick.AddListener(CharListOnClick);
        inventoryButton.onClick.AddListener(InventoryOnClick);
        menuButton.onClick.AddListener(MenuOnClick);
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
    }

    void CharListOnClick()
    {

    }

    void InventoryOnClick()
    {

    }

    void MenuOnClick()
    {
        menuCanvas.SetActive(!menuCanvas.activeInHierarchy);
        Time.timeScale = Time.timeScale == 0 ? 1 : 0;
    }
}
