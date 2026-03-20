using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
/// 
/// Author: Weston Tollette
/// Created: 3/20/26
/// Purpose: Setting these all to the same thing so we dont have to re do it every time we make a new menu
/// 
public class SetMenuActive : MonoBehaviour
{
    [SerializeField][Tooltip("Array of Menus")] 
    GameObject[] menu;

    public void MenuActivate(int option)
    { // will run through all the menus and make sure they are inactive
        if(option<menu.Length && option>=0){ // make sure this only runs if the int is within the range.
            foreach(GameObject obj in menu)
            {
                if (obj.activeSelf)
                { // only make inactive if it is already active.
                    obj.SetActive(false);
                }
            }
            
            menu[option].SetActive(true); // set chosen menu to active.
        }
    }
}
