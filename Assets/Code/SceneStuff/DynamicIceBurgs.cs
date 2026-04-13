using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Splines;

public class DynamicIceBurgs : MonoBehaviour
{
    [SerializeField][Tooltip("Array of all the Iceburg Prefabs")]
    private GameObject[] burgs;
    [SerializeField][Tooltip("Spline animate component")]
    SplineAnimate splan;
    private float lastNormTime = 0; // keeps track of if loop has re set

    [SerializeField][Tooltip("min range for how long it should take for the burg to move")]
    private float minDuration;
    [SerializeField][Tooltip("max range for how long it should take for the burg to move")]
    private float maxDuration;

    // Start is called before the first frame update
    void Start()
    {
        splan.NormalizedTime = 0;

        // Set a random indexed object active
        int randyIndex = Random.Range(0,burgs.Length);
        SetBurgActive(randyIndex);

        // Set duration to a random time between min and max durration
        float randyTime = Random.Range(minDuration,maxDuration);
        splan.Duration = randyTime;
    }

    // Update is called once per frame
    void Update()
    {
        float currentNormTime = splan.NormalizedTime;
        if (currentNormTime < lastNormTime)
        {
            //Debug.Log("end hit");
            // Set a random indexed object active
            int randyIndex = Random.Range(0,burgs.Length);
            SetBurgActive(randyIndex);

            // Set duration to a random time between min and max durration
            float randyTime = Random.Range(minDuration,maxDuration);
            splan.Duration = randyTime;

            // reset the spline animator so it starts back at the start.
            splan.NormalizedTime = 0;
            currentNormTime = splan.NormalizedTime;
        }
        lastNormTime = currentNormTime;
    }

    private void SetBurgActive(int index)
    {
        foreach(GameObject burg in burgs)
        {   // turn off all of the burgs
            if(burg.activeSelf){ // check for if it is active 
                burg.SetActive(false);
            }
        }
        if (index < burgs.Length)
        {   // turn on the burg that should be on.
            burgs[index].SetActive(true);
        }
    }




}
