using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialRoomPeephole : PeepholeController
{
    [SerializeField] private float timeToKnock = 2.0f;
    private int timesAccessed = 0;
    protected override void OnEnable()
    {
        timesAccessed++;

        if (timesAccessed == 2)
        {
            Dictionary<string, object> args = new Dictionary<string, object>();

            EventManager.TriggerEvent("EnteredFirstPeepholeSecondTimeEvent", args);
        }

        base.OnEnable();
    }

    protected override void OnDisable()
    {
        if (timesAccessed == 1)
        {
            Dictionary<string, object> args = new Dictionary<string, object>();
            args.Add("door", linkedDoor);
            args.Add("time", timeToKnock);

            EventManager.TriggerEvent("LeftFirstPeepholeEvent", args);
        }
        else if (timesAccessed == 2)
        {
            EventManager.TriggerEvent("LeftFirstPeepholeSecondTimeEvent", new Dictionary<string, object>());
        }

        base.OnDisable();
    }
}
