using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI; 
using TMPro;
using UnityEngine.Splines;
using QuickTime;
using UnityEngine.EventSystems;
using UnityEngine.AI;
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
    [SerializeField] private GameObject selectedOption; // on pause what option should be selected
    [SerializeField] private GameObject howToPlay; // how to play.
    [SerializeField] private GameObject settingsMenu; // menu for settings
    [SerializeField] private GameObject gameUI; // ui for the game during play
    [SerializeField] private GameObject talkPanel; // this is the dialouge thing. make sure it is active on start.
    [SerializeField] private GameObject TransitionScreen;
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
    [SerializeField][Tooltip("Array of fish")]
    FishHolderSO fishHolder;
    [SerializeField][Tooltip("FishStatsUI")]
    ShowFishStats fishStatsUI;

    private Coroutine running;
    [HideInInspector]public bool hintsEnabled = true;
    public static event System.Action OnHooked;
    public static event System.Action OnHookedCancelled;
    public static Action<float> BoatHit;
    public static event Action OnLatch;
    public static event Action OnLatchCancelled;

    [Header("Turn Off UI")]
    [SerializeField]
    private bool HideUI = false;


    void OnEnable()
    {
        // this should be active before start. in its own script it desides if it should be on
        // stuff breaks if this is done incorectly.
        talkPanel.SetActive(true);
        TransitionScreen.SetActive(true);
        
    }
    void Start()
    {
        pref.ApplySettings();
        input.PauseEvent += HandlePause;
        input.ResumeEvent += HandleResume;
        //input.CheckEvent += HandleCheck;
        
        pauseMenu.SetActive(false); // turn off pause menu and the extra menus it has
        if(howToPlay.activeSelf) howToPlay.SetActive(false);
        if(settingsMenu.activeSelf) settingsMenu.SetActive(false);
        windUpIndicator.gameObject.SetActive(false);
        if(!HideUI){
            gameUI.SetActive(true); // make the game ui active
        }
        //text.gameObject.SetActive(false);

    }

    void OnDestroy()
    {
        input.PauseEvent -= HandlePause;
        input.ResumeEvent -= HandleResume;
        //input.CheckEvent -= HandleCheck;
    }
    public void HandlePause()
    {
        windUpIndicator.gameObject.SetActive(false);
        gameUI.SetActive(false);
        if(!HideUI)
        {
            pauseMenu.SetActive(true);
            if(!pauseMainMenu.activeSelf) pauseMainMenu.SetActive(true);
        }
        //text.gameObject.SetActive(false);
        Time.timeScale=0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SetFirstButton(selectedOption);
    }
    public void HandleResume()
    {
        pref.LoadUIStates();
        if(!HideUI)
        {
            gameUI.SetActive(true);
        }
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

    public void Hooked(bool active)         // Hooked
    {
        if (active) OnHooked.Invoke();
        else OnHookedCancelled.Invoke();
    }

    public void OnBoatHit(float hitAmt)     // Boat Hit
    {
        BoatHit?.Invoke(hitAmt);
    }

    public void OnLatchActive(bool active)   // latch
    {
        if(active) OnLatch?.Invoke();
        else OnLatchCancelled?.Invoke();
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

    public void ChangeScubaMeshActive(bool change)
    {
        //scubaNavMesh.SetActive(change);
    }
    
    /////////////////////////////////////////////////////////////////////////////////////////////////// Present Caught Fish
    public void PresentFish(int key, float weight,QuickTimeType_Enum type)
    {
        fishStatsUI.SetLbs(weight, key);
        fishStatsUI.SetName(fishHolder.GetFish(key).name);
        fishStatsUI.SetType(type);
        fishStatsUI.ActivateUI();
    }

    public void SetFirstButton(GameObject selectedOpt)
    {
        EventSystem.current.SetSelectedGameObject(selectedOpt);
    }
}
