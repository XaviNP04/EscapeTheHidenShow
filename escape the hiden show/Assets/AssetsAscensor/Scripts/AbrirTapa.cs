using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class AbrirTapa : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private string objectID;
    [SerializeField] private GameObject[] tornillos;
    private Desatornillar[] estados;
    private Animator animator;
    void Start()
    {
        estados = new Desatornillar[tornillos.Length];
        for (int i = 0; i < tornillos.Length; i++)
        {
            estados[i] = tornillos[i].GetComponent<Desatornillar>();
        }
        animator = GetComponent<Animator>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        bool desatornillado = true;
        foreach (Desatornillar tor in estados)
        {
            if (!tor.fuera)
            {
                desatornillado = false;
                break;
            }
        }

        if (Inventory.instance.HasItem(objectID) && desatornillado)
        {
            Inventory.instance.RemoveItemByID(objectID);

            for (int i = 1; i < tornillos.Length; i++) {
                tornillos[i].GetComponent<Rigidbody>().isKinematic = false;
            }

            animator.enabled = true;
            StartCoroutine(soltar());
        }
    }

    private IEnumerator soltar()
    {
        yield return new WaitForSeconds(1f);
        animator.enabled = false;
        tornillos[0].GetComponent<Rigidbody>().isKinematic = false;
        GetComponent<Rigidbody>().isKinematic = false;
    }
}
