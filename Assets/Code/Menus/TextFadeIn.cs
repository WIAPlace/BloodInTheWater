using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/// 
/// Author: Marshall Turner
/// Created: 2/25/26
/// Purpose: To add a fade in effect when opening a new scene
/// 
/// Edited: 
/// Edited By: 
/// Edit Purpose: 
///
public class TextFadeIn : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    public float timeStart;


    void Start()
    {
        StartCoroutine(StartFade());
    }

    IEnumerator StartFade()
    {
        yield return new WaitForSeconds(timeStart);
        yield return StartCoroutine(FadeLoadingScreen(1, 2)); //The speed of the fade in



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
        if(GameManager.Instance!=null){
            GameManager.Instance.input.SetGameplay();
        }
    }
}
