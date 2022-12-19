using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Inspectable))]
public class Interactable : MonoBehaviour
{
    [SerializeField] private bool requiresInspection = false;
    [SerializeField] private bool destroyOnUse = true;
    [SerializeField] private InteractableBase interactable;

    private bool wasInspected = false;
    private Inspectable inspectable;

    public bool WasInspected { set => wasInspected = value; }
    public bool DestroyOnUse { get => destroyOnUse; }

    private void Start()
    {
        inspectable = GetComponent<Inspectable>();
    }

    public void Interact()
    {
        if (CanBeInteracted())
        {
            ChangeInspectKey();
            interactable.Interact();

            if (destroyOnUse) Destroy(this);
        }
    }

    private void ChangeInspectKey()
    {
        char lastChar = inspectable.Key[inspectable.Key.Length - 1];
        if (int.TryParse(lastChar.ToString(), out int numberId))
        {
            numberId++;
            inspectable.Key = inspectable.Key.Remove(inspectable.Key.Length - 1) + numberId;
        }
    }

    public void RevertInspectKey()
    {
        char lastChar = inspectable.Key[inspectable.Key.Length - 1];
        if (int.TryParse(lastChar.ToString(), out int numberId))
        {
            numberId--;
            inspectable.Key = inspectable.Key.Remove(inspectable.Key.Length - 1) + numberId;
        }
    }

    public bool CanBeInteracted()
    {
        return (!requiresInspection || wasInspected);
    }

    public void SetInteractable(InteractableBase itrct)
    {
        interactable = itrct;
    }
}
