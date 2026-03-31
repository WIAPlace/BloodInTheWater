using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// weston tollette
// code for when one of the limb colliders on the star fish gets hit.

public class LimbHit_Starfish : MonoBehaviour, IMonster
{
    [SerializeField][Tooltip("Starfish this is connected to")]
    private PrimativeStarFish body;
    public void MonsterHit(Vector3 hitDir)
    {
        body.LimbHit();
    }
}
