using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FourthRoomExitDoor : InteractableBase
{
    public override void Interact()
    {
        if (ProgressManager.PlayerHasItem(ProgressManager.Item.Axe))
        {
            SceneManager.LoadScene("WinScene");
        }
        else
        {
            DialogueManager.DisplayMessage("I can't do that yet.");
            GetComponent<Interactable>().RevertInspectKey();
        }
    }
}
