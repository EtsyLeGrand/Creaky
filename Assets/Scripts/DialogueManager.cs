using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : Singleton<DialogueManager>
{
    [SerializeField] private GameObject dialogueCanvas;
    [SerializeField] private Text messageText;
    [SerializeField] private GameObject waitForClickResponse;
    [SerializeField] private GameObject waitForYesNoResponse;

    private bool isMessageOver = false;

    private void Start()
    {
        Parse("&instant|&y/n|Leave your bedroom and go in the hallway?");
    }

    private static void Parse(string message)
    {
        List<string> args = new List<string>();

        // Also need to find {}

        while (message.IndexOf("&") != -1)
        {
            int startIndex = message.IndexOf("&");
            int endIndex = message.IndexOf("|");

            args.Add(message.Substring(startIndex + 1, endIndex - startIndex - 1));

            message = message.Remove(startIndex, endIndex - startIndex + 1);
        }

        Debug.Log(message);

        foreach (string arg in args)
        {
            Debug.Log(arg);
        }

        DisplayMessage(message, args);
    }

    private static void DisplayMessage(string message, List<string> args)
    {
        Instance.dialogueCanvas.SetActive(true);

        GameObject responseMessageToDisplay;
        bool isYesNoMsg = args.Contains("y/n");
        if (isYesNoMsg)
        {
            responseMessageToDisplay = Instance.waitForYesNoResponse;
        }
        else
        {
            responseMessageToDisplay = Instance.waitForClickResponse;
        }

        bool isInstant = args.Contains("instant");
        if (isInstant)
        {
            InstantMessage(message, responseMessageToDisplay);
        }
        else
        {
            Instance.StartCoroutine(ScrollMessage(message, responseMessageToDisplay));
        }
    }

    private static IEnumerator ScrollMessage(string message, GameObject responseObj)
    {
        if (string.IsNullOrEmpty(message)) yield break;
        yield return null;
        responseObj.SetActive(true);
    }

    private static void InstantMessage(string message, GameObject responseObj)
    {
        if (string.IsNullOrEmpty(message)) return;
        Instance.messageText.text = message;
        responseObj.SetActive(true);
    }
}
