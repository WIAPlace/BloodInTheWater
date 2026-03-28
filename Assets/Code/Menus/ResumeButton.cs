using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// wes tollette
// just resume the game
public class ResumeButton : MonoBehaviour
{
    public void ResumeGame()
    {   // when leave pause menue through resume button resume
        GameManager.Instance.HandleResume();
        GameManager.Instance.input.SetGameplay();
    }
}
