using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Cinemachine;
using UnityEngine.Audio;
/// 
/// Author: Weston Tollette
/// Created: 3/24/26
/// Purpose: Settings setter using player prefs.
/// 
/// Edited:
/// Edited By:
/// Edit Purpose:
///
public class PlayerPrefrenceScript : MonoBehaviour
{
    // sliders
    private float defaultSensX = 1;
    private float defaultSensY = 1;
    private float defaultVol = 50;

    // toggles
    private int defaultDither = 1;
    private int defaultCamShake = 1;
    private int defaultHints = 1;

    //temp holders
    private float tempSensX=1;
    private float tempSensY=1;
    private float tempVol=50;

    private int tempDither=1;
    private int tempCamShake=1;
    private int tempHints=1;

    [Header("In Settings Refrences")]
    // sliders
    [SerializeField][Tooltip("x axis")]
    private Slider xSlider;
    [SerializeField][Tooltip("y axis")]
    private Slider ySlider;
    [SerializeField][Tooltip("Volume")]
    private Slider volSlider;

    // toggles
    [SerializeField][Tooltip("Dither Toggle")]
    private Toggle ditherToggle;
    [SerializeField][Tooltip("Camera Shake Toggle")]
    private Toggle camToggle;
    [SerializeField][Tooltip("Hint Toggle")]
    private Toggle hintToggle;

    // refrences
    [SerializeField][Tooltip("Dither Texture")]
    private RenderTexture ditherTexture;
    [SerializeField][Tooltip("Dither Game Object")]
    private GameObject ditherImage;
    [SerializeField][Tooltip("Main Audio Mixer")]
    private AudioMixer myMixer;

    [Header("In Scene Refrences")]
    [SerializeField][Tooltip("Player Look Script")]
    private PlayerLook playerLook;
    [SerializeField] [Tooltip("Player camera")]
    private CinemachineBasicMultiChannelPerlin noise;

    
    void Awake()
    {
        // adding functionality to each UI element
        xSlider.onValueChanged.AddListener(delegate {SliderChange(0);});
        ySlider.onValueChanged.AddListener(delegate {SliderChange(1);});
        volSlider.onValueChanged.AddListener(delegate{SliderChange(2);});

        ditherToggle.onValueChanged.AddListener(delegate{ToggleChange(0);});
        camToggle.onValueChanged.AddListener(delegate{ToggleChange(1);});
        if(hintToggle!=null)hintToggle.onValueChanged.AddListener(delegate{ToggleChange(2);});

        LoadData(); 

        LoadUIStates();

        ApplySettings();
    }
    void Start()
    {
        ApplySettings();
    }

    public void LoadData()
    {
        defaultSensX = PlayerPrefs.GetFloat("SensX",1); // defaut to one
        defaultSensY = PlayerPrefs.GetFloat("SensY",1); // defaut to one
        defaultVol = PlayerPrefs.GetFloat("Vol",50f);   // default to 50

        defaultDither = PlayerPrefs.GetInt("Dither",1); // default to true;
        defaultCamShake = PlayerPrefs.GetInt("CamShake",1); // default to true;
        defaultHints = PlayerPrefs.GetInt("Hints",1); // default to true; 
    }

    public void SaveData()
    {
        PlayerPrefs.SetFloat("SensX",tempSensX); // sensetivity x
        PlayerPrefs.SetFloat("SensY",tempSensY); // sensetivity Y
        PlayerPrefs.SetFloat("Vol",tempVol); // volume;

        PlayerPrefs.SetInt("Dither",tempDither); // default to true;
        PlayerPrefs.SetInt("CamShake",tempCamShake); // default to true;
        PlayerPrefs.SetInt("Hints",tempHints); // default to true; 
        
        PlayerPrefs.Save();
    }

    

    void OnApplicationQuit()
    {
        SaveData();
    }

    private void RemoveListeners()
    {
        xSlider.onValueChanged.RemoveListener(delegate {SliderChange(0);});
        ySlider.onValueChanged.RemoveListener(delegate {SliderChange(1);});
        volSlider.onValueChanged.RemoveListener(delegate{SliderChange(2);});

        ditherToggle.onValueChanged.RemoveListener(delegate{ToggleChange(0);});
        camToggle.onValueChanged.RemoveListener(delegate{ToggleChange(1);});
        if(hintToggle!=null)hintToggle.onValueChanged.RemoveListener(delegate{ToggleChange(2);});
    }

    void OnDestroy()
    {
        RemoveListeners();
    }
    

    public void LoadUIStates()
    {   // plug stuff into tem data while messing with data that hasnt changed
        tempSensX = defaultSensX;
        tempSensY = defaultSensY;
        tempVol = defaultVol;

        tempDither = defaultDither;
        tempCamShake = defaultCamShake;
        tempHints = defaultHints;
        //Debug.Log(defaultSensX);
        xSlider.value = defaultSensX;
        ySlider.value = defaultSensY;
        volSlider.value = defaultVol;

        ditherToggle.isOn = CheckBool(defaultDither);
        camToggle.isOn = CheckBool(defaultCamShake);
        if(hintToggle!=null)hintToggle.isOn = CheckBool(defaultHints);
    }

    ///////////////////////////////////////////////////////////////////////// Sensetivity Change
    public void SliderChange(int place)
    {
        switch (place)
        {
            case 0:   // Sensitivity x
                tempSensX = xSlider.value;
                break;

            case 1:   // Sensitivity y
                tempSensY = ySlider.value;
                break;
            
            case 2:  // volume
                tempVol = volSlider.value;
                break;

            default:
                break;
        }
    }

    ///////////////////////////////////////////////////////////////////////// Toggle Change
    public void ToggleChange(int place)
    {
        switch (place)
        {
            
            case 0:   // dither toggle
                tempDither = CheckToggle(ditherToggle.isOn);
                //Debug.Log(ditherToggle.isOn);
                break;

            case 1:   // cam shake
                tempCamShake = CheckToggle(camToggle.isOn);
                break;
            
            case 2:  // hint
                tempHints = CheckToggle(hintToggle.isOn);
                break;

            default:
                break;
        }
    }
    private int CheckToggle(bool toggle)
    {
        if(toggle)
        {
            return 1;
        }
        else return 0;
    }
    private bool CheckBool(int toggle)
    {
        if(toggle == 1) return true;
        else return false;
    }
    //////////////////////////////////////////////////////////////////////////// Apply Settings:
    
    public void ApplySettings()
    {
        
        SaveData();
        
        LoadData();
        

        // apply settings 
        ChangeSensitivity();
        ChangeVolume();

        ChangeDither();
        ChangeCamShake();
        ChangeHints();

        LoadUIStates();
    }




    ////////////////////////////////////////////////////////////////////////////// Setting methods
    private void ChangeSensitivity()
    {
        if(playerLook!= null)
        {
            playerLook.UpdateSensitivity(true,defaultSensX);
            playerLook.UpdateSensitivity(false,defaultSensY);
        }
    }
    
    private void ChangeVolume()
    {
        if(myMixer!=null)
        {
            // Use log to map 0-1 to dB, avoiding Log10(0)
            myMixer.SetFloat("MasterVolume", Mathf.Log10(defaultVol) * 20); 
        }
    }

    private void ChangeDither()
    {
        if (!CheckBool(defaultDither))
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

    private void ChangeCamShake()
    {
        if(noise != null)
        { 
            noise.AmplitudeGain = defaultCamShake; 
        }
    }

    private void ChangeHints()
    {
        if(GameManager.Instance != null)
        {
            GameManager.Instance.hintsEnabled = CheckBool(defaultHints);
        }
    }

    
}
