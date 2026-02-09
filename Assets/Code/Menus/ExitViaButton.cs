using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Exit via button for this test build.
public class ExitViaButton : MonoBehaviour
{
    [SerializeField] [Tooltip("Insert the Scriptable Object Input Reader")]
    private InputReader input;
    void Start()
    {
        input.PauseEvent+= HandleExit; 
    }
    private void HandleExit()
    {
        Application.Quit();
        //Debug.Log("Game is exiting"); 
    }
    
}
