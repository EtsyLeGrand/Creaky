using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : Singleton<Inventory>
{
    private List<string> items;
    public static void AddItem(string itemName)
    {
        Instance.items.Add(itemName);
    }

    public static void RemoveItem(string itemName)
    {
        if (HasItem(itemName))
        {
            Instance.items.Remove(itemName);
        }
    }

    public static bool HasItem(string itemName)
    {
        return Instance.items.Contains(itemName);
    }
}
