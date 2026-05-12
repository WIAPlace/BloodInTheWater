using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class TerrainIndexChange : MonoBehaviour
{
    [SerializeField] PlayerMovement pm;
    [SerializeField] int landStepIndex;
    [SerializeField] int woodStepIndex;
    [SerializeField] LayerMask playerMask;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(((1 << other.gameObject.layer) & playerMask.value) != 0 && pm!=null)
        {
            pm.ChangeTerrainIndex(woodStepIndex);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(((1 << other.gameObject.layer) & playerMask.value) != 0 && pm!=null)
        {
            pm.ChangeTerrainIndex(landStepIndex);
        }
    }
}
