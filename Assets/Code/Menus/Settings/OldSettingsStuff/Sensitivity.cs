using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sensitivity : MonoBehaviour
{
    [SerializeField][Tooltip("Player Look Script")]
    private PlayerLook playerLook;
    [SerializeField][Tooltip("Slider")]
    private Slider slider;

    [SerializeField][Tooltip("True if this is for the x axis")]
    private bool isX;
    [SerializeField][Tooltip("Scriptable object for settings")]
    SavedSettings settings;

    void Start()
    {
        if (isX)
        {
            slider.value = settings.xSensitivity;
        }
        else
        {
            slider.value = settings.ySensitivity;
        }
        if(playerLook != null)
        {
           playerLook.UpdateSensitivity(isX, slider.value); 
        }
        
        slider.onValueChanged.AddListener(delegate {SensitivityChange();});
        // on change, change the sensitivity
    }
    public void SensitivityChange()
    {
        //Debug.Log("Changed to: "+slider.value);
        if(playerLook != null)
        {
           playerLook.UpdateSensitivity(isX, slider.value); 
        }
        if (isX)
        {
            settings.xSensitivity = slider.value;
        }
        else
        {
            settings.ySensitivity = slider.value;
        }
    }
}
