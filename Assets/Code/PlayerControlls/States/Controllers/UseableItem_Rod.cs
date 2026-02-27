using UnityEngine;
/// 
/// Author: Weston Tollette
/// Created: 2/25/26
/// Purpose: Abstract for all UseItem States
/// 
/// Edited:
/// Edited By:
/// Edit Purpose:
///
public class UseableItem_Rod : UseableItem_Abstract
{
    [field: SerializeField][Tooltip("The Prefab For the Lure")]
    public GameObject LurePrefab { get; private set; } // The Prefab for the lure object.
    [field: SerializeField][Tooltip("The Prefab for the Cast Spot Indicator")]
    public GameObject CastSpotPrefab { get;  set; }
    [field: SerializeField][Tooltip("Layer for Water")]
    public LayerMask WaterMask { get; private set; } // used to dictate what layer the bobber can be on
    [field: SerializeField][Tooltip("Layer for Fish")]
    public LayerMask FishMask { get; private set; } // used for activating event of fish.

    [field: Header("Rod Variables")]
    [field: SerializeField] [Tooltip("Max Cast Distance")]
    public float CastRange { get; private set; }
    [field: SerializeField][Tooltip("Cast Variance Range")]
    public float CastVary { get; private set; }
    [field: SerializeField][Tooltip("Cast Ping Pong Speed")]
    public float PongSpeed { get; private set; }
    [field: SerializeField][Tooltip("Max Cast Distance")]
    public float FearRadius { get; private set; }

    [HideInInspector]
    public float LureRadius { get; private set; } // how large the lure radius is
    [HideInInspector]
    public bool GetInRange = false; // checks for if the indicator is in range, if not the bobber wont be placed when left click is let go.
    [HideInInspector]
    public float CurrentCastValue; // the value of the cast at a certain point

    private bool isFishing = false;

    private void OnEnable()
    {
        Idle = new Rod_StateItemIdle();
        Readying = new Rod_StateItemReadying();
        IsReady = new Rod_StateItemIsReady();
        UseItem = new Rod_StateUseItem();

        LurePrefab.SetActive(false);
        CastSpotPrefab.SetActive(false); // Set inactive at the start cause why not
        LureRadius = LurePrefab.GetComponent<SphereCollider>().radius; // insures the lure radius is the same as the radius that lure trigger is.
    }
    
    public override IUseableState DoState(Useable_Controller controller)
    {
        return controller.currentState.DoState(controller);
    }
    
    public void SetIfFishing(bool check)
    {
        isFishing = check;
    }
    public bool CheckIfFishing()
    {
        return isFishing;
    }

}
