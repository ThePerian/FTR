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

    public int Count { get { return tempBag.Count; } }

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
    List<InventoryItem> tempBag;

    public Inventory()
    {
        tempBag = new List<InventoryItem>();
    }

    public void Add(InventoryItem item)
    {
        tempBag.Add(item);
    }

    public void Remove(InventoryItem item)
    {
        tempBag.Remove(item);
    }

    public List<InventoryItem> GetInventoryItems()
    {
        return tempBag;
    }
}
