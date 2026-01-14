using UnityEngine;
using System.Collections;

public class ActivarTrampa : MonoBehaviour
{
    [SerializeField] private Transform trampa;
    [SerializeField] private Transform player;
    private Collider collider;
    Quaternion rotacionObjetivo;

    [Header("Diálogo final")]
    [TextArea(2, 5)]
    [SerializeField] private string[] dialogueLines;

    void Start()
    {
        collider = player.GetComponent<Collider>();
        rotacionObjetivo = trampa.localRotation;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (collider == other)
        {
            rotacionObjetivo = Quaternion.Euler(270, 0, 0);
            StartCoroutine(dialogo());
        }
    }

    void Update()
    {
        trampa.localRotation = Quaternion.Lerp(trampa.localRotation, rotacionObjetivo, Time.deltaTime * 10f);
    }

    IEnumerator dialogo()
    {
        yield return new WaitForSeconds(3f);

        if (DialogueSystem.instance != null)
        {
            DialogueSystem.instance.StartDialogue(dialogueLines);
        }
    }
}
