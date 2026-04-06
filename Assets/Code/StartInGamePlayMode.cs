using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartInGamePlayMode : MonoBehaviour
{
    [SerializeField]private InputReader input;

    void Start()
    {
        StartCoroutine(SlowStart());
    }
    IEnumerator SlowStart()
    {
        yield return new WaitForSeconds(.1f);
        input.SetGameplay();
    }
}
