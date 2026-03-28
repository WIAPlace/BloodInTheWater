using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingRodCast : MonoBehaviour
{/*
    //public Animator anim;
    //[SerializeField]public InputReader input;
    public InputActionAsset InputActions;
    private InputReader M_ReadyRod;
    private InputReader M_CastRod;
    private InputReader M_ReelRod;
    
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        InputReader.UseEvent += HandleUse;
    }

    private void HandleUse()
    {

    }
    private void Awake()
    {
         M_ReadyRod = InputSystem.actions.FindAction("Ready");
         M_CastRod = InputSystem.actions.FindAction("Cast");
         M_ReelRod = InputSystem.actions.FindAction("Reel");
    }


    private void OnEnable()
    {
        InputActions.FindActionMap("Fishing").Disable()
    }
    private void Disable()
    {
        InputActions.FindActionMap("Fishing").Disable()
    }

    // Update is called once per frame
    void Update()
    {
        if (M_ReadRod.IsPressed())
        {
            anim.SetTrigger("RodReady");
            //*checks if the button is pressed across multiple frames
            Debug.Log("LMB is pressed");
        }

        if (M_CastRod.WasReleasedThisFrame())
        {
            anim.SetTrigger("RodCast");
            //*checks if button was release and will for one frame
        }

        if (M_ReelRod.WasReleasedThisFrame())
        {
            anim.SetTrigger("RodReel");
            //*checks if button was pressed and will run for one frame
            
        }
    }   
    */
}
