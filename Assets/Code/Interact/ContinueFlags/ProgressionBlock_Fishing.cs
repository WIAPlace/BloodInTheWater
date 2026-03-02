using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ProgressionBlock_Fishing : ProgressionBlock_Abs
{
    private void Start()
    {
        progressFlag = new Progress_EndFishing();
    }
}
