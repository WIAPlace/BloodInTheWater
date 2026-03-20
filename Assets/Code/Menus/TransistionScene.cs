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
/// Edited: 3/20/2026
/// Edited By: Marshall Turner
/// Edit Purpose: A bool to stop the script from changing StaticVariables.sceneToLoad
///
public class TransistionScene : MonoBehaviour
{
    public GameObject loadingScreen;
    public string targetScene;
    public string transitionType;
    public CanvasGroup canvasGroup;
    public bool resetThoughts;
    public string lastFishingLevel;
    public bool levelRetry; //If StaticVariables.sceneToLoad uses StaticVariables.lastLevel(true) or targetScene(false) 


    public void StartGame()
    {
        if (levelRetry)
        {
            StaticVariables.sceneToLoad = StaticVariables.lastLevel;
        }
        else
        {
            StaticVariables.sceneToLoad = targetScene; // changes the scene tied to TransitionData
            StaticVariables.lastLevel = lastFishingLevel; // changes lastLevel
        }
        StartCoroutine(StartLoad());
    }

    IEnumerator StartLoad()
    {
        if (resetThoughts)
        {
            StaticVariables.thoughtNum = 0;
        }

        loadingScreen.SetActive(true); //Turns on the fade image
        yield return StartCoroutine(FadeLoadingScreen(1, 1)); //The speed of the fade in

        if (levelRetry)
        {
            AsyncOperation operation = SceneManager.LoadSceneAsync(StaticVariables.lastLevel);
            while (!operation.isDone)
            {
                yield return null;
            }
        }
        else
        {
            AsyncOperation operation = SceneManager.LoadSceneAsync(transitionType);
            while (!operation.isDone)
            {
                yield return null;
            }
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
