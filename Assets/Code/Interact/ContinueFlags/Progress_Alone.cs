using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Progress_Alone : IProgressFlag
{
    public bool Progress()
    {
        return GameState.Instance.CheckOnBoard();
    }

    
}
