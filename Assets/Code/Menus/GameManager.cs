using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI; 
using TMPro;
using UnityEngine.Splines;
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

    [SerializeField] public InputReader input; //Input reader
    [SerializeField] private GameObject pauseMenu; // ui for the pause menu
    [SerializeField] private GameObject pauseMainMenu; // default menu for pause menu
    [SerializeField] private GameObject howToPlay; // how to play.
    [SerializeField] private GameObject settingsMenu; // menu for settings
    [SerializeField] private GameObject gameUI; // ui for the game during play
    [SerializeField] private Image windUpIndicator; // will show when u have would up and are ready to release
    [SerializeField] private TextMeshProUGUI text; // ui for temp text
    [SerializeField] PersistantItemSpot itemSpot;
    [field: SerializeField] public QuickTimeController_Player qtcPlayer;
    [field: SerializeField] public SplineContainer reelSpline;
    [field: SerializeField] public GameObject lureTarget; 
    [SerializeField] private TimeKeeper keptTime;
    [SerializeField] private PlayerPrefrenceScript pref;
    public Unlocks unlocks;
    [SerializeField] private GameObject scubaNavMesh;

    private Coroutine running;
    [HideInInspector]public bool hintsEnabled = true;
    public static event System.Action OnHooked;
    public static event System.Action OnHookedCancelled;
    public static Action<float> BoatHit;

    
    
    void Start()
    {
        pref.ApplySettings();
        input.PauseEvent += HandlePause;
        input.ResumeEvent += HandleResume;
        //input.CheckEvent += HandleCheck;
        pauseMenu.SetActive(false); // turn off pause menu and the extra menus it has
        if(howToPlay.activeSelf) howToPlay.SetActive(false);
        if(settingsMenu.activeSelf) settingsMenu.SetActive(false);

        gameUI.SetActive(true); // make the game ui active
        windUpIndicator.gameObject.SetActive(false);
        text.gameObject.SetActive(false);

    }

    void OnDestroy()
    {
        input.PauseEvent -= HandlePause;
        input.ResumeEvent -= HandleResume;
        //input.CheckEvent -= HandleCheck;
    }
    public void HandlePause()
    {
        gameUI.SetActive(false);
        pauseMenu.SetActive(true);
        if(!pauseMainMenu.activeSelf) pauseMainMenu.SetActive(true);
        windUpIndicator.gameObject.SetActive(false);
        text.gameObject.SetActive(false);
        Time.timeScale=0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public void HandleResume()
    {
        pref.LoadUIStates();
        gameUI.SetActive(true);
        if(pauseMenu.activeSelf) pauseMenu.SetActive(false);
        if(howToPlay.activeSelf) howToPlay.SetActive(false);
        if(settingsMenu.activeSelf) settingsMenu.SetActive(false);
        windUpIndicator.gameObject.SetActive(false);
        Time.timeScale=1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void HandleDial(bool onoff)
    {// called by dialoge box controll to turn off UI.
        gameUI.SetActive(onoff);
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


    public void SetPause(bool timeOn) // pause for doors maybe
    {
        HandlePause();
        pauseMenu.SetActive(false);
        if (timeOn)
        {
            Time.timeScale=1f;
        }
    }

    //////// Show a UI thing for measure stuff
    public IEnumerator ShowFishLbs(string lbsText)
    {
        text.gameObject.SetActive(true); // see the text (love the text)
        text.text = lbsText;
        
        yield return new WaitForSeconds(4f);

        text.text = ""; // make it empty
        text.gameObject.SetActive(false); // make it unseen
    }
    public void ShowUIText(string txt)
    {
        if (running != null)
        {
            StopCoroutine(running);
        }
        running = StartCoroutine(ShowFishLbs(txt));
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
                    //Debug.LogError("A GameManager instance is missing from the scene.");
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

    // change if the game ui is on or off, used in the custom menu script;
    public void changeGameUI(bool check)
    {
        gameUI.SetActive(check);
    }

    public int GetItemInSpot()
    {
        return itemSpot.spots[2];
    }

    public void Hooked(bool active)
    {
        if (active) OnHooked.Invoke();
        else OnHookedCancelled.Invoke();
    }

    public void OnBoatHit(float hitAmt)
    {
        BoatHit?.Invoke(hitAmt);
    }

    public float CheckTime()
    {   // equation to put the amount of time that has passed into a rotation.
        float max = keptTime.GetMaxTime();
        float time = keptTime.GetCurrentTime();
        time = (max-time)/max * 360;
        return time;
    }
    

    public void DamageBoat(float time)
    {
        keptTime.AddPenaltyTime(time);
    }

    public void UnlockAll()
    {
        unlocks.UnlockAll();
    }
    public void LockAll()
    {
        unlocks.ResetAll();
    }

    public void ChangeScubaMesActive(bool change)
    {
        //scubaNavMesh.SetActive(change);
    }
}
