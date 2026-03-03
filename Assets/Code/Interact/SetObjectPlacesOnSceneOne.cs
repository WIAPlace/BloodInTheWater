using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetObjectPlacesOnSceneOne : MonoBehaviour
{
    [SerializeField] private PersistantItemSpot spots;
    void Awake()
    {
        spots.spots[1] = 0;
        spots.spots[2] = 3;
    }
}
