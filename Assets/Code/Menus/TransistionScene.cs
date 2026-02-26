using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/// 
/// Author: Marshall Turner
/// Created: 2/25/26
/// Purpose: Swaps the scenes but with a fade in
/// 
/// Edited: 
/// Edited By: 
/// Edit Purpose: 
///
public class TransistionScene : MonoBehaviour
{
    public GameObject loadingScreen;
    public string sceneToLoad;
    public CanvasGroup canvasGroup;

    public void StartGame()
    {
        StartCoroutine(StartLoad());
    }

    IEnumerator StartLoad()
    {
        loadingScreen.SetActive(true); //Turns on the fade image
        yield return StartCoroutine(FadeLoadingScreen(1, 1)); //The speed of the fade in

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneToLoad);
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
