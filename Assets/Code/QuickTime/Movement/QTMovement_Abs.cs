using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// 
/// Author: Weston Tollette
/// Created: 3/20/26
/// Purpose: SO for calculations behind fish movement in the QT mini game.
/// 
/// Edited: 
/// Edited By: 
/// Edit Purpose:
/// 
//[CreateAssetMenu(menuName = "QTE_Basic")]
public abstract class QTMovement_Abs : ScriptableObject
{
    public abstract float DoMove(float location, QuickTimeData_Abstract data); // takes the location and will output a new location for it to move next frame.
    // the QTD_A is mainly just for doing corutines if thats needed
}
