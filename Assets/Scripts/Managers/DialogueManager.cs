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
    private static float customMinDisplayTime = -1.0f;

    [SerializeField] private PlayerController playerController;

    private void Update()
    {
        if (isMessageActive)
        {
            timer += Time.deltaTime;
            float tempMinDisplayTime = minDisplayTime;

            if (HasCustomMinDisplayTime())
            {
                tempMinDisplayTime = customMinDisplayTime;
            }

            if ((timer >= tempMinDisplayTime && (Input.GetAxis("Horizontal") != 0f || Input.GetAxis("Vertical") != 0f)) 
                || timer >= Instance.displayTime 
                || Input.GetKeyDown(KeyCode.Space))
            {
                HideMessage();
                customMinDisplayTime = -1.0f;
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

    public static void DisplayMessage(string message, float customMin)
    {
        DisplayMessage(message);
        customMinDisplayTime = customMin;
    }

    private static void HideMessage()
    {
        isMessageActive = false;
        Instance.dialogueCanvas.SetActive(false);
        Instance.playerController.Unlock();
    }

    private static bool HasCustomMinDisplayTime()
    {
        return customMinDisplayTime != -1.0f;
    }
}
