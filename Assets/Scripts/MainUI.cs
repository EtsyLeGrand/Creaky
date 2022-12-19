using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainUI : MonoBehaviour
{
    [SerializeField] private GameObject inspectSection;
    [SerializeField] private GameObject interactSection;

    private void Start()
    {
        EventManager.StartListening("OnLookingAtInspectable", OnLookingAtInspectable);
        EventManager.StartListening("OnNotLookingAtInspectable", OnNotLookingAtInspectable);

        EventManager.StartListening("OnLookingAtInteractable", OnLookingAtInteractable);
        EventManager.StartListening("OnNotLookingAtInteractable", OnNotLookingAtInteractable);
    }

    private void OnLookingAtInspectable(Dictionary<string, object> _)
    {
        inspectSection.SetActive(true);
    }

    private void OnNotLookingAtInspectable(Dictionary<string, object> _)
    {
        inspectSection.SetActive(false);
    }

    private void OnLookingAtInteractable(Dictionary<string, object> _)
    {
        interactSection.SetActive(true);
    }

    private void OnNotLookingAtInteractable(Dictionary<string, object> _)
    {
        interactSection.SetActive(false);
    }
}
