using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using QuickTime;  
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
    [SerializeField][Tooltip("Type")]
    TextMeshProUGUI fishType;
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

    ///////////////////////////////////////////////////////////////////////////////////////// Activate UI
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
        if (infoBox.activeSelf)
        {
            infoBox.SetActive(false);
        }
    }

    ///////////////////////////////////////////////////////////////////////////////////////// Set Lbs
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

    ///////////////////////////////////////////////////////////////////////////////////////// Set Name
    public void SetName(string tempName)
    {   // set the name text to whatever the name of the fish was

        fishName.text = tempName;
    }

    ///////////////////////////////////////////////////////////////////////////////////////// Set Type
    public void SetType(QuickTimeType_Enum type)
    {
        fishType.text = type.ToString();
        switch (type)
        {
            case QuickTimeType_Enum.BasicFish:
                fishType.color = Color.gray; 
                break; // Place Holder

            case QuickTimeType_Enum.Sunlit:
                fishType.color = Color.white;
                break;
            
            case QuickTimeType_Enum.Twilight:
                fishType.color = Color.cyan;
                break;
            
            case QuickTimeType_Enum.Midnight:
                fishType.color = Color.blue;
                break;
            
            case QuickTimeType_Enum.Abbysal: // might not be used 
                fishType.color = Color.magenta;
                break;
            
            case QuickTimeType_Enum.Eldritch:
                fishType.color = Color.green;
                break;

            case QuickTimeType_Enum.SeaAngel:
                fishType.color = Color.black;
                Debug.Log("Shouldn't Happen");
                break;

            default:
                break;
        }
    }




    IEnumerator ActivateUIForABit()
    {
        yield return new WaitForSeconds(delayTime);
        infoBox.SetActive(true); // set active
    }

}
