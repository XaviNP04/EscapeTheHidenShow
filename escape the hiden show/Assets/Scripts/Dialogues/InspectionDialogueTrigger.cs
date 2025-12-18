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

    Camera _camera;

    void Start()
    {
        _camera = Camera.main;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Vector3 point = new Vector3(_camera.pixelWidth / 2, _camera.pixelHeight / 2, 0);
            Ray ray = _camera.ScreenPointToRay(point);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 5f))
            {
                var hitObject = hit.transform.gameObject;
                if (hitObject == this.gameObject)
                {
                    OnInspected();
                }
            }
        }
    }

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
