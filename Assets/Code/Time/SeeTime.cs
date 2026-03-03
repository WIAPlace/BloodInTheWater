using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeeTime : MonoBehaviour, IInteractable
{
    [SerializeField] private TimeKeeper keptTime;
    public void Interact()
    {
        string txt ="Clock for keeping time";
        if(GameState.Instance !=null){
            float timeLeft =Mathf.Round(keptTime.GetTimeLeft() * 10)/10;
            txt = timeLeft.ToString() + " : Seconds Left";
        }
        GameManager.Instance.ShowUIText(txt);
    }
}
