using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Cinemachine;

public class FOV : MonoBehaviour
{
    // Reference to your Cinemachine Virtual Camera
    [SerializeField]
    private CinemachineCamera virtualCamera;
    [SerializeField][Tooltip("Slider")]
    private Slider slider;
    [SerializeField][Tooltip("settings object")]
    SavedSettings settings;
    void Start()
    {
        slider.value = settings.FOV;
        slider.onValueChanged.AddListener(delegate {ChangeFOV();});
    }
    // Public method to be called by the UI Slider
    public void ChangeFOV()
    {
        settings.FOV = slider.value;
        // Update the FieldOfView property in the camera's Lens settings
        virtualCamera.Lens.FieldOfView = slider.value;
        
    }
}
