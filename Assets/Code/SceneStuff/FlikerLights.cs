using System.Collections;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;

// fliker the outerLights when time is going down
public class FlickerLights : MonoBehaviour
{
    [SerializeField] private Light[] outsideLights;
    [SerializeField] private Light[] innerLights;
    [SerializeField,Range(0f,5f)] float minIntensity = 0.5f;
    [SerializeField,Range(0f,5f)] float maxIntensity = 0.5f;
    [SerializeField] float timeBetweenIntensity = .1f;
    [SerializeField, Tooltip("When lights start to fliker the inner lights should go to this intensity")] 
    float innerIntensity = .5f;

    [SerializeField] float burstTime=5f;

    Coroutine running;
    private float currentTimer =0;
    private bool flickering =false;
    // Start is called before the first frame update
    void Awake()
    {
        ValidateIntensityBounds();
        //BeginFlickering();
    }

    void ValidateIntensityBounds()
    {
        if (!(minIntensity > maxIntensity))
        {
            return;
        }
        Debug.Log("Swapping min and max");
        (minIntensity,maxIntensity) = (maxIntensity,minIntensity);
    }

    // callable by another thing, like a the game state controller 
    public void BeginFlickering(float minIntens, float maxIntens, float innerIntens, float flickerTime, float burst)
    {
        minIntensity = minIntens;
        maxIntensity = maxIntens;
        innerIntensity = innerIntens;
        timeBetweenIntensity = flickerTime;
        burstTime = burst;

        BeginFlickering();
    }

    public void BeginFlickering()
    {   // basic constructor if the normal stuff is valid.
        if (running != null)
        {   // stop the flickering corutine if its running
            StopCoroutine(running);
        }

        foreach(Light l in innerLights)
        {   // we want this seprate cause inside the cabin it feels a bit epileptic if left to fliker
            if(l!=null)
            {
                l.intensity = innerIntensity;
            }
        }
        StartCoroutine(Flicker());
        Debug.Log("Flickering");
    }


    void Update()
    {
        if(flickering){
            currentTimer += Time.deltaTime;
            if(!(currentTimer >= timeBetweenIntensity))return;

            // set intenisty of all outerLights to the same fliker so its not fucking off.
            float curIntensity = Random.Range(minIntensity,maxIntensity);
            foreach(Light l in outsideLights)
            {
                l.intensity = curIntensity;
            }
            currentTimer = 0;
        }
    }
    IEnumerator Flicker()
    {
        while (true)
        {    
            flickering = false;
            yield return new WaitForSeconds(burstTime);
            flickering = true;
            yield return new WaitForSeconds(burstTime);
        }   
    }


}
