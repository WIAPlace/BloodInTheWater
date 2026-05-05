using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
/// 
/// Author: Weston Tollette
/// Created: 4/18/26
/// Purpose: switch options without that shit with cameras
/// 
/// Edited:
/// Edited By:
/// Edit Purpose:
///

public class SetMenuActiveBase : MonoBehaviour
{
    [SerializeField][Tooltip("Array of Menus")] 
    GameObject[] menu;
    [SerializeField][Tooltip("Game objects Of first button selected when option is selected")]
    GameObject[] selectedOption;

    public void MenuActivate(int option)
    { // will run through all the menus and make sure they are inactive
        if(option<menu.Length && option>=0){ // make sure this only runs if the int is within the range.
            for(int i = 0; i<menu.Length;i++)
            {
                if (menu[i].activeSelf)
                { // only make inactive if it is already active.
                    menu[i].SetActive(false);
                }
            }
            //Debug.Log(option);
            menu[option].SetActive(true); // set chosen menu to active.
            SetFirstButton(selectedOption[option]); // set the selected button to associated key
        }
        else Debug.Log(option +"didn't work");

    }

    public void SetFirstButton(GameObject selectedOpt)
    {
        EventSystem.current.SetSelectedGameObject(selectedOpt);
    }
}
