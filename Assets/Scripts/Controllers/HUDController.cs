using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    public GameObject charListButton;
    public GameObject inventoryButton;
    public GameObject menuButton;
    public GameObject charListScene;
    public GameObject inventoryScene;
    public GameObject menuCanvas;
    public GameObject player;

    void Start()
    {
        charListButton.GetComponentInChildren<Button>().onClick.AddListener(CharListOnClick);
        inventoryButton.GetComponentInChildren<Button>().onClick.AddListener(InventoryOnClick);
        menuButton.GetComponentInChildren<Button>().onClick.AddListener(MenuOnClick);
    }
    
    void Update()
    {
        
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
