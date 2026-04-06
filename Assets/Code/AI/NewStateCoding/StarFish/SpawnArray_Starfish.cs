using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
// weston tollette
// code for the starfish to spawn at a random location amoung a group of arrays.
public class SpawnArray_Starfish : MonoBehaviour
{
    [SerializeField][Tooltip("Starfish Spawn locations")]
    private Transform[] spawnSpots;

    [SerializeField]
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        SpawnInRandomLocation();
        GameManager.Instance.unlocks.SaveMonsterData(0);
    }

    public void SpawnInRandomLocation()
    {
        int rando = Random.Range(0,spawnSpots.Length);
        transform.SetPositionAndRotation(spawnSpots[rando].position,spawnSpots[rando].rotation);

        switch (rando)
        {
            case 0:
                anim.SetTrigger("LeftSideGrab");
                break;
            case 1:
                anim.SetTrigger("RightSideGrab");
                break;
            case 2:
                anim.SetTrigger("BackGrab");
                break;
            case 3:
                anim.SetTrigger("FrontGrab");
                break;

            default:
                
                break;
        }
    }
    
}
