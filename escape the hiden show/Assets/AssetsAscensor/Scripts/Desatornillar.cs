using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class Desatornillar : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private string objectID;
    private Animator animator;
    public bool fuera { get; private set; }

    private AudioSource unscrewSound;
    private bool soundPlayed = false;

    void Start()
    {
        fuera = false;
        animator = GetComponent<Animator>();
        unscrewSound = GetComponent<AudioSource>();
        animator.enabled = false; // Ensure animator starts disabled
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (fuera || soundPlayed || !Inventory.instance.HasItem(objectID))
        {
            return;
        }

        animator.enabled = true;
        fuera = true;

        if (!soundPlayed && unscrewSound != null)
        {
            unscrewSound.Play();
            soundPlayed = true;
        }

        StartCoroutine(parar());
    }

    private IEnumerator parar()
    {
        yield return new WaitForSeconds(1f);
        animator.enabled = false;

        
    }
}