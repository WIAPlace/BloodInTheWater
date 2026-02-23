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

    void Start()
    {
        slider.onValueChanged.AddListener(delegate {SensitivityChange();});
        // on change, change the sensitivity
    }
    public void SensitivityChange()
    {
        //Debug.Log("Changed to: "+slider.value);
        playerLook.UpdateSensitivity(isX, slider.value);
    }
}
