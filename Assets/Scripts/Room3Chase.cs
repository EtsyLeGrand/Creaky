using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room3Chase : EnableAlienChase
{
    [SerializeField] GameObject doorThatLoadsRoom;
    protected override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            ThirdRoomExitDoor itrctBase = doorThatLoadsRoom.AddComponent<ThirdRoomExitDoor>();

            Interactable interactable = doorThatLoadsRoom.AddComponent<Interactable>();
            interactable.SetInteractable(itrctBase);
        }
        base.OnTriggerEnter(other);
    }
}
