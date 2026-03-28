using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Progress_ObjectInPlace : IProgressFlag
{
    
    public bool Progress()
    {
        bool returnBool = true;
        
        if (GameManager.Instance!=null)
        {
            int spot = 0;
            spot = GameManager.Instance.GetItemInSpot();
        
            if (spot == 3)
            {
                returnBool = false;
            }
        }
        return returnBool;
    }

    
}
