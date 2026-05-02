using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    [SerializeField] TransistionScene transition; // used to change to the

    public void GameEnd(string scene) // transition to scene for either game loose or win
    {
        transition.StartGameScene(scene);
    }
}
