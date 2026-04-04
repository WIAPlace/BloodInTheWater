using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ValueNumberChange : MonoBehaviour
{
    [SerializeField] Slider slider;
    [SerializeField] TextMeshProUGUI number;

    void Awake()
    {
        number.text = slider.value.ToString();
        slider.onValueChanged.AddListener((value)=> 
        number.text = value.ToString());

        
    }
}
