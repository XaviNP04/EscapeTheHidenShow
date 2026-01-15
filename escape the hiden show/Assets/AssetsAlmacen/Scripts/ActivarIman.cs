using UnityEngine;
using UnityEngine.EventSystems;

public class ActivarIman : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private Transform mecanismo;
    [SerializeField] private GameObject iman;
    [SerializeField] private float velocidad;
    [SerializeField] private MoverIman[] botones;
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

            foreach (MoverIman b in botones)
                b.enabled = false;
        }
    }

    void Update()
    {
        iPos = mecanismo.position;
        float movimiento = velocidad * Time.deltaTime;

        if (bajando)
        {
            if (contacto.choque || contacto.obtenido)
            {
                bajando = false;
            } 
            else
            {
                iPos.y -= movimiento;
                mecanismo.position = iPos;
            }
        } 
        else if (pulsado)
        {
            if (iPos.y < 15f)
            {
                iPos.y = Mathf.MoveTowards(iPos.y, 15f, movimiento);
                mecanismo.position = iPos;
            }

            if (Mathf.Approximately(iPos.y, 15f))
            {
                pulsado = false;
                mat.DisableKeyword("_EMISSION");

                foreach (MoverIman b in botones)
                    b.enabled = true;
            }
        }
    }
}
