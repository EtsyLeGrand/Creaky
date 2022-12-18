using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressManager : Singleton<ProgressManager>
{
    public enum Item
    {
        GoldenKey,
    }

    private static List<Item> inventory = new List<Item>();

    public static void AddToInventory(Item item)
    {
        if (!inventory.Contains(item))
        {
            inventory.Add(item);
        }
    }

    public static void UseFromInventory(Item item)
    {
        if (inventory.Contains(item))
        {
            inventory.Remove(item);
            return;
        }

        throw new System.Exception("The item was absent from the inventory. Aborting.");
    }

    public static bool PlayerHasItem(Item item)
    {
        return inventory.Contains(item);
    }
}
