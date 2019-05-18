using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartingShop : MonoBehaviour
{
    public GameObject itemButtonPrefab;
    public Image itemIcon;
    public Text itemNameLabel;
    public Text itemPriceLabel;
    public Text itemWeightLabel;
    public Text itemDescriptionField;
    public Button transferButton;
    public Text playerWeightLabel;
    public Text playerMoneyLabel;
    public Transform shopArea;
    public Transform playerArea;
    public Text warningLabel;
    public Sprite defaultSprite;

    Player player;
    List<InventoryItem> shopItems;

    void Start()
    {
        player = Player.Instance;

        //TODO: add proper initialization
        shopItems = new List<InventoryItem>()
        {
            new InventoryItem("item1", 1, 10),
            new InventoryItem("item2", 2, 20),
            new InventoryItem("item3", 3, 30)
        };

        ResetScreen();
    }

    public void ShowDetails(InventoryItem item, bool buying)
    {
        itemIcon.sprite = item.icon ?? defaultSprite;
        itemNameLabel.text = item.fullName;
        itemPriceLabel.text = item.price + " RU";
        itemWeightLabel.text = item.weight + " kg";
        itemDescriptionField.text = item.description;
        transferButton.GetComponentInChildren<Text>().text = buying ? "Купить" : "Продать";
        transferButton.onClick.RemoveAllListeners();
        InventoryItem itemToTransfer = item;
        if (buying)
            transferButton.onClick.AddListener(() => BuyItem(itemToTransfer));
        else
            transferButton.onClick.AddListener(() => SellItem(itemToTransfer));
        warningLabel.text = "";
    }

    public void BuyItem(InventoryItem item)
    {
        if (player.money - item.price < 0)
        {
            warningLabel.text = "Недостаточно денег";
            return;
        }

        bool success = player.AddItemToInventory(item);
        if (success)
        {
            //do not remove items from infinite shop
            //shopItems.Remove(item);
            player.money -= item.price;
        }
        else
        {
            warningLabel.text = "Превышен допустимый вес или количество предметов"; //temp
            return;
        }

        ResetScreen();
    }

    public void SellItem(InventoryItem item)
    {
        bool success = player.RemoveItemFromInventory(item);
        if (success)
        {
            //do not add items to infinite shop
            //shopItems.Add(item);
            player.money += item.price;
        }
        else
        {
            warningLabel.text = "Не удалось продать предмет"; //temp
            return;
        }
        
        ResetScreen();
    }

    void ResetScreen()
    {
        foreach (var child in shopArea.GetComponentsInChildren<Button>())
            Destroy(child.gameObject);
        foreach (var child in playerArea.GetComponentsInChildren<Button>())
            Destroy(child.gameObject);

        foreach (var item in shopItems)
        {
            GameObject itemButton = Instantiate(itemButtonPrefab, shopArea);
            itemButton.GetComponent<Image>().sprite = item.icon ?? defaultSprite;
            if (item.icon == null)
                itemButton.GetComponentInChildren<Text>().text = item.fullName;
            InventoryItem itemToTransfer = item;
            itemButton.GetComponent<Button>().onClick.AddListener(() => ShowDetails(itemToTransfer, true));
            if (player.money <= 0)
                itemButton.GetComponent<Button>().interactable = false;
            else
                itemButton.GetComponent<Button>().interactable = true;
        }
        foreach (var item in player.Inventory)
        {
            GameObject itemButton = Instantiate(itemButtonPrefab, playerArea);
            itemButton.GetComponent<Image>().sprite = item.icon ?? defaultSprite;
            if (item.icon == null)
                itemButton.GetComponentInChildren<Text>().text = item.fullName;
            InventoryItem itemToTransfer = item;
            itemButton.GetComponent<Button>().onClick.AddListener(() => ShowDetails(itemToTransfer, false));
        }

        itemNameLabel.text = "";
        itemPriceLabel.text = "";
        itemWeightLabel.text = "";
        itemDescriptionField.text = "";
        itemIcon.sprite = defaultSprite;
        warningLabel.text = "";
        playerWeightLabel.text = player.CurrentWeight + "/" + player.MaxWeight + " kg";
        playerMoneyLabel.text = player.money + " RU";
        transferButton.onClick.RemoveAllListeners();
        transferButton.GetComponentInChildren<Text>().text = "Выберите предмет";
    }
}
