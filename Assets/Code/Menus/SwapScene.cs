using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapScene : MonoBehaviour
{
    [SerializeField] private string sceneName;
    [SerializeField] private ChangeScene sceneChange;

    private void OnCollisionEnter(Collision collision)
    {
        sceneChange.LoadSceneByName(sceneName);
        Debug.Log("Hit");
    }
    public void ChangeScene()
    {
        sceneChange.LoadSceneByName(sceneName);
    }
    
}
