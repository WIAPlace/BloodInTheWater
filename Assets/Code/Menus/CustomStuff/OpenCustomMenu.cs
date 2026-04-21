using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// open a menu for custom stuff
public class OpenCustomMenu : MonoBehaviour, IInteractable
{
    [SerializeField]
    private InputReader input;
    [SerializeField]
    private GameObject CustomMenu;
    [SerializeField]
    private SetMenuButtonOnActive smboa;

    void Start()
    {
        CustomMenu.SetActive(false);
    }

    void OnDestroy()
    {
        input.ResumeEvent -= HandleResume;
    }

    public void Interact()
    {
        //throw new System.NotImplementedException();
        input.SetUI();
        GameManager.Instance.changeGameUI(false);
        input.ResumeEvent += HandleResume;
        CustomMenu.SetActive(true);
        Time.timeScale=0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        smboa.SetFirstButton();
    }


    public void HandleResume()
    {
        input.ResumeEvent -= HandleResume;
        input.SetGameplay();
        GameManager.Instance.changeGameUI(true);
        CustomMenu.SetActive(false);
        Time.timeScale=1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }
}
