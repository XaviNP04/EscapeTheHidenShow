using UnityEngine;
using UnityEngine.EventSystems;

public class ActivarIman : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private Transform mecanismo;
    [SerializeField] private GameObject iman;
    [SerializeField] private float velocidad;
    private ImanContacto contacto;
    private bool bajando = false;
    private Vector3 iPos;
    private bool pulsado = false;
    private Material mat;

    void Start()
    {
        contacto = iman.GetComponent<ImanContacto>(); 
        mat = GetComponent<Renderer>().material;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!pulsado && contacto.obtenido)
        {
            mat.EnableKeyword("_EMISSION");
            contacto.SoltarObjeto();
        } 
        else if (!pulsado)
        {
            mat.EnableKeyword("_EMISSION");
            bajando = true;
            pulsado = true;
        }
    }

    void Update()
    {
        mat.DisableKeyword("_EMISSION");
        iPos = mecanismo.position;

        if (bajando)
        {
            if (contacto.choque || contacto.obtenido)
            {
                bajando = false;
            } 
            else
            {
                iPos.y = iPos.y - velocidad;
                mecanismo.position = iPos;
            }
        } 
        else if (pulsado)
        {
            if (iPos.y + velocidad < 15f)
                iPos.y = iPos.y + velocidad;
            else
                iPos.y = 15f;
            mecanismo.position = iPos;

            if (iPos.y == 15f)
            {
                pulsado = false;
            }
        }
    }
}
