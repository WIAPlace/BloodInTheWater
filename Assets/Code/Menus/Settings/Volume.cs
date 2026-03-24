using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine;

public class Volume : MonoBehaviour
{
    [SerializeField] private AudioMixer myMixer;
    [SerializeField] private Slider volumeSlider;
    [SerializeField][Tooltip("Scriptable object for settings")]
    SavedSettings settings;

    private void Start()
    {   // start
        volumeSlider.value = settings.Volume;
        SetVolume();
    }

    public void SetVolume()
    {
        float volume = volumeSlider.value;
        settings.Volume = volume;
        // Use log to map 0-1 to dB, avoiding Log10(0)
        myMixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 20); 
    }
}

