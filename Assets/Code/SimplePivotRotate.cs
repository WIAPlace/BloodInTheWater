using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimplePivotRotate : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform customPivot;

    void Update()
    {
        transform.RotateAround(customPivot.position, Vector3.up, 20 * Time.deltaTime);
    }
}
