using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
// data holder for custom slider stuff.
[System.Serializable]
public class SliderData
{
    public Slider sliderInstance;
    public int associatedID;

    // Constructor to make creation easier
    public SliderData(Slider slider, int id)
    {
        sliderInstance = slider;
        associatedID = id; // index for instaniating the fish.
    }
}