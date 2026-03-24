using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class HintArray : MonoBehaviour
{
    [SerializeField] GameObject[] Hints_FishingRod;
    [SerializeField] GameObject[] Hints_Fish;
    [SerializeField] GameObject[] Hints_Spinner;
    [SerializeField] GameObject[] Hints_Interact;
    [SerializeField] GameObject[] Hints_Objects;

    private GameObject[][] HintIcons = new GameObject[5][];
    private void Start()
    {
        HintIcons[0] = Hints_FishingRod;
        HintIcons[1] = Hints_Fish;
        HintIcons[2] = Hints_Spinner;
        HintIcons[3] = Hints_Interact;
        HintIcons[4] = Hints_Objects;
    }

    public void ShowHint(int type,int hint)
    {
        CloseHints();
        if(HintIcons != null && HintIcons[type]!=null && HintIcons[type][hint]!=null)
        {
            HintIcons[type][hint].SetActive(true);
        }
        else if(GameManager.Instance != null)
        {
            //GameManager.Instance.CloseHint();
        }
    }

    // close out all the hints so there is no overlap or they dont remain.
    public void CloseHints()
    {
        foreach(GameObject[] hintType in HintIcons)
        {
            if(hintType!=null){
                foreach(GameObject hint in hintType)
                {
                    if (hint != null && hint.activeSelf)
                    {
                    hint.SetActive(false); 
                    }
                }
            }
        }
    }
}
