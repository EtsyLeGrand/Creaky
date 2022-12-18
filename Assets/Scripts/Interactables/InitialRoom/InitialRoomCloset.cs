using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialRoomCloset : InteractableBase
{
    public override void Interact()
    {
        ProgressManager.AddToInventory(ProgressManager.Item.GoldenKey);
        DialogueManager.DisplayMessage("A [Golden Key]? What does it even open?");
    }
}
