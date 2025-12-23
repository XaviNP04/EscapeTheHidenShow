using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class Desatornillar : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private string objectID;
    private Animator animator;
    public bool fuera { get; private set; }

    void Start()
    {
        fuera = false;
        animator = GetComponent<Animator>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (Inventory.instance.HasItem(objectID))
        {
            animator.enabled = true;
            fuera = true;
            StartCoroutine(parar());
        }
    }

    private IEnumerator parar()
    {
        yield return new WaitForSeconds(1f);
        animator.enabled = false;
    }
}
