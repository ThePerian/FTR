using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryScreen : MonoBehaviour
{
    public GameObject inventoryCellPrefab;
    public GameObject inventoryItemPrefab;
    public Transform backpackTier1Area;
    public Sprite defaultSprite;

    Player player;

    void Awake()
    {
        player = Player.Instance;
        foreach (InventoryItem item in player.Inventory.GetInventoryItems())
        {
            GameObject newCell = Instantiate(inventoryCellPrefab);
            newCell.GetComponent<DragAndDropCell>().acceptableItems =
                new List<InventoryItem.ItemType>();
            newCell.GetComponent<Transform>().SetParent(backpackTier1Area);
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
