using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// 
/// Author: Marsahll Turner
/// Created: 3/5/26
/// Purpose: 
/// 
/// Edited:
/// Edited By:
/// Edit Purpose:
///
public class PanelSwap : MonoBehaviour
{
    [SerializeField] CanvasGroup panelSelf;
    [SerializeField] CanvasGroup otherPanelOne;
    [SerializeField] CanvasGroup otherPanelTwo;
    [SerializeField] CanvasGroup otherPanelThree;

    [SerializeField] bool startingPanel = false;

    void Start()
    {
        if (!startingPanel)
        {
            panelSelf.alpha = 0;
        }
    }

    // Update is called once per frame
    public void ChangePanel()
    {
        panelSelf.alpha = 1;
        otherPanelOne.alpha = 0;
        otherPanelTwo.alpha = 0;
        otherPanelThree.alpha = 0;
    }
}
