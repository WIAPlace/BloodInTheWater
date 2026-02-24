using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// 
/// Author: Weston Tollette
/// Created: 2/22/26
/// Purpose: Rework of the TestLure stuff into a new singleton fishing system Utilizing Finite State System stuff for coding.
/// 
/// Edited:
/// Edited By:
/// Edit Purpose:
///
public class FishingSystem : MonoBehaviour
{
    public static FishingSystem Instance {get;set;}
    
    private void Awake()
    { // makes sure this is the only instance of this system.
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }


}
