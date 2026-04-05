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
    [field: SerializeField][Tooltip("Reel In Speed")]
    public float ReelIn {get; private set;} 

    [field: SerializeField]
    public ParticleSystem BigSplash { get; private set; }
    [field: SerializeField]
    public ParticleSystem SmallSplash { get; private set; }

    [HideInInspector]
    public float LureRadius { get; private set; } // how large the lure radius is
    [HideInInspector]
    public bool GetInRange = false; // checks for if the indicator is in range, if not the bobber wont be placed when left click is let go.
    [HideInInspector]
    public float CurrentCastValue; // the value of the cast at a certain point

    // Animations
    [SerializeField][Tooltip("SFX for fish grabbing bobber.")]
    public AudioClip fishHookIndicator;
    




    private bool isFishing = false;

    private void OnEnable()
    {
        Idle = new Rod_StateItemIdle();
        Readying = new Rod_StateItemReadying();
        IsReady = new Rod_StateItemIsReady();
        UseItem = new Rod_StateItemUse();
        Place = new Rod_StateItemPlace();
        UnderAtk = new Rod_StateItemUnderAttack();


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

    public void RetrieveLure(Vector3 currentLurePosition, float radius) // this will let the fish know they are no longer in lure zone
    {   
        Vector3 lurePos = currentLurePosition; 
        // Get all colliders within the sphere radius at this object's position
        Collider[] hitColliders = Physics.OverlapSphere(lurePos, radius, FishMask);
        // might want to make this non alloc version.
        
        int i = 0;
        
        while (i < hitColliders.Length)
        {
            // Try to get the TargetScript component from the hit object
            Fish_Controller target = hitColliders[i].GetComponent<Fish_Controller>();
            
            if (LurePrefab.activeSelf && target != null) // this should probably be traded out for some kind of event thing
            {
                target.SC.BobberSpooked(lurePos); 
                
            }
            else if (target != null)
            {
                target.SC.LureReeledIn();  // we should get the data here
            }
            i++;
        }
    }

    public void SpawnEffectAtPosition(Vector3 position, ParticleSystem part)
    {
        // Instantiate the prefab at the specified position with no rotation
        ParticleSystem ps = Instantiate(part, position, Quaternion.Euler(Vector3.right*-90));
        Destroy(ps,ps.main.duration);
    }
    public void RodTriggerAnimator(Useable_Controller controller,int key)
    {
        controller.anim.ResetTrigger("RodReel");
        controller.anim.ResetTrigger("RodUnReady");
        controller.anim.ResetTrigger("RodReady");
        controller.anim.ResetTrigger("RodReelingOut");
        controller.anim.ResetTrigger("RodCast");
        controller.anim.ResetTrigger("RodReelIn");

        switch (key)
        {
            case 0:
                controller.anim.SetTrigger("RodCast");
                break;

            case 1:
                controller.anim.SetTrigger("RodReady");
                break;
            
            case 2:
                controller.anim.SetTrigger("RodUnReady"); // lower the rod if we were still readying
                break;

            case 3:
                controller.anim.SetTrigger("RodReel");
                break;
            
            case 4:
                controller.anim.SetTrigger("RodReelingOut");
                break;
            case 5:
                controller.anim.SetTrigger("RodReelIn");
                break;
            default:
                break;
        }
        //Debug.Log(key);
    }

}
