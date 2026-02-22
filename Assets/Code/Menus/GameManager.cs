using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private InputReader input;
    [SerializeField] private GameObject pauseMenu;
    void Start()
    {
        input.PauseEvent += HandlePause;
        input.ResumeEvent += HandleResume;
        pauseMenu.SetActive(false);
    }

    void OnDestroy()
    {
        input.PauseEvent -= HandlePause;
        input.ResumeEvent -= HandleResume;
    }
    void HandlePause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale=0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    void HandleResume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale=1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
