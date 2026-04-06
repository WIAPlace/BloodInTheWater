using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.AI;

public class FishInstanceMenu : MonoBehaviour
{
    [SerializeField][Tooltip("Fish Holder SO")]
    private FishHolderSO fishHolder;
    [SerializeField][Tooltip("PlayerUnlocks")]
    private Unlocks unlocks;

    [SerializeField][Tooltip("Custom Canvas")]
    private GameObject canvas;
    [SerializeField][Tooltip("Slider Prefab")]
    private GameObject FIS;
    [SerializeField][Tooltip("fish parent")]
    private GameObject spawn;

    [SerializeField][Tooltip("Player")]
    QuickTimeController_Player player;
    [SerializeField][Tooltip("Spinner Settings Sliders")]
    Slider[] spinnerSettings;

    [SerializeField][Tooltip("Array of Monsters")]
    GameObject[] monsters;

    [SerializeField][Tooltip("Toggles Associated with monsters")]
    Toggle[] monsterToggles;

    private float defaultHitArea = 100;
    private float defaultHitSpeed = 400;
    private float defaultHitSmooth = 100;

    private List<SliderData> activeSliders = new List<SliderData>();
    // list of sliders associated with their fish indexes.

    private List<GameObject> activeFish = new List<GameObject>();
    // fish active in scene

    int walkableMask;

    private int startValue=0;
    private int min = 0;
    [SerializeField][Tooltip("Max Number of Fish")] private int max = 5;

    private int fishHoldLen;
    // Start is called before the first frame update
    void Start()
    {
        defaultHitArea = player.GetHitArea() / 10;
        defaultHitSpeed = player.GetHitSpeed() / 90;
        defaultHitSmooth = player.GetHitSmooth();

        fishHoldLen = fishHolder.GetLength();
        walkableMask = 1 << NavMesh.GetAreaFromName("Walkable");
        //Debug.Log(fishHoldLen);
        for(int i = 0; i < fishHoldLen; i++)
        {
            SliderInstance(i);
        }
        SpinnerSettingsChanges();
        TogglesAvalible();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    ///////////////////////////////////////////////////////////// Unlocked Fish Sliders
    private void SliderInstance(int key)
    {
        if (unlocks.LoadFishData(key) == 0)
        { // if the fish has not been caught
            return;
        }
        // 1. Instantiate the prefab as a child of the content parent
        GameObject newSliderGO = Instantiate(FIS, canvas.transform);
        
        // 2. Get the Slider component to modify values
        Slider slider = newSliderGO.GetComponent<Slider>();

        TextMeshProUGUI name = newSliderGO.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        name.text = fishHolder.GetFish(key).name;
        //TextMeshProUGUI number = newSliderGO.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        

        slider.wholeNumbers = true;
        slider.value = startValue;
        slider.minValue = min;
        slider.maxValue = max;

        SliderData newData = new SliderData(slider, key);
        activeSliders.Add(newData);
        
        /*
        slider.onValueChanged.AddListener((value) =>
        {
           //number.text = value.ToString();
        });
        */
    }

    //////////////////////////////////////////////////////////////////////////////////////////////// Spinner Settings
    private void SpinnerSettingsChanges()
    {
        // at start set these to their defaults
        DefaultSpinnerSettings();

        // Adds the Listeners to change the spinner controls to the sliders.
        spinnerSettings[0].onValueChanged.AddListener((value)=>
        player.ChangeHitArea(value*10));
        spinnerSettings[1].onValueChanged.AddListener((value)=>
        player.ChangeHitSpeed(value*90));
        spinnerSettings[2].onValueChanged.AddListener((value)=>
        player.changeHitSmooth(value));
    }
    public void DefaultSpinnerSettings() // used for setting it back to the values we have it in game
    {
        
        spinnerSettings[0].value = defaultHitArea;
        spinnerSettings[1].value = defaultHitSpeed;
        spinnerSettings[2].value = defaultHitSmooth;
    }

    //////////////////////////////////////////////////////////////////////////////////////////////// Monster Settings
    private void TogglesAvalible()
    {
        for(int i =0; i < monsterToggles.Length; i++)
        {
            monsterToggles[i].isOn=false;
            monsterToggles[i].gameObject.SetActive(false);
            // turn off the object on load then only turn it on if its been unlocked.
            if (unlocks.LoadMonsterData(i))
            {// if the monster hasnt been unlocked
                monsterToggles[i].gameObject.SetActive(true);
                int index = i;
                monsterToggles[i].onValueChanged.AddListener(delegate
                {   // add a listener that will activate the monster toggle associated with this ones key.
                    
                    MonsterToggleChanged(monsterToggles[index],monsters[index]);
                });
            }
        }
    }

    public void MonsterToggleChanged(Toggle monTog,GameObject mon)
    {   // set the monster at key i, to whatever the toggle is at.
        //Debug.Log(i);
        mon.gameObject.SetActive(monTog.isOn);
    }



    //////////////////////////////////////////////////////////////////////////////////////////////// Apply Changes
    // change the amount of fish relative to the new sliders values
    public void ApplyChanges()
    {
        GameManager.Instance.ChangeScubaMesActive(false);
        foreach(GameObject i in activeFish)
        {// destroy all the previous fish just so we dont over load the system
            Destroy(i);
        }

        for(int i = 0; i < activeSliders.Count; i++)
        {
            for(int val = 0; val<activeSliders[i].sliderInstance.value; val++)
            {   
                NavMeshHit hit; // spawn the new navmesh on a navmesh surface
                NavMesh.SamplePosition(transform.position, out hit, 50f,walkableMask);


                int id = activeSliders[i].associatedID;
                GameObject newFish = Instantiate(fishHolder.GetFish(id),hit.position,spawn.transform.rotation, spawn.transform);
                activeFish.Add(newFish);
            }
        }
        GameManager.Instance.ChangeScubaMesActive(true);
    }
}
