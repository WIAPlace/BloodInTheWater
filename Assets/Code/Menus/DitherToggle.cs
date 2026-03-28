using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
/// 
/// Author: Marsahll Turner
/// Created: 3/8/26
/// Purpose: To toggle on/off the dither effect on screen
/// 
/// Edited: 
/// Edited By: 
/// Edit Purpose: 
/// 
public class DitherToggle : MonoBehaviour
{
    public Toggle toggle;
    public RenderTexture ditherTexture;
    public GameObject ditherImage;
    public SavedSettings settings;

    void Start()
    {
        if (toggle != null && settings != null)
        {
            // Initialize the UI toggle with the saved state from the ScriptableObject
            

            // Add a listener to the toggle's OnValueChanged event to update the ScriptableObject
            toggle.onValueChanged.AddListener(OnToggleValueChanged);
            toggle.isOn = settings.DitherToggle;
        }
    }

    private void OnToggleValueChanged(bool newValue)
    {
        // Update the ScriptableObject's value whenever the UI toggle changes
        settings.DitherToggle = newValue;
        if (!newValue)
        {
            ditherImage.SetActive(false);
            Camera.main.targetTexture = null;
        }
        else
        {
            ditherImage.SetActive(true);
            Camera.main.targetTexture = ditherTexture;
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
