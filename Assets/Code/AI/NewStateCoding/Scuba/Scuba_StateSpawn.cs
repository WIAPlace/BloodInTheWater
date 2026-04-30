using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
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
        
        int spots = scubaGuy.GetNumberOfSpots(); // get number of spots
        int rando = Random.Range(0, spots); // picks a spot out of the avalible options
        //scubaGuy.transform.position = scubaGuy.GetScubaSpots(rando).transform.position;
        scubaGuy.agent.Warp(scubaGuy.GetScubaSpots(rando).transform.position);
        //Debug.Log("Spawned At: "+rando);
    }   


    public IBoatStomperState DoState(Scuba_Controller SC)
    {   
        
        return  this;
    }

    public void DoEnter(Scuba_Controller SC)
    {
        //throw new System.NotImplementedException();
        if(SC.stun != null) // clean up if he has been here before
        { // stops a corunie if its already playing
            SC.StopCoroutine(SC.stun);
        }
        SC.rb.isKinematic = true;
        SC.agent.enabled = true;

        Spawning(SC);
        //SC.active=true;
        SC.body.SetActive(true);
        //SC.gameObject.SetActive(true);
        //Debug.Log("Active");
        //Spawing animation and stuff.
        SC.SetAnimation(3);
    }

    public void DoExit(Scuba_Controller SC)
    {
        //throw new System.NotImplementedException();
        SC.SetAnimation(4);
    }
}
