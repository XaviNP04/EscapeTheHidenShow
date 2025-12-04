using UnityEngine;

public class ImanContacto : MonoBehaviour
{
    public bool obtenido { get; private set; }
    public bool choque { get; private set; }
    [SerializeField] private GameObject requiredObject;
    [SerializeField] private Transform puntoIman;
    [SerializeField] private GameObject puntoImantado;
    private Collider collider;
    private Rigidbody rb;
    private bool soltado;

    void Start()
    {
        obtenido = false;
        choque = false;
        soltado = false;
        collider = requiredObject.GetComponent<Collider>();
        rb = puntoImantado.GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other == collider && !soltado)
        {
            obtenido = true;
            rb.isKinematic = true;
            puntoImantado.transform.SetParent(puntoIman);
            puntoImantado.transform.localPosition = Vector3.zero;
            soltado = false;

        } else
        {
            choque = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other == collider)
        {
            obtenido = false;
        }
        else
        {
            choque = false;
        }
    }

    public void SoltarObjeto()
    {
        puntoImantado.transform.SetParent(null, true);
        rb.isKinematic = false;
        soltado = true;
        obtenido = false;
    }
}
