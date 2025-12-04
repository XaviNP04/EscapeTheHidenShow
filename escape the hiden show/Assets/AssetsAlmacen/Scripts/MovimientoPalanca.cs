using UnityEngine;
using UnityEngine.EventSystems;

public class MovimientoPalanca : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    [SerializeField] private Transform targetObject;
    [SerializeField] private float maxRotation = 15f;
    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private float gradosPixel = 1f;
    private float xAnterior;
    private float yAnterior;
    private float giroX = 0f;
    private float giroZ = 0f;

    private bool Dragged = false;
    private Quaternion rotacionInicial;

    void Start()
    {
        // Guardar la rotación inicial de la palanca para poder resetearla
        rotacionInicial = transform.rotation;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        xAnterior = eventData.position.x;
        yAnterior = eventData.position.y;
    }

    public void OnDrag(PointerEventData eventData)
    {
        giroX = giroX - (eventData.position.x - xAnterior) * gradosPixel;
        giroZ = giroZ - (eventData.position.y - yAnterior) * gradosPixel;

        giroX = Mathf.Clamp(giroX, -maxRotation, maxRotation);
        giroZ = Mathf.Clamp(giroZ, -maxRotation, maxRotation);

        transform.localRotation = Quaternion.Euler(giroX,
                                                   rotacionInicial.y,
                                                   giroZ);


        if (targetObject != null)
        {
            // Crear el vector de movimiento (X es lateral, Z es profundidad/avance)
            Vector3 movement = new Vector3(-giroZ, 0f, -giroX).normalized;

            // Aplicar el movimiento
            targetObject.Translate(movement * movementSpeed * Time.deltaTime, Space.World);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        transform.localRotation = rotacionInicial;
    }
}
