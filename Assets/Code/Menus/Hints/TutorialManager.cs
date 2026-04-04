using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    private static TutorialManager TM;
    [SerializeField] private HintArray hintArray;
    [SerializeField] private InputReader input;
    [SerializeField] private GameObject hintUI;
    

    //////// singleton stuff ////////////////////////////////////
    public static TutorialManager Instance // accesor for the game manager singleton
    {
        get
        {
            if (TM == null)
            {
                // If the instance is null, try to find an existing instance in the scene
                TM = FindObjectOfType<TutorialManager>();
                if (TM == null)
                {
                    //Debug.LogError("A GameManager instance is missing from the scene.");
                }
            }
            return TM;
        }
    }

    private void Awake() // ON Start make sure this is the only one
    {
        if (TM != null && TM != this)
        {
            // If another instance already exists, destroy this new one to enforce singularity
            Destroy(this.gameObject);
        }
        else
        {
            // Set the instance to this object if it's the first one
            TM = this;
            // DontDestroyOnLoad(this.gameObject); 
        }
    }
    
    void OnDestroy()
    {
        input.InteractEvent -= CloseHint;
    }

    public void TriggerTutorial(int type, int hint)
    {
        // 1. First, check if the "gate" is even open
        if (hintArray.HintIcons[type][hint] == null) 
        {
            Debug.Log("Tutorial locked: Conditions not met yet.");
            return; 
        }
        string id = hintArray.HintIcons[type][hint].name;

        // 2. Then, check if they've already seen it (the persistent flag)
        if (PlayerPrefs.GetInt(id, 0) == 0)
        {
            ShowTutorial(type,hint);

            // 3. Mark as seen so it never shows again
            PlayerPrefs.SetInt(id, 1);
            PlayerPrefs.Save();
        }
    }

    void ShowTutorial(int type, int hint)
    {
        //Debug.Log("Showing: " + id);
        if(hintUI != null && GameManager.Instance.hintsEnabled){
            Time.timeScale = 0;
            hintUI.SetActive(true);
            hintArray.ShowHint(type,hint);
            input.InteractEvent += CloseHint; // allow player to close out the hint menu
            input.InteractEventQT += CloseHint;
        }
    }
    public void CloseHint()
    {
        if (hintUI != null && hintUI.activeSelf)
        {
            hintArray.CloseHints();
            hintUI.SetActive(false);
            input.InteractEvent -= CloseHint; // stop listening for interact
            input.InteractEventQT -= CloseHint;
            Time.timeScale = 1;
        }
    }
    
    public void ResetAllTutorials()
    {
        for(int type = 0; type < hintArray.HintIcons.Length; type++)
        {
            for(int hint = 0; hint < hintArray.HintIcons[type].Length;hint++)
            {
                if (hintArray.HintIcons[type][hint] != null)
                {
                    string id = hintArray.HintIcons[type][hint].name;
                    PlayerPrefs.DeleteKey(id);
                    //Debug.Log(id);
                }
            }
        }
        PlayerPrefs.Save();
    }
}
