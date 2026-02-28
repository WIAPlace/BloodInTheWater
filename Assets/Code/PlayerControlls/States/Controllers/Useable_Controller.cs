using System;
using System.Collections;
using UnityEngine;
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

    public UseableItem_Empty empty;
    public UseableItem_Rod rod;
    public UseableItem_Harp harp;

    
    public UseableItem_Abstract[] UseableItem { get; private set; }
    // 0 = empty
    // 1 = fishing rod
    // 2 = harpoon    

    [HideInInspector]
    public int currentItemIndex=0; // used to check what item is in the hand

    public UseableItem_Abstract currentItem; // used to hold what the current item in use is
    public IUseableState currentState; // will just be swaped out for whatever is needed at that point


    
    public float readyTime; // used for how long the readying state will last
    public float placeTime; // used in a corutine for how long it will take to place a thing
    public Coroutine running; // if not null the corutine is running

    [SerializeField] [Tooltip("used to see what state we are in")]
    private string debugCurrentStateName = "";

    // States:
    //public Abs_StateItemIdle Idle; // no contact in between action states.
    //public Abs_StateItemReadying Readying; // The process of setting up to do the thing. //press held down 
    //public Abs_StateItemIsReady IsReady; // finished readying, if let go it will do its thing
    //public Abs_StateItemUse UseItem; // If Ready On release, the object will do its thing

    void Update()
    {
        if(currentState != null)
        {
            currentState = currentItem.DoState(this);
            debugCurrentStateName = currentState.GetType().Name; //used for debuging to see name
        }
    }
    


    /////////////////////////////////////////////////////////////////////////// Change State
    public void ChangeState(IUseableState newState)
    {
        currentState.DoExit(this); // leave the prevvious state
        currentState = newState;
        currentState.DoEnter(this); // enter the new state
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

    ///////////////////////////////////////////////////////////////////////// Stop anything running.
    public void StopRunning()
    {
        if(running != null)
        {
            //Debug.Log("stoped corutine");
            StopCoroutine(running);
            running = null;
        }
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
    void Start()
    {
        UseableItem = new UseableItem_Abstract[3];

        UseableItem[0] = empty;
        UseableItem[1] = rod;
        UseableItem[2] = harp;

        currentItemIndex = 1;
        currentItem = UseableItem[currentItemIndex];
        currentState = currentItem.Idle;

    }    

    ////////////////////////////////////////////////////////////////////////// On thing happens
    void OnEnable()
    {
        EnableControlls();
    }
    void OnDisable()
    {
        DisableControlls();
    }
    void OnDestroy()
    {
        DisableControlls();
    }

    /////////////////////////////////////////////////////////////////////////// On Use Begin
    private void HandleUse()
    { // on clicking trigger of use
        //Debug.Log("Used Begun");
        if(currentItem == null)
        { // Error catcher for if we have nothing in our hands
            return;
        }

        ChangeState(currentItem.Readying);
    }

    ////////////////////////////////////////////////////////////////////////// On Use Cancled
    private void HandleUseCancelled()
    { // on let go of trigger for use
        //Debug.Log("UsedCancelled");
        if(currentItem == null)
        { // Error catcher for if we have nothing in our hands
            return;
        }

        if(currentState == currentItem.IsReady)
        {   // If the windup has occured and we are ready to inact
            ChangeState(currentItem.UseItem);
        }
        else if(currentState == currentItem.Readying)
        { // if winding up and let go early. 
            // this is mainly used to handle simple clicks not setting off the items
            ChangeState(currentItem.Idle);
        }
    }
}
