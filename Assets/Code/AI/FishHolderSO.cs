using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Weston Tollette
// holder for fish. mainly used for instantiating them dynamicly
[CreateAssetMenu(menuName = "FishHolderSO")]
public class FishHolderSO : ScriptableObject
{
    [SerializeField] [Tooltip("Array for fish holder")]
    GameObject[] fishHolder;

    public GameObject GetFish(int index)
    { // get a fish at a certain index.
        if (index >= fishHolder.Length)
        {
            return null;
        }
        
        return fishHolder[index];
    }
    public int GetLength()
    {
        return fishHolder.Length;
    }
}
