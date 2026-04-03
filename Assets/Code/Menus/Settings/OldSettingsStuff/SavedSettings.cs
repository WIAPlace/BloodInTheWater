using UnityEngine;


public class SavedSettings : ScriptableObject
{
    public float xSensitivity = 50f;
    public float ySensitivity = 50f;
    public float FOV = 50f;
    public float Volume = .6f;
    public bool DitherToggle;
    public bool CameraShake;
    public bool HintsToggle;
}
