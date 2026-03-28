using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnEnableDisable : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject objectToDisable;
    void OnEnable()
    {
        objectToDisable.SetActive(false);
    }
}
