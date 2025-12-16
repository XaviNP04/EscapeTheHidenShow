using UnityEngine;
using TMPro;
using System.Collections;

public class DialogueSystem : MonoBehaviour
{
    public static DialogueSystem instance;

    [Header("UI")]
    public GameObject panel;
    public TMP_Text dialogueText;
    public TMP_Text continueHint;

    [Header("Typewriter Speed")]
    public float typeSpeed = 0.03f;

    private string[] lines;
    private int index;

    private bool isTyping = false;
    private bool skipTyping = false;

    public static bool dialogueActive = false;    

    void Awake()
    {
        instance = this;

    }

    void Update()
    {


        if (!panel.activeSelf) return;

        if (Input.GetMouseButtonDown(0))
        {
            if (isTyping)
            {
                skipTyping = true;
            }
            else
            {
                NextLine();
            }
        }


    }


    public void StartDialogue(string[] newLines)
    {

        lines = newLines;
        index = 0;

        panel.SetActive(true);
        dialogueActive = true;

        StartCoroutine(TypeLine());
        
    }


    IEnumerator TypeLine()
    {
        isTyping = true;
        skipTyping = false;

        dialogueText.text = "";

        foreach (char c in lines[index])
        {
            if (skipTyping)
            {
                dialogueText.text = lines[index];
                break;
            }

            dialogueText.text += c;
            yield return new WaitForSeconds(typeSpeed);
        }

        isTyping = false;
    }


    void NextLine()
    {
        index++;

        if (index >= lines.Length)
        {
            EndDialogue();
            return;
        }

        StartCoroutine(TypeLine());
    }


    void EndDialogue()
    {
        panel.SetActive(false);
        dialogueActive = false;
    }
}
