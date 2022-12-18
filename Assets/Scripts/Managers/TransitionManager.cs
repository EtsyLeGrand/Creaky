using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransitionManager : Singleton<TransitionManager>
{
    [SerializeField] private Image fade;
    [SerializeField] private float timeToFade;
    [SerializeField] private List<GameObject> objectsToDisableOnTransition = new List<GameObject>();
    public static IEnumerator TransitionToPeephole(PeepholeController peepholeController)
    {
        DisableRequired();
        Instance.fade.gameObject.SetActive(true);
        float timer = 0.0f;
        while (timer <= Instance.timeToFade)
        {
            timer += Time.deltaTime;
            Instance.fade.color = new Color(Instance.fade.color.r, Instance.fade.color.g, Instance.fade.color.b, timer / Instance.timeToFade);
            yield return null;
        }
        Camera.main.enabled = false;
        peepholeController.GetComponent<Camera>().enabled = true;
        peepholeController.IsActive = true;

        timer = Instance.timeToFade;
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            Instance.fade.color = new Color(Instance.fade.color.r, Instance.fade.color.g, Instance.fade.color.b, timer / Instance.timeToFade);
            yield return null;
        }
        Instance.fade.gameObject.SetActive(false);
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
