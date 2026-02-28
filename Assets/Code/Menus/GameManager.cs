using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI; 
/// 
/// Author: Weston Tollette
/// Created: 2/22/26
/// Purpose: Game Manager script
/// 
/// Edited:
/// Edited By:
/// Edit Purpose:
/// 
public class GameManager : MonoBehaviour
{
    private static GameManager GM; // static for the game manager

    [SerializeField] private InputReader input; //Input reader
    [SerializeField] private GameObject pauseMenu; // ui for the pause menu
    [SerializeField] private GameObject gameUI; // ui for the game during play
    [SerializeField] private Image windUpIndicator; // will show when u have would up and are ready to release
    void Start()
    {
        input.PauseEvent += HandlePause;
        input.ResumeEvent += HandleResume;
        pauseMenu.SetActive(false);
        gameUI.SetActive(true);
        windUpIndicator.gameObject.SetActive(false);
    }

    void OnDestroy()
    {
        input.PauseEvent -= HandlePause;
        input.ResumeEvent -= HandleResume;
    }
    void HandlePause()
    {
        gameUI.SetActive(false);
        pauseMenu.SetActive(true);
        windUpIndicator.gameObject.SetActive(false);
        Time.timeScale=0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    void HandleResume()
    {
        gameUI.SetActive(true);
        pauseMenu.SetActive(false);
        windUpIndicator.gameObject.SetActive(false);
        Time.timeScale=1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    
    /////////////// Easing Stuff for Indicator //////////////////////////////////
    public IEnumerator EaseIndicator(float duration) // enumerator to call to show wind up
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            windUpIndicator.fillAmount = Mathf.Lerp(0f,1f,elapsedTime/duration);
            yield return null;
        }
        windUpIndicator.fillAmount = 1f;
    }
    public void ActiveIndicator(bool onoff)
    {
        windUpIndicator.gameObject.SetActive(onoff);
    }


    //////// singleton stuff ////////////////////////////////////
    public static GameManager Instance // accesor for the game manager singleton
    {
        get
        {
            if (GM == null)
            {
                // If the instance is null, try to find an existing instance in the scene
                GM = FindObjectOfType<GameManager>();
                if (GM == null)
                {
                    Debug.LogError("A GameManager instance is missing from the scene.");
                }
            }
            return GM;
        }
    }

    private void Awake() // ON Start make sure this is the only one
    {
        if (GM != null && GM != this)
        {
            // If another instance already exists, destroy this new one to enforce singularity
            Destroy(this.gameObject);
        }
        else
        {
            // Set the instance to this object if it's the first one
            GM = this;

            // DontDestroyOnLoad(this.gameObject); 
        }
    }
}
