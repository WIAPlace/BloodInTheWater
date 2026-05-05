using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "FishImages")]
public class FishImagesSO : ScriptableObject
{
    [SerializeField] private Sprite[] fishImages;
    [SerializeField] private Sprite nullCatch; // if there is nothing return this

    public Sprite GetFishImage(int index)
    {
        if(index>=fishImages.Length || index < 0) return nullCatch;

        if (fishImages[index] != null)
        {
            return fishImages[index];
        }
        else return nullCatch;
    }
}
