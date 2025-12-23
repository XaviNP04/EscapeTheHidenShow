using UnityEngine;
using UnityEngine.EventSystems;

public class ConectarCable : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private GameObject cable;
    [SerializeField] private TipoColor colorID;
    private Collider collider;

    [SerializeField] private CablesManager manager;

    void Start()
    {
        collider = GetComponent<Collider>();
        cable.SetActive(false);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        cable.SetActive(true);
        collider.enabled = false;
        manager.comprobar(colorID);

    }

    public void reiniciar()
    {
        cable.SetActive(false);
        collider.enabled = true;
    }
}
