using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackTriggered : MonoBehaviour
{
    [SerializeField]
    private LatchedOnFace LOF;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other)
    {
        LOF.Triggered(other);
        //Debug.Log("Back Off");
    }
}
