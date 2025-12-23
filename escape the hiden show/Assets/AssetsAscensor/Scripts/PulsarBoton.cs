using UnityEngine;
using UnityEngine.EventSystems;

public class PulsarBoton : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private float posOFF;
    [SerializeField] private float posON;
    private Vector3 pos;
    public bool on { get; private set; }

    void Start()
    {
        pos = transform.localPosition;
        on = false;
        pos.x = posOFF;
        transform.localPosition = pos;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (on)
        {
            pos.x = posOFF;
            on = false;
        } else
        {
            pos.x = posON;
            on = true;
        }

        transform.localPosition = pos;
    }
}
