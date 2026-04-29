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
/// Edited: Weston T
/// Edited By: 3/1/2026
/// Edit Purpose: Creating a overload for start dialouge
///
public class DialogueBoxController : MonoBehaviour
{
    public static DialogueBoxController instance;
    [SerializeField] PlayerLook playerLook;
    [SerializeField] TextMeshProUGUI dialogueText;
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] CanvasGroup dialogueBox;

    [SerializeField] CanvasGroup crosshairImage;
    public Image crosshairImageSmall;
    public static event Action OnDialogueStarted;
    public static event Action OnDialogueEnded;
    bool skipLineTriggered;
    public bool OnStart;
    public DialogueAsset startDialogue;

    private Coroutine typing;
    public AudioSource audioSource;
    //Typing Speed
    float charactersPerSecond = 25;

    private void Start() //If OnStart is true, there would be a starting dialogue on Scene start using the startName and startDialogue variables
    {
        if (OnStart)
        {
            StartCoroutine(LateStart());

            //StartDialogue(startDialogue.dialogue,startDialogue.audioclip, 0, startDialogue.speaker);
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
    IEnumerator LateStart()
    {
        //GameManager.Instance.SetPause(false);
        yield return new WaitForSeconds(1f);
        //GameManager.Instance.input.SetUI();
        StartDialogue(startDialogue.dialogue,startDialogue.audioclip, 0, startDialogue.speaker);
    }

    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// Dialouge
    // The dialogue
    public void StartDialogue(string[] dialogue,AudioClip[] audioclip, int startPosition, string[] speaker,GameObject[] lookLocations)
    {
        GameManager.Instance.HandleDial(false); // turn stuff off
        dialogueBox.gameObject.SetActive(true);
        StopAllCoroutines();
        StartCoroutine(RunDialogue(dialogue,audioclip, startPosition, speaker,lookLocations));
    }

    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// Overloads
    // Overload Function for grandfathering in old scripts
    public void StartDialogue(string[] dialogue,AudioClip[] audioclip, int startPosition, string[] speaker)
    {
        StartDialogue(dialogue,audioclip,startPosition,speaker,null);
    }
    // overload function that should be used going forward
    public void StartDialogue(DialogueAsset dialAsset,int startPosition) // Overload for less stuff without look locations
    { 
        StartDialogue(dialAsset.dialogue, dialAsset.audioclip, startPosition, dialAsset.speaker, null);
    }

    // overload function that should be used going forward if look locations are needed
    public void StartDialogue(DialogueAsset dialAsset,int startPosition,GameObject[] lookLocations) // Overload for less stuff
    { 
        StartDialogue(dialAsset.dialogue, dialAsset.audioclip, startPosition, dialAsset.speaker, lookLocations);
    }

    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// run Dialouge
    //Prints the lines
    IEnumerator RunDialogue(string[] dialogue,AudioClip[] audioclip, int startPosition, string[] speaker,GameObject[] lookLocations)
    {
        skipLineTriggered = false;
        OnDialogueStarted?.Invoke();
        

        for (int i = startPosition; i < dialogue.Length; i++)
        {
            
            nameText.text = speaker[i];
            //Text
            // italics checker
            if(!string.IsNullOrEmpty(dialogue[i]) && dialogue[i][0] == '*')
            {// italics
                // if not italics already set it to italics
                if(dialogueText.fontStyle != FontStyles.Italic) dialogueText.fontStyle = FontStyles.Italic;
                dialogueText.text = dialogue[i].Substring(1); // set text to what it should be minus the '*' at the start
            }
            else
            {// normal text
                if(dialogueText.fontStyle != FontStyles.Bold) dialogueText.fontStyle = FontStyles.Bold;
                dialogueText.text = dialogue[i]; 
            }

            // look
            if(lookLocations != null&& i<lookLocations.Length && lookLocations[i]!=null) // if the array is full
            {
                if(lookLocations[i].transform != playerLook.GetLookLocation())
                {   // dont change the camera position if its changing to the same position, just stay there
                    playerLook.LookAtTarget(lookLocations[i]);  
                } 
            }
            else playerLook.EnableFreeLook(); // if all of that 

            typing = StartCoroutine(TypeTextUncapped(dialogueText.text));

            //Audio
            if(audioSource != null)
            {
                audioSource.clip = audioclip[i];
                audioSource.Play();
            }

            while (skipLineTriggered == false)
            {
                // Wait for the current line to be skipped
                yield return null;
            }
            skipLineTriggered = false;
        }

        OnDialogueEnded?.Invoke();
        dialogueBox.gameObject.SetActive(false); //Hides box once done
        GameManager.Instance.HandleDial(true); // set stuff back on.
        playerLook.EnableFreeLook();
    }

    public void SkipLine()
    {
        if (typing != null)
        {
            StopCoroutine(typing);
            skipLineTriggered = true;
        }
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