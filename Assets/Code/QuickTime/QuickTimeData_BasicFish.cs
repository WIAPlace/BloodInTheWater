using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// 
/// Author: Weston Tollette
/// Created: 2/12/26
/// Purpose: Basic 1 zone hit thing.
/// 
/// Edited:
/// Edited By:
/// Edit Purpose:
/// 
public class QuickTimeData_BasicFish : QuickTimeData_Abstract
{
    private float fishLength;
    public QuickTimeData_BasicFish(QuickTimeData_BasicFish other) : base(other)
    {
        this.fishLength = other.fishLength; 
    }
}
