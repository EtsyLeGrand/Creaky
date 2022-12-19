using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FourthRoomFridge : InteractableBase
{
    public override void Interact()
    {
        if (ProgressManager.PlayerHasItem(ProgressManager.Item.Crowbar))
        {
            DialogueManager.DisplayMessage("I've acquired an [Axe]");
            ProgressManager.AddToInventory(ProgressManager.Item.Axe);
        }
        else
        {
            DialogueManager.DisplayMessage("The doors are stuck!");
            GetComponent<Interactable>().RevertInspectKey();
        }
    }
}
