using UnityEngine;

public class InspectionDialogueTrigger : MonoBehaviour
{
    [Header("Texto del diálogo")]
    [TextArea(2, 6)]
    public string[] dialogueLines;

    [Header("Comportamiento")]
    public bool onlyOnce = true;
    public int triggerEveryXInspections = 1; // 1 = siempre, 5 = cada 5

    private int inspectionCount = 0;
    private bool hasTriggered = false;

    public void OnInspected()
    {
        if (onlyOnce && hasTriggered)
            return;

        inspectionCount++;

        if (inspectionCount % triggerEveryXInspections != 0)
            return;

        if (DialogueSystem.instance != null)
        {
            DialogueSystem.instance.StartDialogue(dialogueLines);
            hasTriggered = true;
        }
    }
}
