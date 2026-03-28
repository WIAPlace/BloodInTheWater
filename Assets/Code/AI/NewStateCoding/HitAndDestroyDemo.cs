using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// 
/// Author: Weston Tollette
/// Created: 3/2/26
/// Purpose: Demonstration for hitting something to destroy it
/// 
/// Edited:
/// Edited By:
/// Edit Purpose:
///
public class HitAndDestroyDemo : MonoBehaviour, IMonster
{
    public void MonsterHit(Vector3 hitDir)
    {
        gameObject.SetActive(false);
    }
}
