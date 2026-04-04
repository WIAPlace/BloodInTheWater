using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Splines;
/// 
/// Author: Weston Tollette
/// Created: 2/25/26
/// Purpose: Usable Items Controller
/// 
/// Edited:
/// Edited By:
/// Edit Purpose:
///
public class Useable_Controller : MonoBehaviour
{

    [SerializeField] [Tooltip("Insert the Scriptable Object Input Reader.")]
    private InputReader input;
    public AudioSource audioSource; // sound player
    public GameObject FPCamera;
    public GameObject fakeFish; // fake fish to hold on stick
    private TestReelLine TRL;
    //public GameObject fakeFishBody; // body of it that is a child of the fake fish object
    [field:SerializeField]
    public SplineContainer reelSpline;

    public PersistantItemSpot perSpot;
    

    public UseableItem_Empty empty;
    public UseableItem_Rod rod;
    public UseableItem_Harp harp;
    

    
    public UseableItem_Abstract[] UseableItem { get; private set; }
    // 0 = empty
    // 1 = fishing rod
    // 2 = harpoon    

    [SerializeField][Tooltip("Starting Item in hand [0=nothing,1=rod,2=harpoon]")]
    private int startingItemInHand=0;
    [HideInInspector]
    public int currentItemIndex=0; // used to check what item is in the hand

    public UseableItem_Abstract currentItem; // used to hold what the current item in use is
    public IUseableState currentState; // will just be swaped out for whatever is needed at that point
    public IUseableState previousState; // used for if we need to know the state that came before another.
    
    

    
    public float readyTime; // used for how long the readying state will last
    public float placeTime; // used in a corutine for how long it will take to place a thing
    public Coroutine running; // if not null the corutine is running
    public Coroutine windUp; // corutine for wind up indicator

    // used for debuging
    [SerializeField] [Tooltip("used to see what state we are in")]
    private string debugCurrentStateName = "";
    [SerializeField] [Tooltip("used to see what state we are in")]
    private string debugPreviousStateName = "";
    //[SerializeField] public Animator anim;

    [HideInInspector]
    public bool showingFish=false;

    //public Animator mAnimator;

    // States:
    //public Abs_StateItemIdle Idle; // no contact in between action states.
    //public Abs_StateItemReadying Readying; // The process of setting up to do the thing. //press held down 
    //public Abs_StateItemIsReady IsReady; // finished readying, if let go it will do its thing
    //public Abs_StateItemUse UseItem; // If Ready On release, the object will do its thing

    void Update()
    {
        if(currentState != null)
        {
            IUseableState holder = currentItem.DoState(this);
            if(currentState != holder) 
            { // using this as a of being able to utilize change state instead of just changing current state dirrectly
                ChangeState(holder);
            }

            debugCurrentStateName = currentState.GetType().Name; //used for debuging to see name
            debugPreviousStateName = previousState?.GetType().Name; //used for debuging to see name
            if (showingFish)
            {
                ShowingFish();
            }
        }
    }
    


    /////////////////////////////////////////////////////////////////////////// Change State
    public void ChangeState(IUseableState newState)
    {
        previousState = currentState;
        currentState?.DoExit(this); // leave the prevvious state
        currentState = newState;
        currentState?.DoEnter(this); // enter the new state   
    }
    /////////////////////////////////////////////////////////////////////////// Change Item
    public void ChangeItem(int num)
    {
        if(num<UseableItem.Length) // bounds checker
        {
            currentItem = UseableItem[num];
        }
    }
    /////////////////////////////////////////////////////////////////////////// wait to change
    /// start running a corutine then once its done change to the new state
    public void StartWaitToChange(IUseableState newState, float time)
    { // will return the corutine so that later on it can be stopped
        running = StartCoroutine(WaitToChange(newState,time));
    }
    
    private IEnumerator WaitToChange(IUseableState newState, float time)
    { // wait a number of seconds till the item is ready
        yield return new WaitForSeconds(time);
        ChangeState(newState);
    }

    ///////////////////////////////////////////////////////////////////////// Stop a corutine from running.
    public void StopCo(Coroutine activeCo)
    {
        if(activeCo != null)
        {
            //Debug.Log("stoped corutine");
            StopCoroutine(activeCo);
            activeCo = null;
        }
    }

    ////////////////////////////////////////////////////////////////////////// Wind Up Cross Hair
    public void WindUpOn(float duration) // wind up on
    {
        GameManager.Instance.ActiveIndicator(true);
        windUp = StartCoroutine(GameManager.Instance.EaseIndicator(duration));
    }
    public void WindUpOff() // wind up off
    {
        StopCo(windUp);
        GameManager.Instance.ActiveIndicator(false);
    }


    ///////////////////////////////////////////////////////////////////////// Enable and disable controls
    public void EnableControlls() // enable
    {
        //Debug.Log("Enabled");
        if(input.CheckIfGameplay())
        {
            input.UseEvent += HandleUse;
            input.UseCancelledEvent += HandleUseCancelled;
        }
    }
    public void DisableControlls() // disable
    {
        //Debug.Log("Disabled");
        input.UseEvent -= HandleUse;
        input.UseCancelledEvent -= HandleUseCancelled;
    }
    
    

    ///////////////////////////////////////////////////////////////////////// On Begin
    void OnValidate()
    {
        TryGetComponent(out UseableItem_Empty empty);
        TryGetComponent(out UseableItem_Rod rod);
        TryGetComponent(out UseableItem_Harp harp);
    }
    void Awake()
    {
        TRL = GetComponent<TestReelLine>(); 

        UseableItem = new UseableItem_Abstract[3];

        UseableItem[0] = empty;
        UseableItem[1] = rod;
        UseableItem[2] = harp;

        CheckItemAtStart();

        currentItemIndex = startingItemInHand;
        currentItem = UseableItem[currentItemIndex];

        currentItem.useableMesh.SetActive(true);
        currentState = currentItem.Idle;
    }    

