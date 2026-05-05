using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectUnlocks : MonoBehaviour
{
    [SerializeField] private Button[] levelSelection;
    [SerializeField] private TMP_Text[] labels;
    [SerializeField] private Unlocks unlocks;
    private int curLevelUnlocked=0;
    // Start is called before the first frame update
    void OnEnable()
    {
        curLevelUnlocked = unlocks.loadLevelData(0);
        EnableUnlockedLevels();
    }

    private void EnableUnlockedLevels()
    {
        for(int i = 0; i < levelSelection.Length; i++)
        {
            if(i < curLevelUnlocked)
            {   // if level is unlocked make it interactable
                levelSelection[i].interactable = true;
                labels[i].color = Color.white;
            }
            else
            {   // if not make it look like and function as non interactable
                levelSelection[i].interactable = false;
                labels[i].color = Color.black;
            }
        }
    }
}
