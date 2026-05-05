using System.Collections;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;

public class OnInteractChangeCamera : MonoBehaviour, IInteractable
{
    [SerializeField] CinemachineCamera cam;
    [SerializeField] CinemachineSplineDolly dollyCam;
    [SerializeField] CinemachineCamera finalCam;

    [SerializeField] private float splineTime;

    [SerializeField] GameObject OldOcean;
    [SerializeField] GameObject newOcean;

    [SerializeField] TransistionScene transistion;

    public void Interact()
    {
        cam.Priority=20;
        StartCoroutine(MoveAlongSpline());
        OldOcean.SetActive(false);
        newOcean.SetActive(true);
        GameManager.Instance.HandleDial(false);
        GameManager.Instance.unlocks.SaveLevelData(0,8);
    }

    // Start is called before the first frame update
    void Start()
    {   
        cam.Priority =0;
        finalCam.Priority = 0;
        newOcean.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator MoveAlongSpline()
    {
        float currentPosition = 0;
        while(currentPosition <= 130)
        {
            currentPosition+=Time.deltaTime*splineTime;
            dollyCam.CameraPosition = currentPosition;
            yield return null;
        }
        cam.Priority =0;
        finalCam.Priority = 20;

        yield return new WaitForSeconds(8f);
        transistion.StartGame(); 
    }
}
