using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransitionManager : Singleton<TransitionManager>
{
    [SerializeField] private Image fade;
    [SerializeField] private float timeToFade;
    [SerializeField] private List<GameObject> objectsToDisableOnTransition = new List<GameObject>();
    private static bool fadingIn = false; // Transp. to black
    private static bool fadingOut = false; // Black to transp

    public static bool FadingIn { get => fadingIn; }
    public static bool FadingOut { get => fadingOut; }

    public static IEnumerator TransitionToPeephole(PeepholeController peepholeController)
    {
        fadingIn = true;
        DisableRequired();
        Instance.fade.gameObject.SetActive(true);
        float timer = 0.0f;
        while (timer <= Instance.timeToFade)
        {
            timer += Time.deltaTime;
            Instance.fade.color = new Color(Instance.fade.color.r, Instance.fade.color.g, Instance.fade.color.b, timer / Instance.timeToFade);
            yield return null;
        }

        fadingIn = false;
        Camera.main.enabled = false;
        peepholeController.GetComponent<Camera>().enabled = true;
        peepholeController.enabled = true;
        fadingOut = true;

        timer = Instance.timeToFade;
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            Instance.fade.color = new Color(Instance.fade.color.r, Instance.fade.color.g, Instance.fade.color.b, timer / Instance.timeToFade);
            yield return null;
        }
        Instance.fade.gameObject.SetActive(false);
        fadingOut = false;
    }

    public static IEnumerator SwitchToPlayerFromPeephole(PeepholeController peepholeController)
    {
        fadingIn = true;
        Instance.fade.gameObject.SetActive(true);
        float timer = 0.0f;
        while (timer <= Instance.timeToFade)
        {
            timer += Time.deltaTime;
            Instance.fade.color = new Color(Instance.fade.color.r, Instance.fade.color.g, Instance.fade.color.b, timer / Instance.timeToFade);
            yield return null;
        }

        fadingIn = false;
        PlayerController pc = FindObjectOfType<PlayerController>();
        pc.Eyes.GetComponent<Camera>().enabled = true;
        peepholeController.GetComponent<Camera>().enabled = false;
        peepholeController.enabled = false;
        fadingOut = true;

        timer = Instance.timeToFade;
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            Instance.fade.color = new Color(Instance.fade.color.r, Instance.fade.color.g, Instance.fade.color.b, timer / Instance.timeToFade);
            yield return null;
        }

        Instance.fade.gameObject.SetActive(false);
        fadingOut = false;
        EnableRequired();
        pc.Unlock();
    }

    private static void DisableRequired()
    {
        foreach (GameObject obj in Instance.objectsToDisableOnTransition)
        {
            obj.SetActive(false);
        }
    }

    private static void EnableRequired()
    {
        foreach (GameObject obj in Instance.objectsToDisableOnTransition)
        {
            obj.SetActive(true);
        }
    }
}
