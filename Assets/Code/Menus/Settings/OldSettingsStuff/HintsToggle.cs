using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HintsToggle : MonoBehaviour
{
    public Toggle toggle;
    public SavedSettings settings;

    void Start()
    {
        if (toggle != null && settings != null)
        {
            // Initialize the UI toggle with the saved state from the ScriptableObject
            // Add a listener to the toggle's OnValueChanged event to update the ScriptableObject
            toggle.onValueChanged.AddListener(OnToggleValueChanged);
            toggle.isOn = settings.HintsToggle;
        }
    }

    private void OnToggleValueChanged(bool newValue)
    {
        // Update the ScriptableObject's value whenever the UI toggle changes
        settings.HintsToggle = newValue;
        if(GameManager.Instance != null)
        {
            GameManager.Instance.hintsEnabled = newValue;
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
