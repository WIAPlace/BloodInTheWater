using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// 
/// Author: Weston Tollette
/// Created: 2/24/26
/// Purpose: Scuba man spawns into the game
/// 
/// Edited:
/// Edited By:
/// Edit Purpose:
///
public class Scuba_StateSpawn : IBoatStomperState
{
    

    private void Spawning(Scuba_Controller scubaGuy) // choose a random spot to spawn.
    {
        int spots = scubaGuy.GetNumberOfSpots();
        int rando = Random.Range(0, spots);
        scubaGuy.transform.position = scubaGuy.GetScubaSpots(rando).transform.position;
        Debug.Log("Spawned At: "+rando);
    }   


    public IBoatStomperState DoState(Scuba_Controller SC)
    {
        Spawning(SC);
        //Spawing animation and stuff.
        return  SC.MoveState;
    }
}
