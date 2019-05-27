using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    public enum BagTier
    {
        Tier1, Tier2
    }

    public enum VestTier
    {
        Tier1, Tier2, Tier3
    }

    public enum BackpackTier
    {
        Tier1, Tier2, Tier3
    }
    
    public enum ContainerTier
    {
        Tier1, Tier2, Tier3, Tier4
    }

    public int Count { get { return tempBag.Length; } }

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
    InventoryItem[] tempBag;

    public Inventory()
    {
        tempBag = new InventoryItem[12];
    }

    public void Add(InventoryItem item)
    {
        for (int i = 0; i < tempBag.Length; i++)
        {
            if (tempBag[i] == null)
            {
                tempBag[i] = item;
                return;
            }
        }
    }

    public void Remove(InventoryItem item)
    {
        for (int i = 0; i < tempBag.Length; i++)
        {
            if (tempBag[i].fullName == item.fullName)
            {
                tempBag[i] = null;
                return;
            }
        }
    }

    public InventoryItem[] GetInventoryItems()
    {
        return tempBag;
    }
}
