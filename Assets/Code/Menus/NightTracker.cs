using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NightTracker : MonoBehaviour
{
    public TextMeshProUGUI nightText;

    void Start()
    {
        
    }

    void Update()
    {
        nightText.text = StaticVariables.nightNum;
    }
}
