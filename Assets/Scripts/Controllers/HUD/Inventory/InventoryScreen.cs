using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryScreen : MonoBehaviour
{
    public GameObject inventoryCellPrefab;
    public GameObject inventoryItemPrefab;
    public Sprite defaultSprite;
    [Header("Header zone")]
    public Text moneyField;
    public Text boltsField;
    [Header("Suit details")]
    public Text suitDetailsName;
    public GameObject suitDetailsSlot;
    public Image suitConditionBar;
    public Text suitDetailsText;
    public GameObject suitDetailPanel;
    [Header("Weapon details")]
    public Text weaponDetailsName;
    public GameObject weaponDetailsSlot;
    public Image weaponConditionBar;
    public Text weaponDetailsText;
    public GameObject weaponDetailPanel;
    [Header("Loot area")]
    public Transform lootArea;
    public Text lootSourceName;
    public Image lootSourceIcon;
    [Header("Main area")]
    public GameObject flashlightSlot;
    public GameObject helmetSlot;
    public GameObject maskSlot;
    public GameObject primaryWeaponSlot;
    public GameObject suitSlot;
    public GameObject secondaryWeaponSlot;
    public GameObject detectorSlot;
    public GameObject leftPocketSlot;
    public GameObject leftBeltSlot;
    public GameObject rightBeltSlot;
    public GameObject rightPocketSlot;
    public GameObject containerSlot;
    public Transform containerArea;
    [Header("Bags area")]
    public GameObject vestSlot;
    public Transform vest1Area;
    public Transform vest2Area;
    public Transform vest3Area;
    public GameObject bagSlot;
    public Transform bag1Area;
    public Transform bag2Area;
    public GameObject backpackSlot;
    public Transform backpack1Area;
    public Transform backpack2Area;
    public Transform backpack3Area;
    [Header("Footer zone")]
    public Transform quickSlotsArea;

    Player player;

    void Awake()
    {
        player = Player.Instance;
        foreach (InventoryItem item in player.Inventory.GetInventoryItems())
        {
            GameObject newCell = Instantiate(inventoryCellPrefab);
            newCell.GetComponent<DragAndDropCell>().acceptableItems =
                new List<InventoryItem.ItemType>();
            newCell.GetComponent<Transform>().SetParent(backpack1Area);
            if (item == null)
                continue;
            GameObject newItem = Instantiate(inventoryItemPrefab);
            newItem.GetComponent<DragAndDropItem>().item = item;
            newItem.GetComponent<Image>().sprite = item.icon ?? defaultSprite;
            newItem.GetComponent<Transform>().SetParent(newCell.GetComponent<Transform>());
        }
    }

    private void OnEnable()
    {
        
    }

    void Update()
    {
        
    }
}
