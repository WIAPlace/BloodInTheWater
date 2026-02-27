using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/// 
/// Author: Marshall Turner
/// Created: 2/25/26
/// Purpose: Swaps the scenes but with a fade in
/// 
/// Edited: 2/26/2026
/// Edited By: Marshall Turner
/// Edit Purpose: To reduce required transistion scenes
///
public class TransistionScene : MonoBehaviour
{
    public GameObject loadingScreen;
    public string targetScene;
    public string transitionType;
    public CanvasGroup canvasGroup;

    public void StartGame()
    {
        TransitionData.sceneToLoad = targetScene; // changes the scene tied to TransitionData
        StartCoroutine(StartLoad());
    }

    IEnumerator StartLoad()
    {
        loadingScreen.SetActive(true); //Turns on the fade image
        yield return StartCoroutine(FadeLoadingScreen(1, 1)); //The speed of the fade in

        AsyncOperation operation = SceneManager.LoadSceneAsync(transitionType);
        while (!operation.isDone)
        {
            yield return null;
        }

    }
    // The fade in itself
    IEnumerator FadeLoadingScreen(float targetValue, float duration)
    {
        float startValue = canvasGroup.alpha;
        float time = 0;

        while (time < duration)
        {
            canvasGroup.alpha = Mathf.Lerp(startValue, targetValue, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        canvasGroup.alpha = targetValue;
    }

    
}
