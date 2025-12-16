using System.Threading.Tasks;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [TextArea(2, 5)]
    public string[] dialogueLines;

    public bool autoStartOnSceneLoad = false;

    async Task Start()
    {
        if (!autoStartOnSceneLoad)
            return;

        TriggerDialogue();
    }

    public void TriggerDialogue()
    {
        DialogueSystem.instance.StartDialogue(dialogueLines);
    }
}
