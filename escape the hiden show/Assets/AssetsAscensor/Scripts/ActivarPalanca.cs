using UnityEngine;
using UnityEngine.EventSystems;

public class ActivarPalanca : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private bool[] secuencia;
    [SerializeField] private PulsarBoton[] botones;
    [SerializeField] private GameObject luzOn;
    [SerializeField] private GameObject luzOff;
    [SerializeField] private GameObject panelBraille;
    private Material matOn;
    private Material matOff;
    private Quaternion rotacionObjetivo;

    void Start()
    {
        panelBraille.SetActive(false);
        matOn = luzOn.GetComponent<Renderer>().material;
        matOff = luzOff.GetComponent<Renderer>().material;
        rotacionObjetivo = transform.localRotation;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        bool correcto = true;
        for (int i = 0; i < secuencia.Length; i++)
        {
            if (botones[i].on != secuencia[i])
            {
                correcto = false;
                break;
            }
        }

        if (correcto)
        {
            Debug.Log("Correcto");
            matOn.EnableKeyword("_EMISSION");
            rotacionObjetivo = Quaternion.Euler(-90, 0, 0);
            panelBraille.SetActive(true);
        }
        Debug.Log("Intentando");
        matOff.EnableKeyword("_EMISSION");
    }

    void Update()
    {
        transform.localRotation = Quaternion.Lerp(transform.localRotation, rotacionObjetivo, Time.deltaTime * 5f);
    }
}
