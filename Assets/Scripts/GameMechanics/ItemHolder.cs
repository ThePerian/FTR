using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ItemHolder : InventoryItem
{
    public int capacity;
    public InventoryItem[] items;
    
    public ItemHolder(int capacity)
    {
        items = new InventoryItem[capacity];
    }

    public bool Swap(int firstPosition, int secondPosition)
    {
        try
        {
            InventoryItem temp = items[firstPosition];
            items[firstPosition] = items[secondPosition];
            items[secondPosition] = temp;
        }
        catch (Exception e) // index out of bounds
        {
            return false;
        }

        return true;
    }

    public bool Add(InventoryItem newItem)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] == null)
            {
                items[i] = newItem;
                return true;
            }
        }

        return false; // all positions are taken
    }

    public bool Add(InventoryItem newItem, int position)
    {
        try
        {
            if (items[position] == null)
            {
                items[position] = newItem;
                return true;
            }
            else
            {
                return false; // position is taken
            }
        }
        catch (Exception e)
        {
            return false; // index out of bounds
        }
    }

    public bool Remove(int position)
    {
        try
        {
            items[position] = null;
            return true;
        }
        catch (Exception e)
        {
            return false; // index out of bounds
        }
    }

    public bool RemoveFirst(InventoryItem item)
    {
        for (int i = 0; i < items.Length; i++)
        { // TODO: proper search (probably should remove this method altogether)
            if (items[i].fullName == item.fullName)
            {
                items[i] = null;
                return true;
            }
        }

        return false;
    }
}
