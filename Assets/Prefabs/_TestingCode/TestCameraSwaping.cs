using System.Collections;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;

public class TestCameraSwaping : MonoBehaviour
{
    [SerializeField] CinemachineCamera[] camArray;
    [SerializeField] int[] inbetweens;



    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SwitchShots());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SwitchShots()
    {
        yield return new WaitForSeconds[30];
        camArray[0].Priority = 0;
        camArray[1].Priority = 2;
        Debug.Log("Hit");
        yield return new WaitForSeconds[inbetweens[1]];
        camArray[1].Priority = 0;
        camArray[2].Priority = 2;
        Debug.Log("Hit");
        yield return new WaitForSeconds[inbetweens[2]];
        camArray[2].Priority = 0;
        camArray[3].Priority = 2;
        Debug.Log("Hit");
        yield return new WaitForSeconds[inbetweens[3]];
        camArray[3].Priority = 0;
        camArray[4].Priority = 2;
        Debug.Log("Hit");
        yield return new WaitForSeconds[inbetweens[4]];
        camArray[4].Priority = 0;
        camArray[5].Priority = 2;
        Debug.Log("Hit");
    }
}
