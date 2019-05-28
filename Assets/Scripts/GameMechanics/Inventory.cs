using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    public int Count { get { return CountItems(); } }

    InventoryItem helmet;
    InventoryItem mask;
    InventoryItem flashlight;
    Suit body;
    Weapon primaryWeapon;
    Weapon secondaryWeapon;
    ItemHolder vest;
    ItemHolder bag;
    ItemHolder backpack;
    InventoryItem leftBelt;
    InventoryItem rightBelt;
    InventoryItem leftPocket;
    InventoryItem rightPocket;
    InventoryItem detector;
    InventoryItem knife;
    ItemHolder container;
    int boltCount;
    ItemHolder tempBag;

    public Inventory()
    {
        tempBag = new ItemHolder(12);
    }

    public void Add(InventoryItem item)
    {
        tempBag.Add(item);
    }

    public void Remove(InventoryItem item)
    {
        tempBag.RemoveFirst(item);
    }

    public InventoryItem[] GetInventoryItems()
    {
        return tempBag.items;
    }

    int CountItems()
    {
        int result = 0;
        result += tempBag.items.Length;

        return result;
    }
}
