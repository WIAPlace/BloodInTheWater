using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/// 
/// Author: Marshall Turner
/// Created: 2/25/26
/// Purpose: A timer before the scene changes be itself
/// 
/// Edited: 2/26/2026
/// Edited By: Marshall Turner
/// Edit Purpose: To reduce required transistion scenes
///
public class TransitionTimer : MonoBehaviour
{
    public GameObject loadingScreen;
    public CanvasGroup canvasGroup;
    public float time;
    IEnumerator SceneWait()
    {
        yield return new WaitForSeconds(time);
    }

    void Start()
    {
        //StartCoroutine(SceneWait());
        StartCoroutine(StartLoad());
    }

    IEnumerator StartLoad()
    {
        yield return new WaitForSeconds(time);
        loadingScreen.SetActive(true); //Turns on the fade image
        yield return StartCoroutine(FadeLoadingScreen(1, 1)); //The speed of the fade in

       SceneManager.LoadSceneAsync(TransitionData.sceneToLoad);
       

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
