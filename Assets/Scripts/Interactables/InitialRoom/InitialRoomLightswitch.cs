using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialRoomLightswitch : InteractableBase
{
    public override void Interact()
    {
        DialogueManager.DisplayMessage("... Nothing happened.");
    }
}
