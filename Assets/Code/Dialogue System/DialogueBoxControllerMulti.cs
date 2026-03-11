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
/// Edited: Marshall T
/// Edited By: 3/10/2026
/// Edit Purpose: Add multi-choice dialogue
///
public class DialogueBoxControllerMulti : MonoBehaviour
{
    public static DialogueBoxControllerMulti instance;

    [SerializeField] TextMeshProUGUI dialogueText;
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] CanvasGroup dialogueBox;
    [SerializeField] GameObject answerBox;
    [SerializeField] Button[] answerObjects;

    public Image crosshairImage;
    public Image crosshairImageSmall;

    public static event Action OnDialogueStarted;
    public static event Action OnDialogueEnded;

    bool skipLineTriggered;
    bool answerTriggered;
    int answerIndex;

    public bool OnStart;
    public string startName;
    public DialogueTree startDialogue;

    private Coroutine typing;
    public AudioSource audioSource;
    //Typing Speed
    float charactersPerSecond = 25;

    private void Start() //If OnStart is true, there would be a starting dialogue on Scene start using the startName and startDialogue variables
    {
        if (OnStart)
        {
            StartCoroutine(LateStart());
            StartDialogue(startDialogue, 0, startName);
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
        yield return new WaitForSeconds(.4f);
        StartDialogue(startDialogue, 0, startName);
    }

    // The dialogue
    public void StartDialogue(DialogueTree dialogueTree, int startPosition, string name)
    {
        ResetBox();
        GameManager.Instance.HandleDial(false); // turn stuff off
        nameText.text = name;
        dialogueBox.gameObject.SetActive(true);
        StopAllCoroutines();
        StartCoroutine(RunDialogue(dialogueTree, startPosition));
    }

    public void StartDialogue(DialogueTree dialogueTree, int startPosition) // Overload for less stuff
    { // this will be used for system stuff/
        //Debug.Log("Hit");
        ResetBox();
        GameManager.Instance.HandleDial(false); // turn stuff off
        nameText.text = ""; // no name
        dialogueBox.gameObject.SetActive(true);
        StopAllCoroutines();
        StartCoroutine(RunDialogue(dialogueTree, startPosition));
    }

    //Prints the lines
    IEnumerator RunDialogue(DialogueTree dialogueTree, int section)
    {
        skipLineTriggered = false;
        OnDialogueStarted?.Invoke();

        for (int i = 0; i < dialogueTree.sections[section].dialogue.Length; i++)
        {
            dialogueText.text = dialogueTree.sections[section].dialogue[i];
            typing = StartCoroutine(TypeTextUncapped(dialogueText.text));

            //if (audioSource != null)
            //{
                //audioSource.clip = audioclip[i];
                //audioSource.Play();
            //}

            while (skipLineTriggered == false)
            {
                yield return null;
            }
            skipLineTriggered = false;
        }

        if (dialogueTree.sections[section].endAfterDialogue)
        {
            OnDialogueEnded?.Invoke();
            dialogueBox.gameObject.SetActive(false); //Hides box once done
            GameManager.Instance.HandleDial(true); // set stuff back on.
            yield break;
        }

        dialogueText.text = dialogueTree.sections[section].branchPoint.question;
        ShowAnswers(dialogueTree.sections[section].branchPoint);

        while (answerTriggered == false)
        {
            yield return null;
        }
        answerBox.SetActive(false);
        answerTriggered = false;

        StartCoroutine(RunDialogue(dialogueTree, dialogueTree.sections[section].branchPoint.answers[answerIndex].nextElement));
    }

    public void SkipLine()
    {
        StopCoroutine(typing);
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
    void ResetBox()
    {
        StopAllCoroutines();
        dialogueBox.gameObject.SetActive(false);
        answerBox.SetActive(false);
        skipLineTriggered = false;
        answerTriggered = false;
    }
    void ShowAnswers(BranchPoint branchPoint)
    {
        // Reveals the aselectable answers and sets their text values
        answerBox.SetActive(true);
        for (int i = 0; i < 3; i++)
        {
            if (i < branchPoint.answers.Length)
            {
                answerObjects[i].GetComponentInChildren<TextMeshProUGUI>().text = branchPoint.answers[i].answerLabel;
                answerObjects[i].gameObject.SetActive(true);
            }
            else
            {
                answerObjects[i].gameObject.SetActive(false);
            }
        }
    }
    public void AnswerQuestion(int answer)
    {
        answerIndex = answer;
        answerTriggered = true;
    }
}