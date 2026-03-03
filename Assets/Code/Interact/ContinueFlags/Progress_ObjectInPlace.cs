using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Progress_ObjectInPlace : IProgressFlag
{
    [SerializeField] PersistantItemSpot itemSpot;
    public bool Progress()
    {
        bool returnBool = true;
        if (itemSpot!=null && itemSpot.spots[2] == 4)
        {
            returnBool = false;
        }
        return returnBool;
    }

    
}
