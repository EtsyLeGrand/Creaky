using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomChangerTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            ProgressManager.NextRoom();
            EventManager.TriggerEvent("OnEnterThirdRoom", new Dictionary<string, object>());
        }
    }
}
