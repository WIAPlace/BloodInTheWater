using System.Collections;
using System.Collections.Generic;
using Unity.Cinemachine;
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
    [SerializeField][Tooltip("Array of Cameras")]
    CinemachineCamera[] cameras; // array of cameras

    public void MenuActivate(int option)
    { // will run through all the menus and make sure they are inactive
        if(option<menu.Length && option>=0){ // make sure this only runs if the int is within the range.
            for(int i = 0; i<menu.Length;i++)
            {
                if (menu[i].activeSelf)
                { // only make inactive if it is already active.
                    menu[i].SetActive(false);
                    if (cameras[i] != null)
                    {
                        cameras[i].Priority =0;
                    }
                }
            }
            
            menu[option].SetActive(true); // set chosen menu to active.
            if (cameras[option] != null)
            { // set priority camea
                cameras[option].Priority = 3;
            }
        }
    }
}
