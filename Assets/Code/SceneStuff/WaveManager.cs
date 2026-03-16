using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
/// 
/// Author: Weston Tollette
/// Created: 3/15/26
/// Purpose: Wave Manager
/// 
public class WaveManager : MonoBehaviour
{
    [SerializeField]
    Material waterMat;

    [SerializeField][Tooltip("Direction for the water to move twoards")]
    Vector2[] direction;

    [SerializeField][Tooltip("Array for the diffrent Amplitudes of the Sin Waves")]
    float[] amplitude;
    [SerializeField][Tooltip("Array for the diffrent frequencys of the Sin Waves")]
    float[] frequency;
    [SerializeField][Tooltip("Array for the diffrent speed of the Sin Waves")]
    float[] speed;

    public static WaveManager Instance {get; private set;}

    private void Awake()
    {
        // If an instance already exists, and it's not this one, destroy this one
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            // Otherwise, set this one as the instance
            Instance = this;
            // Optional: Use DontDestroyOnLoad(gameObject); to keep the object persistent across scene loads
        }
        SetWaterMaterialVars();
    }

    private void SetWaterMaterialVars()
    {
        // direction
        waterMat.SetVector("_Direction_A",direction[0]);
        waterMat.SetVector("_Direction_B",direction[1]);
        waterMat.SetVector("_Direction_C",direction[2]);
        // amplitude
        waterMat.SetFloat("_Amplitude_A",amplitude[0]);
        waterMat.SetFloat("_Amplitude_B",amplitude[1]);
        waterMat.SetFloat("_Amplitude_C",amplitude[2]);
        // frequency
        waterMat.SetFloat("_Frequency_A",frequency[0]);
        waterMat.SetFloat("_Frequency_B",frequency[1]);
        waterMat.SetFloat("_Frequency_C",frequency[2]);
        // speed
        waterMat.SetFloat("_Speed_A",speed[0]);
        waterMat.SetFloat("_Speed_B",speed[1]);
        waterMat.SetFloat("_Speed_C",speed[2]);
    }

    // Update is called once per frame
    public void WaveUpdate(Transform obj)
    {
        float x = obj.position.x;
        float z = obj.position.z;

        float y = 0; // will be assigned in a second.

        Vector2 worldPos = new Vector2(x,z);
        

        for(int i = 0; i < amplitude.Length; i++)
        {
            float projection = Vector2.Dot(worldPos,direction[i]);
            float phase = (projection * frequency[i]) + (Time.time *speed[i] * 2 * Mathf.PI);
            y += (float)(amplitude[i]*2/Mathf.PI * Mathf.Asin(Mathf.Sin(phase)));
        }

        //float newY = Mathf.SmoothDamp(obj.position.y, y, ref yVelocity, smoothTime);

        obj.position = new Vector3(x,y,z);
    }
}
