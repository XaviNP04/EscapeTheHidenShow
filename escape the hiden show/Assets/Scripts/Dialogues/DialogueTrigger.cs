using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [TextArea(2, 5)]
    public string[] dialogueLines;

    public bool autoStartOnSceneLoad = false;

    void Start()
    {
        if (autoStartOnSceneLoad)
            TriggerDialogue();
    }

    public void TriggerDialogue()
    {
        DialogueSystem.instance.StartDialogue(dialogueLines);
    }
}
