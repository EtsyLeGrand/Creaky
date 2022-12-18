using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : Singleton<DialogueManager>
{
    [SerializeField] private GameObject dialogueCanvas;
    [SerializeField] private TMPro.TMP_Text messageText;
    [SerializeField] private GameObject waitForYesNoResponse;
    [SerializeField] private float displayTime;
    [SerializeField] private float minDisplayTime;

    private static float timer;
    private static bool isMessageActive = false;

    [SerializeField] private PlayerController playerController;

    private void Start()
    {
        DisplayMessage("My old computer. It's a real piece of junk.");
    }

    private void Update()
    {
        if (isMessageActive)
        {
            timer += Time.deltaTime;
            if (timer >= Instance.minDisplayTime && (Input.GetAxis("Horizontal") != 0f || Input.GetAxis("Vertical") != 0f))
            {
                HideMessage();
            }

            if (timer >= Instance.displayTime)
            {
                HideMessage();
            }
        }
    }

    public static void DisplayMessage(string message)
    {
        Instance.playerController.Lock();
        timer = 0.0f;
        isMessageActive = true;
        Instance.dialogueCanvas.SetActive(true);
        Instance.messageText.text = message;
    }

    private static void HideMessage()
    {
        isMessageActive = false;
        Instance.dialogueCanvas.SetActive(false);
        Instance.playerController.Unlock();
    }
}
