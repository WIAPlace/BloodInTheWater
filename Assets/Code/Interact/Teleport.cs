using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour, IInteractable
{
    public Transform teleportTarget; //Teleport location
    public GameObject thePlayer;
    public GameObject loadingScreen;
    public CanvasGroup canvasGroup;
    public float time = 0.2f;

    public void Interact()
    {
        StartCoroutine(StartFade());
    }

    IEnumerator StartFade()
    {
        loadingScreen.SetActive(true); //Turns on the fade image
        yield return StartCoroutine(FadeScreen(1, 0.5f)); //The speed of the fade in
        yield return new WaitForSeconds(time);
        thePlayer.transform.position = teleportTarget.transform.position;
        yield return new WaitForSeconds(time);
        yield return StartCoroutine(FadeScreen(0, 0.5f)); //The speed of the fade in
        loadingScreen.SetActive(false); //Turns off the fade image

    }

    IEnumerator FadeScreen(float targetValue, float duration)
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
