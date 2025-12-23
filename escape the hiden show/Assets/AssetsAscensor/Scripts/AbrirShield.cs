using UnityEngine;
using UnityEngine.EventSystems;

public class AbrirShield : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private bool activado = false; 
    [SerializeField] private string objectID;
    [SerializeField] private GameObject puerta;
    private Quaternion rotacionObjetivo;

    void Start()
    {
        rotacionObjetivo = puerta.transform.rotation;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (activado && Inventory.instance.HasItem(objectID))
        {
            Inventory.instance.RemoveItemByID(objectID);
            rotacionObjetivo = Quaternion.Euler(0, 180, 90);
        }
    }

    public void activar()
    {
        activado = true;
    }

    void Update()
    {
        puerta.transform.rotation = Quaternion.Lerp(transform.rotation, rotacionObjetivo, Time.deltaTime * 5f);
    }
}
