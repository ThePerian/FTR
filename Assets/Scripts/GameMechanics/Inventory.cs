using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    public int Count { get { return CountItems(); } }

    InventoryItem helmet;
    InventoryItem mask;
    InventoryItem flashlight;
    InventoryItem body;
    InventoryItem primaryWeapon;
    InventoryItem secondaryWeapon;
    InventoryItem vest;
    InventoryItem bag;
    InventoryItem backpack;
    InventoryItem leftBelt;
    InventoryItem rightBelt;
    InventoryItem leftPocket;
    InventoryItem rightPocket;
    InventoryItem detector;
    InventoryItem knife;
    InventoryItem container;
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
