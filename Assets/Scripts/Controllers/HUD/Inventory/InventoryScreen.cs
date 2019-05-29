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
    public DragAndDropCell suitDetailsSlot;
    public Image suitConditionBar;
    public Text suitDetailsText;
    public GameObject suitDetailPanel;
    [Header("Weapon details")]
    public Text weaponDetailsName;
    public DragAndDropCell weaponDetailsSlot;
    public Image weaponConditionBar;
    public Text weaponDetailsText;
    public GameObject weaponDetailPanel;
    [Header("Loot area")]
    public Transform lootArea;
    public Text lootSourceName;
    public Image lootSourceIcon;
    [Header("Main area")]
    public DragAndDropCell flashlightSlot;
    public DragAndDropCell helmetSlot;
    public DragAndDropCell maskSlot;
    public DragAndDropCell primaryWeaponSlot;
    public DragAndDropCell suitSlot;
    public DragAndDropCell secondaryWeaponSlot;
    public DragAndDropCell detectorSlot;
    public DragAndDropCell leftPocketSlot;
    public DragAndDropCell leftBeltSlot;
    public DragAndDropCell rightBeltSlot;
    public DragAndDropCell rightPocketSlot;
    public DragAndDropCell containerSlot;
    public Transform containerArea;
    [Header("Bags area")]
    public DragAndDropCell vestSlot;
    public Transform vest1Area;
    public Transform vest2Area;
    public Transform vest3Area;
    public DragAndDropCell bagSlot;
    public Transform bag1Area;
    public Transform bag2Area;
    public DragAndDropCell backpackSlot;
    public Transform backpack1Area;
    public Transform backpack2Area;
    public Transform backpack3Area;
    [Header("Footer zone")]
    public Transform quickSlotsArea;

    Player player;
    List<InventoryItem.ItemType> allItemTypes;
    ProgressBar weaponCondition;
    ProgressBar suitCondition;

    void Awake()
    {
        allItemTypes = new List<InventoryItem.ItemType>()
            {
                InventoryItem.ItemType.Artifact,
                InventoryItem.ItemType.Backpack,
                InventoryItem.ItemType.Bag,
                InventoryItem.ItemType.Suit,
                InventoryItem.ItemType.Container,
                InventoryItem.ItemType.Detector,
                InventoryItem.ItemType.Flashlight,
                InventoryItem.ItemType.Helmet,
                InventoryItem.ItemType.Knife,
                InventoryItem.ItemType.Mask,
                InventoryItem.ItemType.Other,
                InventoryItem.ItemType.PrimaryWeapon,
                InventoryItem.ItemType.SecondaryWeapon,
                InventoryItem.ItemType.Vest
            };

        player = Player.Instance;

        foreach (InventoryItem item in player.Inventory.GetInventoryItems())
        {
            GameObject newCell = Instantiate(inventoryCellPrefab);
            DragAndDropCell cellController = newCell.GetComponent<DragAndDropCell>();
            cellController.acceptableItems = new List<InventoryItem.ItemType>(allItemTypes);
            newCell.GetComponent<Transform>().SetParent(backpack1Area);
            AddItemToCell(item, newCell.GetComponent<DragAndDropCell>());
        }

        weaponCondition = new ProgressBar(weaponConditionBar, 1, 1);
        suitCondition = new ProgressBar(suitConditionBar, 1, 1);
    }

    private void OnEnable()
    {
        ResetScreen();
    }

    void ResetScreen()
    {
        Inventory inv = player.Inventory;

        // set header fields
        moneyField.text = $"Деньги: {player.money} RU";
        boltsField.text = $"Болты: {inv.boltCount} шт.";

        // set suit details area
        AddItemToCell(null, suitDetailsSlot);
        suitDetailsName.text = "";
        suitDetailsText.text = "";
        suitDetailPanel.SetActive(false);

        // set weapon details area
        AddItemToCell(null, weaponDetailsSlot);
        weaponDetailsName.text = "";
        weaponDetailsText.text = "";
        weaponDetailPanel.SetActive(false);

        //set loot area
        lootSourceIcon = null;
        lootSourceName.text = "Окружение";
        foreach (var child in lootArea.GetComponentsInChildren<DragAndDropCell>())
        {
            Destroy(child.gameObject);
        }
        for (int i = 0; i < inv.lootSlots.capacity; i++)
        {
            GameObject newCell = Instantiate(inventoryCellPrefab);
            DragAndDropCell cellController = newCell.GetComponent<DragAndDropCell>();
            cellController.acceptableItems = new List<InventoryItem.ItemType>(allItemTypes);
            newCell.GetComponent<Transform>().SetParent(lootArea);
            AddItemToCell(inv.lootSlots.items[i], cellController);
        }

        // set main area
        flashlightSlot.acceptableItems = new List<InventoryItem.ItemType>
        {
            InventoryItem.ItemType.Flashlight
        };
        AddItemToCell(inv.flashlight, flashlightSlot);
        helmetSlot.acceptableItems = new List<InventoryItem.ItemType>
        {
            InventoryItem.ItemType.Helmet
        };
        AddItemToCell(inv.helmet, helmetSlot);
        maskSlot.acceptableItems = new List<InventoryItem.ItemType>
        {
            InventoryItem.ItemType.Mask
        };
        AddItemToCell(inv.mask, maskSlot);
        primaryWeaponSlot.acceptableItems = new List<InventoryItem.ItemType>
        {
            InventoryItem.ItemType.PrimaryWeapon
        };
        AddItemToCell(inv.primaryWeapon, primaryWeaponSlot);
        suitSlot.acceptableItems = new List<InventoryItem.ItemType>
        {
            InventoryItem.ItemType.Suit
        };
        AddItemToCell(inv.body, suitSlot);
        secondaryWeaponSlot.acceptableItems = new List<InventoryItem.ItemType>
        {
            InventoryItem.ItemType.SecondaryWeapon
        };
        AddItemToCell(inv.secondaryWeapon, secondaryWeaponSlot);
        detectorSlot.acceptableItems = new List<InventoryItem.ItemType>
        {
            InventoryItem.ItemType.Detector
        };
        AddItemToCell(inv.detector, detectorSlot);
        leftPocketSlot.acceptableItems = new List<InventoryItem.ItemType>
        {
            InventoryItem.ItemType.Artifact,
            InventoryItem.ItemType.Detector,
            InventoryItem.ItemType.Flashlight,
            InventoryItem.ItemType.Knife,
            InventoryItem.ItemType.Other
        };
        AddItemToCell(inv.leftPocket, leftPocketSlot);
        leftBeltSlot.acceptableItems = new List<InventoryItem.ItemType>
        {
            InventoryItem.ItemType.Artifact,
            InventoryItem.ItemType.Detector,
            InventoryItem.ItemType.Flashlight,
            InventoryItem.ItemType.Knife,
            InventoryItem.ItemType.SecondaryWeapon,
            InventoryItem.ItemType.Other
        };
        AddItemToCell(inv.leftBelt, leftBeltSlot);
        rightBeltSlot.acceptableItems = new List<InventoryItem.ItemType>
        {
            InventoryItem.ItemType.Artifact,
            InventoryItem.ItemType.Detector,
            InventoryItem.ItemType.Flashlight,
            InventoryItem.ItemType.Knife,
            InventoryItem.ItemType.SecondaryWeapon,
            InventoryItem.ItemType.Other
        };
        AddItemToCell(inv.rightBelt, rightBeltSlot);
        rightPocketSlot.acceptableItems = new List<InventoryItem.ItemType>
        {
            InventoryItem.ItemType.Artifact,
            InventoryItem.ItemType.Detector,
            InventoryItem.ItemType.Flashlight,
            InventoryItem.ItemType.Knife,
            InventoryItem.ItemType.Other
        };
        AddItemToCell(inv.rightPocket, rightPocketSlot);

        // set item holders and populate respective areas
        // vest
        foreach (var child in vest1Area.GetComponentsInChildren<DragAndDropCell>())
        {
            Destroy(child.gameObject);
        }
        foreach (var child in vest2Area.GetComponentsInChildren<DragAndDropCell>())
        {
            Destroy(child.gameObject);
        }
        foreach (var child in vest3Area.GetComponentsInChildren<DragAndDropCell>())
        {
            Destroy(child.gameObject);
        }
        vest1Area.parent.gameObject.SetActive(false);
        vest2Area.parent.gameObject.SetActive(false);
        vest3Area.parent.gameObject.SetActive(false);
        vestSlot.acceptableItems = new List<InventoryItem.ItemType>
        {
            InventoryItem.ItemType.Vest
        };
        AddItemToCell(inv.vest, vestSlot);
        if (inv.vest != null)
        {
            vest1Area.parent.gameObject.SetActive(true);
            if (inv.vest.capacity > 4)
            {
                vest2Area.parent.gameObject.SetActive(true);
            }
            if (inv.vest.capacity > 8)
            {
                vest3Area.parent.gameObject.SetActive(true);
            }
            for (int i = 0; i < inv.vest.capacity; i++)
            {
                GameObject newCell = Instantiate(inventoryCellPrefab);
                DragAndDropCell cellController = newCell.GetComponent<DragAndDropCell>();
                cellController.acceptableItems = new List<InventoryItem.ItemType>()
                {
                    InventoryItem.ItemType.Artifact,
                    InventoryItem.ItemType.Detector,
                    InventoryItem.ItemType.Flashlight,
                    InventoryItem.ItemType.Knife,
                    InventoryItem.ItemType.Other
                };
                if (i < 4)
                {
                    newCell.GetComponent<Transform>().SetParent(vest1Area);
                }
                else if (i < 8)
                {
                    newCell.GetComponent<Transform>().SetParent(vest2Area);
                }
                else
                {
                    newCell.GetComponent<Transform>().SetParent(vest3Area);
                }
                AddItemToCell(inv.vest.items[i], cellController);
            }
        }
        // bag
        foreach (var child in bag1Area.GetComponentsInChildren<DragAndDropCell>())
        {
            Destroy(child.gameObject);
        }
        foreach (var child in bag2Area.GetComponentsInChildren<DragAndDropCell>())
        {
            Destroy(child.gameObject);
        }
        bag1Area.parent.gameObject.SetActive(false);
        bag2Area.parent.gameObject.SetActive(false);
        bagSlot.acceptableItems = new List<InventoryItem.ItemType>
        {
            InventoryItem.ItemType.Bag
        };
        AddItemToCell(inv.bag, bagSlot);
        if (inv.bag != null)
        {
            bag1Area.parent.gameObject.SetActive(true);
            if (inv.bag.capacity > 6)
            {
                bag2Area.parent.gameObject.SetActive(true);
            }
            for (int i = 0; i < inv.bag.capacity; i++)
            {
                GameObject newCell = Instantiate(inventoryCellPrefab);
                DragAndDropCell cellController = newCell.GetComponent<DragAndDropCell>();
                cellController.acceptableItems = new List<InventoryItem.ItemType>(allItemTypes);
                if (i < 6)
                {
                    newCell.GetComponent<Transform>().SetParent(bag1Area);
                }
                else
                {
                    newCell.GetComponent<Transform>().SetParent(bag2Area);
                }
                AddItemToCell(inv.bag.items[i], cellController);
            }
        }
        // backpack
        foreach (var child in backpack1Area.GetComponentsInChildren<DragAndDropCell>())
        {
            Destroy(child.gameObject);
        }
        foreach (var child in backpack2Area.GetComponentsInChildren<DragAndDropCell>())
        {
            Destroy(child.gameObject);
        }
        foreach (var child in backpack3Area.GetComponentsInChildren<DragAndDropCell>())
        {
            Destroy(child.gameObject);
        }
        backpack1Area.parent.gameObject.SetActive(false);
        backpack2Area.parent.gameObject.SetActive(false);
        backpack3Area.parent.gameObject.SetActive(false);
        backpackSlot.acceptableItems = new List<InventoryItem.ItemType>
        {
            InventoryItem.ItemType.Backpack
        };
        AddItemToCell(inv.backpack, backpackSlot);
        if (inv.backpack != null)
        {
            backpack1Area.parent.gameObject.SetActive(true);
            if (inv.backpack.capacity > 12)
            {
                backpack2Area.parent.gameObject.SetActive(true);
            }
            if (inv.backpack.capacity > 24)
            {
                backpack3Area.parent.gameObject.SetActive(true);
            }
            for (int i = 0; i < inv.backpack.capacity; i++)
            {
                GameObject newCell = Instantiate(inventoryCellPrefab);
                DragAndDropCell cellController = newCell.GetComponent<DragAndDropCell>();
                cellController.acceptableItems = new List<InventoryItem.ItemType>(allItemTypes);
                if (i < 12)
                {
                    newCell.GetComponent<Transform>().SetParent(backpack1Area);
                }
                else if (i < 24)
                {
                    newCell.GetComponent<Transform>().SetParent(backpack2Area);
                }
                else
                {
                    newCell.GetComponent<Transform>().SetParent(backpack3Area);
                }
                AddItemToCell(inv.backpack.items[i], cellController);
            }
        }
        // container
        foreach (var child in containerArea.GetComponentsInChildren<DragAndDropCell>())
        {
            Destroy(child.gameObject);
        }
        containerSlot.acceptableItems = new List<InventoryItem.ItemType>
        {
            InventoryItem.ItemType.Container
        };
        AddItemToCell(inv.container, containerSlot);
        if (inv.container != null)
        {
            for (int i = 0; i < inv.container.capacity; i++)
            {
                GameObject newCell = Instantiate(inventoryCellPrefab);
                DragAndDropCell cellController = newCell.GetComponent<DragAndDropCell>();
                cellController.acceptableItems = new List<InventoryItem.ItemType>()
                {
                    InventoryItem.ItemType.Artifact
                };
                newCell.GetComponent<Transform>().SetParent(containerArea);
                AddItemToCell(inv.container.items[i], cellController);
            }
        }

        // populate quick slot area
        foreach (var child in quickSlotsArea.GetComponentsInChildren<DragAndDropCell>())
        {
            Destroy(child.gameObject);
        }
        for (int i = 0; i < inv.quickSlots.capacity; i++)
        {
            GameObject newCell = Instantiate(inventoryCellPrefab);
            DragAndDropCell cellController = newCell.GetComponent<DragAndDropCell>();
            cellController.acceptableItems = new List<InventoryItem.ItemType>(allItemTypes);
            newCell.GetComponent<Transform>().SetParent(quickSlotsArea);
            AddItemToCell(inv.quickSlots.items[i], cellController);
        }

        
    }

    void AddItemToCell(InventoryItem item, DragAndDropCell cell)
    {
        if (cell == null)
            return;

        if (item == null)
        {
            DragAndDropItem itemController = cell.GetComponentInChildren<DragAndDropItem>();
            if (itemController != null)
                Destroy(itemController.gameObject);
            return;
        }

        GameObject newItem = Instantiate(inventoryItemPrefab);
        newItem.GetComponent<DragAndDropItem>().item = item;
        newItem.GetComponent<Image>().sprite = item.icon ?? defaultSprite;
        newItem.GetComponent<Transform>().SetParent(cell.GetComponent<Transform>());
    }

    void OnSimpleDragAndDropEvent(DragAndDropCell.DropEventDescriptor desc)
    {
        
    }
}
