using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FourthRoomSink : InteractableBase
{
    public override void Interact()
    {
        if (ProgressManager.PlayerHasItem(ProgressManager.Item.OvenMitts))
        {
            DialogueManager.DisplayMessage("I've acquired a [Golden Key]");
            ProgressManager.AddToInventory(ProgressManager.Item.GoldenKey);
        }
        else
        {
            DialogueManager.DisplayMessage("I can't do that yet.");
            GetComponent<Interactable>().RevertInspectKey();
        }
    }
}
