using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FourthRoomOven : InteractableBase
{
    public override void Interact()
    {
        ProgressManager.AddToInventory(ProgressManager.Item.OvenMitts);
        DialogueManager.DisplayMessage("I've acquired [Oven Mitts]");
    }
}
