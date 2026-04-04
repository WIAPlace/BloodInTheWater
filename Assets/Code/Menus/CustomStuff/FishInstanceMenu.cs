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

    private List<SliderData> activeSliders = new List<SliderData>();
    // list of sliders associated with their fish indexes.

    private List<GameObject> activeFish = new List<GameObject>();
    // fish active in scene

    int walkableMask;

    private int startValue=0;
    private int min = 0;
    private int max = 5;

    private int fishHoldLen;
    // Start is called before the first frame update
    void Start()
    {
        fishHoldLen = fishHolder.GetLength();
        //Debug.Log(fishHoldLen);
        for(int i = 0; i < fishHoldLen; i++)
        {
            SliderInstance(i);
        }
        walkableMask = 1 << NavMesh.GetAreaFromName("Walkable");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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

        slider.wholeNumbers = true;
        slider.value = startValue;
        slider.minValue = min;
        slider.maxValue = max;

        SliderData newData = new SliderData(slider, key);
        activeSliders.Add(newData);
        /*
        slider.onValueChanged.AddListener((value) =>
        {
           Debug.Log(value);
        });
        */
    }


    // change the amount of fish relative to the new sliders values
    public void ApplyChanges()
    {
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
    }
}
