using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// script to reset all player prefrence stuff;
public class DeleteAllPlayerPrefs : MonoBehaviour
{
    public void ResetAllPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save(); // Ensures immediate disk write

        Debug.Log("PlayerPrefs have been reset.");
        // Optionally reload game state or UI to reflect changes
    }
}
