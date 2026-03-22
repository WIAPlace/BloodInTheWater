using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// 
/// Author: Weston Tollette
/// Created: 3/20/26
/// Purpose: SO for calculation of a basic fish in the QT minigame
/// 
/// Edited: 
/// Edited By: 
/// Edit Purpose:
/// 
[CreateAssetMenu(menuName = "QTMove/BasicFish")]
public class QTMovement_BasicFish : QTMovement_Abs
{
    [SerializeField]private float moveSpeed;
    
    public override float DoMove(float location,QuickTimeData_Abstract data)
    {
        

        return location * moveSpeed; 
    }
}
