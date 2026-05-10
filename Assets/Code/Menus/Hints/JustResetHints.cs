using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// simply reset all tutorials, used when press start game
public class JustResetHints : MonoBehaviour
{
    [SerializeField] HintArray hintArray;
    [SerializeField] TransistionScene tranScene;
    public void ResetAllTutorials()
    {
        if(hintArray.HintIcons==null) return;
        for(int type = 0; type < hintArray.HintIcons.Length; type++)
        {
            if(hintArray.HintIcons[type]!=null){
                for(int hint = 0; hint < hintArray.HintIcons[type].Length;hint++)
                {
                    if (hintArray.HintIcons[type][hint] != null)
                    {
                        string id = hintArray.HintIcons[type][hint].name;
                        //Debug.Log(id);
                        PlayerPrefs.DeleteKey(id);
                        //Debug.Log(id);
                    }
                }
            }
        }
        PlayerPrefs.Save();
        tranScene.StartGame();
    }
}
