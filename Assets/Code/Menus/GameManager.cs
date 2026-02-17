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
    }

    void OnDestroy()
    {
        input.PauseEvent -= HandlePause;
        input.ResumeEvent -= HandleResume;
    }
    void HandlePause()
    {
        pauseMenu.SetActive(true);
    }
    void HandleResume()
    {
        pauseMenu.SetActive(false);
    }
}
