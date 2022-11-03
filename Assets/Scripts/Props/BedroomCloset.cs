using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BedroomCloset : InteractableProp, IInspectable
{
    public string PropName => "Closet";
    public string[] InspectKeys
    {
        get => new string[1]
        {
            "closet_bedroom"
        };
    }

    public new string InteractMessage => null;
    public new InteractItem[] InteractItem
    {
        get => new[]
        {
            new InteractItem("Dirty T-Shirt", InteractType.AcquireItem)
        };
    }

    public void Inspect()
    {
        //DialogueManager.Message(InspectManager.GetMessageContent(InspectKeys[0]));
    }
}
