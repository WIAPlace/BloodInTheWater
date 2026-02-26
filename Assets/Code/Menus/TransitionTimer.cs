using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// 
/// Author: Marshall Turner
/// Created: 2/25/26
/// Purpose: A timer before the scene changes be itself
/// 
/// Edited: 
/// Edited By: 
/// Edit Purpose: 
///
public class TransitionTimer : MonoBehaviour
{
    public TransistionScene transistion;
    public float time;
    IEnumerator SceneWait()
    {
        yield return new WaitForSeconds(time);
        transistion.StartGame();
    }

    public void Start()
    {
        StartCoroutine(SceneWait());
    }
}