    void CheckItemAtStart()
    {
        for(int i = 1; i<perSpot.spots.Length; i++)
        {
            if(perSpot.spots[i]== -1)
            {
                startingItemInHand = i;
            }
        }
    }

    ////////////////////////////////////////////////////////////////////////// On thing happens
    void OnEnable()
    {
        EnableControlls();
        GameManager.OnHooked += HandleHook;
        GameManager.OnHookedCancelled += HandleHookCancelled;
    }
    void OnDisable()
    {
        DisableControlls();
        GameManager.OnHooked -= HandleHook;
        GameManager.OnHookedCancelled -= HandleHookCancelled;
    }
    void OnDestroy()
    {
        DisableControlls();
        GameManager.OnHooked -= HandleHook;
        GameManager.OnHookedCancelled -= HandleHookCancelled;
    }

    /////////////////////////////////////////////////////////////////////////// On Use Begin
    private void HandleUse()
    { // on clicking trigger of use
        //Debug.Log("Used Begun");
        if (showingFish)
        {
            StopShowingFish(); // turn off the fish and destroy its children
        }

        if(currentItem == null)
        { // Error catcher for if we have nothing in our hands
            return;
        }
        if(currentState != currentItem.IsReady)
        {
            ChangeState(currentItem.Readying);
        }
        else
        {
            ChangeState(currentItem.UseItem);
        }
    }

    ////////////////////////////////////////////////////////////////////////// On Use Cancelled
    private void HandleUseCancelled()
    { // on let go of trigger for use
        //Debug.Log("UsedCancelled");
        if(currentItem == null)
        { // Error catcher for if we have nothing in our hands
            return;
        }

        if(currentState == currentItem.IsReady)
        {   // If the windup has occured and we are ready to inact
            //Debug.Log("hit");
            ChangeState(currentItem.UseItem);
        }
        else if(currentState == currentItem.Readying)
        { // if winding up and let go early. 
            // this is mainly used to handle simple clicks not setting off the items
            ChangeState(currentItem.Idle);
            
        }
    }

    private void HandleHook()
    {
        
        if(currentState == currentItem.Readying || currentState == currentItem.Idle)//&& rod.CheckIfFishing())
        { // changes it from the reel in stuff to catching state.
            //Debug.Log("Hooked");
            ChangeState(currentItem.IsReady);
            
        }
    }
    private void HandleHookCancelled()
    {
        //Debug.Log("Hooked Cancelled");
        if(currentState == currentItem.IsReady && rod.CheckIfFishing())
        { // changes it from the reel in stuff to catching state.
            ChangeState(currentItem.Idle);
        }
    }

    ////////////////////////////////////////////////////////////////////////////// Reel In The Fake Fish 
    public void ReelInFakeFish(GameObject fish, int rot)
    {
        showingFish = false;
        // direction
        fakeFish.transform.position = reelSpline.EvaluatePosition(1,0);
        // rotation
        Vector3 forward = reelSpline.EvaluateTangent(0,1);
        // Calculate the up vector (adjust as needed for 2D or specific orientations)
        Vector3 up = reelSpline.EvaluateUpVector(0,1);

        // Set the rotation to align with the spline's direction and up vector
        fakeFish.transform.rotation = Quaternion.LookRotation(forward, up);
        
        GameObject fakeFishBody = Instantiate(fish,fish.transform.position, fish.transform.rotation);

        TRL.setRot(rot); // change the rot to what it should be now

        fakeFishBody.transform.parent = fakeFish.transform;
        
        StartCoroutine(ReelInFakeFishThenHold());
    }
    private IEnumerator ReelInFakeFishThenHold()
    {
        float i = 0;
        while (i < 1)
        {
            i+=.01f;
            fakeFish.transform.position = reelSpline.EvaluatePosition(1,i);

            Vector3 forward = reelSpline.EvaluateTangent(1,i);
            // Calculate the up vector (adjust as needed for 2D or specific orientations)
            Vector3 up = reelSpline.EvaluateUpVector(1,i);

            // Set the rotation to align with the spline's direction and up vector
            fakeFish.transform.rotation = Quaternion.LookRotation(forward, up);
            yield return new WaitForSeconds(.01f);
        }
        showingFish = true;
    }
    private void StopShowingFish()
    {
        if (showingFish)
        {
            showingFish = false;
            TRL.setRot(0); // change the rot to what it should be now
            
            foreach (Transform child in fakeFish.transform)
            {// destroy the children
                Destroy(child.gameObject);
            }
            
        }
    }
    private void ShowingFish()
    {
        fakeFish.transform.position = reelSpline.EvaluatePosition(1,1);

        Vector3 forward = reelSpline.EvaluateTangent(1,1);
        // Calculate the up vector (adjust as needed for 2D or specific orientations)
        Vector3 up = reelSpline.EvaluateUpVector(1,1);

        // Set the rotation to align with the spline's direction and up vector
        fakeFish.transform.rotation = Quaternion.LookRotation(forward, up);
    }

    public void PickUpRodHints()
    {
        //input.InteractEvent +=GiveHintForCast;
        if(GameManager.Instance.hintsEnabled)
        {
            TutorialManager.Instance.TriggerTutorial(4,0); // pickup
        }
    }
}
