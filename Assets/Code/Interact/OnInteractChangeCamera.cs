using System.Collections;
using System.Collections.Generic;
using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class OnInteractChangeCamera : MonoBehaviour, IInteractable
{
    [SerializeField] CinemachineCamera cam;
    [SerializeField] CinemachineSplineDolly dollyCam;

    [SerializeField] private float splineTime;

    public void Interact()
    {
        cam.Priority=20;
        StartCoroutine(MoveAlongSpline());
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator MoveAlongSpline()
    {
        float currentPosition = 0;
        while(currentPosition <= 131)
        {
            currentPosition+=Time.deltaTime*splineTime;
            dollyCam.CameraPosition = currentPosition;
            yield return null;
        }
    }


}
