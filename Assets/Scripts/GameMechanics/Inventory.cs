using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    public int Count { get { return CountItems(); } }

    public InventoryItem helmet;
    public InventoryItem mask;
    public InventoryItem flashlight;
    public Suit body;
    public Weapon primaryWeapon;
    public Weapon secondaryWeapon;
    public ItemHolder vest;
    public ItemHolder bag;
    public ItemHolder backpack;
    public InventoryItem leftBelt;
    public InventoryItem rightBelt;
    public InventoryItem leftPocket;
    public InventoryItem rightPocket;
    public InventoryItem detector;
    public Weapon meleeWeapon;
    public ItemHolder container;
    public int boltCount;
    public ItemHolder quickSlots;
    public ItemHolder lootSlots;

    public Inventory()
    {
        backpack = new ItemHolder(12);
        quickSlots = new ItemHolder(18);
        lootSlots = new ItemHolder(12);
    }

    public void Add(InventoryItem item)
    {
        backpack.Add(item);
    }

    public void Remove(InventoryItem item)
    {
        backpack.RemoveFirst(item);
    }

    public InventoryItem[] GetInventoryItems()
    {
        return backpack.items;
    }

    int CountItems()
    {
        int result = 0;
        result += backpack.items.Length;

        return result;
    }
}
