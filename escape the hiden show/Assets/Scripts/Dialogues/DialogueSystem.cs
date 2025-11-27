using UnityEngine;
using TMPro;

public class DialogueSystem : MonoBehaviour
{
    public static DialogueSystem instance;

    public GameObject panel;
    public TMP_Text dialogueText;
    public TMP_Text continueHint;

    private string[] lines;
    private int index;

    void Awake()
    {
        instance = this;
    }

    void Update()
    {
        if (panel.activeSelf && Input.GetMouseButtonDown(0))
        {
            NextLine();
        }
    }

    public void StartDialogue(string[] newLines)
    {
        lines = newLines;
        index = 0;

        panel.SetActive(true);
        dialogueText.text = lines[index];
    }

    void NextLine()
    {
        index++;

        if (index >= lines.Length)
        {
            EndDialogue();
            return;
        }

        dialogueText.text = lines[index];
    }

    void EndDialogue()
    {
        panel.SetActive(false);
    }
}
