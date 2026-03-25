using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.UI;

public class CameraShakeToggle : MonoBehaviour
{
    //[SerializeField] CinemachineCamera cam;
    [SerializeField] private CinemachineBasicMultiChannelPerlin noise;
    [SerializeField] private SavedSettings settings;
    [SerializeField] private Toggle toggle;

    void Start()
    {
        if (toggle != null && settings != null)
        {
            // Initialize the UI toggle with the saved state from the ScriptableObject
            

            // Add a listener to the toggle's OnValueChanged event to update the ScriptableObject
            toggle.onValueChanged.AddListener(OnToggleValueChanged);
            toggle.isOn = settings.CameraShake;
        }
    }

    private void OnToggleValueChanged(bool newValue)
    {
        // Update the ScriptableObject's value whenever the UI toggle changes
        settings.CameraShake = newValue;
        if(noise != null)
        {
            if (newValue)
            {   
                noise.AmplitudeGain = 1;
            }
            else
            {
                noise.AmplitudeGain = 0;
            }
        }
    }

    // Remember to remove the listener to prevent memory leaks when the object is destroyed
    void OnDestroy()
    {
        if (toggle != null)
        {
            toggle.onValueChanged.RemoveListener(OnToggleValueChanged);
        }
    }
}
