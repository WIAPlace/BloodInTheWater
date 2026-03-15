using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
/// 
/// Author: Weston Tollette
/// Created: 3/15/26
/// Purpose: Move Along with the waves.
/// 
public class Waves : MonoBehaviour
{
    [SerializeField]
    Material waterMat;

    [SerializeField][Tooltip("Direction for the water to move twoards")]
    Vector2 direction;

    [SerializeField][Tooltip("Array for the diffrent Amplitudes of the Sin Waves")]
    float[] amplitude;
    [SerializeField][Tooltip("Array for the diffrent frequencys of the Sin Waves")]
    float[] frequency;
    [SerializeField][Tooltip("Array for the diffrent speed of the Sin Waves")]
    float[] speed;

    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float x = transform.position.x;
        float z = transform.position.z;

        float y = 0; // will be assigned in a second.

        //Vector3 totalNormal = Vector3.zero;

        Vector2 worldPos = new Vector2(x,z);
        float projection = Vector2.Dot(worldPos,direction);

        for(int i = 0; i < amplitude.Length; i++)
        {
            float phase = (projection * frequency[i]) + (Time.time *speed[i]);
            y += (float)(amplitude[i]*Math.Sin(phase));

            // get the slope of the wave
            //float slope = amplitude[i] * frequency[i] * Mathf.Cos(phase);

            //apply it to the total normal
            //totalNormal.x -= worldPos.x * slope;
            //totalNormal.z -= worldPos.y * slope;
        }
        //totalNormal.y = 1f; // set y to up
        //totalNormal.Normalize();

        transform.position = new Vector3(x,y,z);
        //transform.rotation = Quaternion.LookRotation(transform.forward, totalNormal);
        //Quaternion targetRot = Quaternion.LookRotation(transform.forward, totalNormal);
        //transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, Time.deltaTime * 5f);
    }
}
