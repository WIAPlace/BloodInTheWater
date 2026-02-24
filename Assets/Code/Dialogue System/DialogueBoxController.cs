using System;
using System.Collections;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
/// 
/// Author: Marsahll Turner
/// Created: 2/12/26
/// Purpose: Controls the Dialogue Box
/// 
/// Edited: Marshall Turner
/// Edited By: 2/15/2026
/// Edit Purpose: Adding typewriter text and audio
///
public class DialogueBoxController : MonoBehaviour
{
    public static DialogueBoxController instance;

    [SerializeField] TextMeshProUGUI dialogueText;
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] CanvasGroup dialogueBox;

    public Image crosshairImage;
    public Image crosshairImageSmall;
    public static event Action OnDialogueStarted;
    public static event Action OnDialogueEnded;
    bool skipLineTriggered;
    public bool OnStart;
    public string startName;
    public DialogueAsset startDialogue;

    public AudioSource audioSource;
    //Typing Speed
    float charactersPerSecond = 25;

    private void Start() //If OnStart is true, there would be a starting dialogue on Scene start using the startName and startDialogue variables
    {
        if (OnStart)
        {
            StartDialogue(startDialogue.dialogue,startDialogue.audioclip, 0, startName);
        }
        else
        {
            dialogueBox.gameObject.SetActive(false);
        }
        
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    // The dialogue
    public void StartDialogue(string[] dialogue,AudioClip[] audioclip, int startPosition, string name)
    {
        nameText.text = name;
        dialogueBox.gameObject.SetActive(true);
        crosshairImage.gameObject.SetActive(false); //Hides crosshair
        crosshairImageSmall.gameObject.SetActive(false); //Hides crosshair
        StopAllCoroutines();
        StartCoroutine(RunDialogue(dialogue,audioclip, startPosition));
        
    }

    //Prints the lines
    IEnumerator RunDialogue(string[] dialogue,AudioClip[] audioclip, int startPosition)
    {
        skipLineTriggered = false;
        OnDialogueStarted?.Invoke();
        

        for (int i = startPosition; i < dialogue.Length; i++)
        {
            //Text
            dialogueText.text = dialogue[i];
            StartCoroutine(TypeTextUncapped(dialogueText.text));

            //Audio
            audioSource.clip = audioclip[i];
            audioSource.Play();

            while (skipLineTriggered == false)
            {
                // Wait for the current line to be skipped
                yield return null;
            }
            skipLineTriggered = false;
        }

        OnDialogueEnded?.Invoke();
        dialogueBox.gameObject.SetActive(false); //Hides box once done
        crosshairImage.gameObject.SetActive(true); //Brings back the croshair
        crosshairImageSmall.gameObject.SetActive(true); //Brings back the croshair

    }

    public void SkipLine()
    {
        skipLineTriggered = true;
    }
    //Typewriter Text
    IEnumerator TypeTextUncapped(string line)
    {
        float timer = 0;
        float interval = 1 / charactersPerSecond;
        string textBuffer = null;
        char[] chars = line.ToCharArray();
        int i = 0;

        while (i < chars.Length)
        {
            if (timer < Time.deltaTime)
            {
                textBuffer += chars[i];
                dialogueText.text = textBuffer;
                timer += interval;
                i++;
            }
            else
            {
                timer -= Time.deltaTime;
                yield return null;
            }
        }
    }
}