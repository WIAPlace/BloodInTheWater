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

    void Start()
    {
        slider.onValueChanged.AddListener(delegate {ChangeFOV();});
    }
    // Public method to be called by the UI Slider
    public void ChangeFOV()
    {
        // Update the FieldOfView property in the camera's Lens settings
        virtualCamera.Lens.FieldOfView = slider.value;
    }
}
