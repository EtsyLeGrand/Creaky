using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableDoor : InteractableBase
{
    public enum DoorInteraction
    {
        Peephole,
        DoorOpenOrClose,
    }

    [SerializeField] private DoorInteraction interactionType;
    [SerializeField] private PeepholeController linkedPeephole;

    public override void Interact()
    {
        if (interactionType == DoorInteraction.Peephole)
        {
            PlayerController pc = FindObjectOfType<PlayerController>();
            pc.Lock();
            StartCoroutine(TransitionManager.TransitionToPeephole(linkedPeephole));
        }
    }

}
