using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItem
{
    public float weight;
    public float volume;
    public string fullName;
    public string description;
    public int price;
    public Sprite icon;

    public InventoryItem(string itemName, float itemWeight, int itemPrice)
    {
        fullName = itemName;
        weight = itemWeight;
        price = itemPrice;
    }

    public InventoryItem() { }
}
