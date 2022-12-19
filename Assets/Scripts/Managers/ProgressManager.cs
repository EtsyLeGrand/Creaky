using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressManager : Singleton<ProgressManager>
{
    public enum Item
    {
        GoldenKey,
    }
    private static List<Item> inventory = new List<Item>();
    [SerializeField] private GameObject darkRapax;
    [SerializeField] private List<GameObject> rooms;
    private GameObject currentRoom;
    private int currentRoomIndex = 0;
    private GameObject currentRapax;

    private void Start()
    {
        currentRoom = rooms[0];
        EventManager.StartListening("LeftFirstPeepholeEvent", LeftFirstPeepholeEvent);
        EventManager.StartListening("EnteredFirstPeepholeSecondTimeEvent", EnteredFirstPeepholeSecondTimeEvent);
        EventManager.StartListening("LeftFirstPeepholeSecondTimeEvent", LeftFirstPeepholeSecondTimeEvent);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad0))
        {
            NextRoom();
        }
    }

    public static void AddToInventory(Item item)
    {
        if (!inventory.Contains(item))
        {
            inventory.Add(item);
        }
    }

    public static void UseFromInventory(Item item)
    {
        if (inventory.Contains(item))
        {
            inventory.Remove(item);
            return;
        }

        throw new System.Exception("The item was absent from the inventory. Aborting.");
    }

    public static bool PlayerHasItem(Item item)
    {
        return inventory.Contains(item);
    }

    public static void NextRoom()
    {
        Instance.currentRoomIndex++;
        Destroy(Instance.currentRapax);
        Destroy(Instance.currentRoom);
        Instance.currentRoom = Instantiate(Instance.rooms[Instance.currentRoomIndex], Vector3.zero, Quaternion.identity);
    }

    private void LeftFirstPeepholeEvent(Dictionary<string, object> args)
    {
        StartCoroutine(LeftFirstPeepholeEventCoroutine((GameObject)args["door"], (float)args["time"]));
    }

    private IEnumerator LeftFirstPeepholeEventCoroutine(GameObject door, float timeToKnock)
    {
        float timer = 0.0f;
        while (timer <= timeToKnock)
        {
            timer += Time.deltaTime;
            yield return null;
        }
        door.GetComponent<AudioSource>().Play();
        DialogueManager.DisplayMessage("...", 2.0f);

        door.GetComponent<Inspectable>().Key = "initial_room_door_knocked";
        InteractableDoor newDoor = door.AddComponent<InteractableDoor>();
    }

    private void EnteredFirstPeepholeSecondTimeEvent(Dictionary<string, object> args)
    {
        currentRapax = Instantiate(darkRapax, new Vector3(18.7f, 1.4f, -2.76f), Quaternion.Euler(0, 180, 0));
        currentRapax.GetComponent<Animator>().Play("Walk");
        currentRapax.GetComponent<RapaxController>().WalkForward();
    }

    private void LeftFirstPeepholeSecondTimeEvent(Dictionary<string, object> args)
    {
        StartCoroutine(LeftFirstPeepholeSecondTimeEventCoroutine());
        
    }

    private IEnumerator LeftFirstPeepholeSecondTimeEventCoroutine()
    {
        while (TransitionManager.FadingIn) { yield return null; }

        NextRoom();
    }
}
