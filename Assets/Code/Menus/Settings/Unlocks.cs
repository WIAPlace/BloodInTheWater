using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

//Weston TOllette
// unlock tracker for the game
[CreateAssetMenu(menuName = "Unlocks")]
public class Unlocks : ScriptableObject
{
    [SerializeField] [Tooltip("Array of Fish Keys for Player Prefs. \nNaming Convention: first 3 letters of name")]
    string[] fishKeys;

    [SerializeField] [Tooltip("Array of which Monsters the player has unlocked. \nNaming convention: first 3 letters of name")]
    string[] monsterKeys;

    [SerializeField] [Tooltip("Array of which levels the player has unlocked. \nOnly the max and current should be in here.")]
    string[] levelKeys;

    /////////////////////////////////////////////////////////////////////////////////////////////////// All
    public void UnlockAll()
    {
        for(int i = 0; i < fishKeys.Length; i++)
        {   // unlock fish
            SaveFishData(i,1);
        }

        for(int i = 0; i < monsterKeys.Length; i++)
        {   // unlock monsters
            //PlayerPrefs.SetInt(monsterKeys[i],0);
            SaveMonsterData(i);
        }
        // unlock levels?
        PlayerPrefs.SetInt(levelKeys[1],100);
        PlayerPrefs.SetInt(levelKeys[0],8);
        PlayerPrefs.Save();
    }

    public void ResetAll()
    {
        for(int i = 0; i < fishKeys.Length; i++)
        {   // reset fish
            //SaveFishData(i,0);
            PlayerPrefs.DeleteKey(fishKeys[i]);
        }
        for(int i = 0; i < monsterKeys.Length; i++)
        {   // reset monsters
            //PlayerPrefs.SetInt(monsterKeys[i],0);
            PlayerPrefs.DeleteKey(monsterKeys[i]);
        }

        // reset levels
        //PlayerPrefs.SetInt(levelKeys[0],0);
        //PlayerPrefs.SetInt(levelKeys[1],0);
        //PlayerPrefs.DeleteKey(monsterKeys[0]);
        //PlayerPrefs.DeleteKey(monsterKeys[1]);

        //Debug.Log("Locked");
        PlayerPrefs.Save();
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////// Fish
    // Save Fish Data
    public void SaveFishData(int key, float weight)
    {   // saves the weight to the key.
        float tempCompare = PlayerPrefs.GetFloat(fishKeys[key],0);
        if (tempCompare < weight)
        {   // only keep track of the heaviest fish caught.
            //Debug.Log(weight);
            PlayerPrefs.SetFloat(fishKeys[key],weight);
        }
    }

    // Load Fish Data
    public float LoadFishData(int key)
    {   // load data about the fish 
        return PlayerPrefs.GetFloat(fishKeys[key],0);
    }  // if 0 the fish hasnt been unlocked

    /////////////////////////////////////////////////////////////////////////////////////////////////// Monsters
    // Save monster
    public void SaveMonsterData(int key)
    { // when called this will set monster data to have seen if it hasnt already been set to that.
        if (PlayerPrefs.GetInt(monsterKeys[key], 0) == 0)
        {
            PlayerPrefs.SetInt(monsterKeys[key],1);
        }
    }
    public bool LoadMonsterData(int key)
    {
        // if true then the monster has been unlocked.v
        if (PlayerPrefs.GetInt(monsterKeys[key], 0) != 0) return true;
        else return false;
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////// Levels
    public void SaveLevelData(int key, int lvl)
    {
        if (key == 1)
        {   // if the unlocked level is greater than the current level dont set it to it.
            if(PlayerPrefs.GetInt(levelKeys[0],0)<PlayerPrefs.GetInt(levelKeys[1],0)) return;
        }
        PlayerPrefs.SetInt(levelKeys[key],lvl);
    }
    public int loadLevelData(int key)
    {   // get what level they are are on or have unlocked to.
        return PlayerPrefs.GetInt(levelKeys[key],0);
    }

}
