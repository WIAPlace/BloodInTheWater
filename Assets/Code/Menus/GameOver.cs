using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    [SerializeField] TransistionScene transition; // used to change to the

    public void GameEnd() // transition to scene for either game loose or win
    {
        transition.StartGame();
    }
}
