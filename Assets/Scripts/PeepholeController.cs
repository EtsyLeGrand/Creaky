using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeepholeController : MonoBehaviour
{
    [SerializeField] private GameObject linkedCanvas;
    [SerializeField] protected GameObject linkedDoor;
    [SerializeField] private float timeToShowCanvas;
    private float timer = 0.0f;

    protected virtual void OnEnable()
    {
        StartCoroutine(ShowLeaveCanvasAfterDelay());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && timer >= timeToShowCanvas)
        {
            StartCoroutine(TransitionManager.SwitchToPlayerFromPeephole(this));
            enabled = false;
        }
    }

    private IEnumerator ShowLeaveCanvasAfterDelay()
    {
        while (timer < timeToShowCanvas)
        {
            timer += Time.deltaTime;
            yield return null;
        }
        linkedCanvas.SetActive(true);
    }

    protected virtual void OnDisable()
    {
        timer = 0.0f;
        linkedCanvas.SetActive(false);
    }
}
