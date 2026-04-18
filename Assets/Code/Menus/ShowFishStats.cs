using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
// weston tollette
// stat menu for fish that pops up when fish is caught.
public class ShowFishStats : MonoBehaviour
{
    [SerializeField][Tooltip("Fish Info Box")]
    private GameObject infoBox;
    [SerializeField][Tooltip("Fish Name Text")]
    TextMeshProUGUI fishName;
    [SerializeField][Tooltip("LBS")]
    TextMeshProUGUI fishWeight;
    [SerializeField][Tooltip("New Best game object")]
    GameObject newBest;
    [SerializeField][Tooltip("How long it should stay on screen for")]
    float stayTime;
    [SerializeField][Tooltip("How long it should wait befor appearing. likley cross refrence with scale script")]
    float delayTime;
    private Coroutine running;

    //private string tempFishName;
    //private string tempFishLbs;
    // Start is called before the first frame update
    void Start()
    {
        infoBox.SetActive(false);
    }

    public void ActivateUI() // activate ui element
    {
        running = StartCoroutine(ActivateUIForABit());
    }
    public void ForceDeactivateUI()
    {
        if (running != null)
        {
            StopCoroutine(running);
        }
    }

    public void SetLbs(float lbs, int key)
    {   // set lbs to whatever lbs that fish was
        if (GameManager.Instance.unlocks.LoadFishData(key) < lbs)
        {   // if this is the larges you've caught/
            if(!newBest.activeSelf) newBest.SetActive(true); // if not active make active
        }
        else {
            if(newBest.activeSelf) newBest.SetActive(false); // if active make in aactive
            //Debug.Log(GameManager.Instance.unlocks.LoadFishData(key));
        }
        fishWeight.text = "Weight: " + ((lbs*10f)/10f).ToString("F1") + " Lbs";
    }
    public void SetName(string tempName)
    {   // set the name text to whatever the name of the fish was

        fishName.text = tempName;
    }
    IEnumerator ActivateUIForABit()
    {
        yield return new WaitForSeconds(delayTime);
        infoBox.SetActive(true); // set active
        yield return new WaitForSeconds(stayTime);
        infoBox.SetActive(false); // set inactive
    }

}
