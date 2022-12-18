using cakeslice;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inspectable : MonoBehaviour
{
    [SerializeField] private string key;
    [SerializeField] private List<Outline> outlineObject;

    public string Key { get => key; set => key = value; }
    public List<Outline> OutlineObject { get => outlineObject; set => outlineObject = value; }
    
    private void Awake()
    {
        foreach (Outline outline in outlineObject)
        {
            outline.eraseRenderer = true;
        }
    }

    public void Inspect()
    {
        if (TryGetComponent(out Interactable interactable)) interactable.WasInspected = true;

        DialogueManager.DisplayMessage(InspectManager.GetMessageContent(key));
    }
}
