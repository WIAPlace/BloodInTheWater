using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoIntoUIMode : MonoBehaviour
{
    [SerializeField]
    private InputReader input;

    void Start()
    {
        StartCoroutine(LateStart());
    }
    IEnumerator LateStart()
    {
        yield return new WaitForSeconds(.1f);
        input.SetUI();
        Time.timeScale=1f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
