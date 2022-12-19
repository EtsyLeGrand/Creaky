using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdRoomExitDoor : InteractableBase
{
    public override void Interact()
    {
        ProgressManager.NextRoom();
    }
}
