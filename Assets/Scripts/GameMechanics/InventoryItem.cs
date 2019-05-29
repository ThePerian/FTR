using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItem
{
    public enum ItemType
    {
        Helmet,
        Mask,
        Flashlight,
        Suit,
        PrimaryWeapon,
        SecondaryWeapon,
        Knife,
        Detector,
        Bag,
        Vest,
        Backpack,
        Artifact,
        Container,
        Other
    }

    public float weight;
    public float volume;
    public string fullName;
    public string description;
    public int price;
    public Sprite icon;
    public ItemType type;

    public InventoryItem(string itemName, float itemWeight, int itemPrice)
    {
        fullName = itemName;
        weight = itemWeight;
        price = itemPrice;
        type = ItemType.Other;
    }

    public InventoryItem() { }
}
