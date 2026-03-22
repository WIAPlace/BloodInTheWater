using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// wes tollette
// just resume the game
public class ResumeButton : MonoBehaviour
{
    public void ResumeGame()
    {
        GameManager.Instance.HandleResume();
    }
}
