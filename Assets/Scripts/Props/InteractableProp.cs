using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableProp : Prop
{
    public InteractItem[] InteractItem
    {
        get;
    }
    public string InteractMessage
    {
        get;
    }
    public virtual void Interact()
    {
        foreach (InteractItem i in InteractItem)
        {
            // If player does not have required items
            if ((i.InteractType == InteractType.UseAndRemoveItem || i.InteractType == InteractType.UseItem)
                && !Inventory.HasItem(i.Item)) break;
        }

        foreach (InteractItem i in InteractItem)
        {
            switch (i.InteractType)
            {
                case InteractType.AcquireItem:
                    //DialogueManager.Message($"Picked up {i.Item}.");
                    Inventory.AddItem(i.Item);
                    break;

                case InteractType.UseItem:
                    //DialogueManager.Message($"Used {i.Item}.");
                    break;

                case InteractType.UseAndRemoveItem:
                    //DialogueManager.Message($"Used {i.Item}.");
                    Inventory.RemoveItem(i.Item);
                    break;

                case InteractType.None:
                    //DialogueManager.Message(InteractMessage);
                    break;
            }
        }
    }
}
public struct InteractItem
{
    private string item;
    private InteractType interactType;
    public string Item { get => item; }
    public InteractType InteractType { get => interactType; }

    public InteractItem(string item, InteractType interactType)
    {
        this.item = item;
        this.interactType = interactType;
    }
}

public enum InteractType
{
    None,
    AcquireItem,
    UseItem,
    UseAndRemoveItem,
}

