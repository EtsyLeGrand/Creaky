using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class YouLoseUI : MonoBehaviour
{
    private void OnEnable()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void OnTryAgainButtonClicked()
    {
        SceneManager.LoadScene("Basic Bedroom");
    }

    public void OnQuitButtonClicked()
    {
        Application.Quit();
    }
}
