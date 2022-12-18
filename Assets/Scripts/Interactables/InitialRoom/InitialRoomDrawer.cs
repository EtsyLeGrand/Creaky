using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialRoomDrawer : InteractableBase
{
    public override void Interact()
    {
        GetComponent<Animator>().Play("closepush_01");
    }
}
