using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FourthRoomDresser : InteractableBase
{
    public override void Interact()
    {
        if (ProgressManager.PlayerHasItem(ProgressManager.Item.GoldenKey))
        {
            DialogueManager.DisplayMessage("I've acquired a [Crowbar]");
            ProgressManager.AddToInventory(ProgressManager.Item.Crowbar);
        }
        else
        {
            DialogueManager.DisplayMessage("It's locked.");
            GetComponent<Interactable>().RevertInspectKey();
        }
    }
}
