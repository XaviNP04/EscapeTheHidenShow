using UnityEngine;
using UnityEngine.EventSystems;

public class AbrirShield : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private bool activado = false; 
    [SerializeField] private string objectID;
    [SerializeField] private GameObject puerta;
    private Quaternion rotacionObjetivo;

    [SerializeField] private AudioSource beepSound;
    [SerializeField] private AudioSource abrirSound;

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
            abrirSound.Play();
        }
    }

    public void activar()
    {
        activado = true;
        beepSound.Play();

    }

    void Update()
    {
        puerta.transform.rotation = Quaternion.Lerp(transform.rotation, rotacionObjetivo, Time.deltaTime * 5f);
    }
}
