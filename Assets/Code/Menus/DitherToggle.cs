using System.Collections;
using System.Collections.Generic;
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
    public bool ditherToggle;
    public RenderTexture ditherTexture;
    public GameObject ditherImage;

    void Start()
    {
        ditherImage.SetActive(false);
        Camera.main.targetTexture = null;
    }

    public void DitherSwitch()
    {
        ditherToggle = !ditherToggle;
        
        if (!ditherToggle)
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
}
