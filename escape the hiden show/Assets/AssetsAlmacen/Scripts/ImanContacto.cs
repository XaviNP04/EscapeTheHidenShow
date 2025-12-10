using UnityEngine;
using System.Collections;

public class ImanContacto : MonoBehaviour
{
    public bool obtenido;
    public bool choque;
    [SerializeField] private GameObject requiredObject;
    [SerializeField] private Transform puntoIman;
    [SerializeField] private GameObject puntoImantado;
    private Collider reqCollider;
    private Collider collider;
    private Rigidbody rb;

    void Start()
    {
        obtenido = false;
        choque = false;
        reqCollider = requiredObject.GetComponent<Collider>();
        collider = GetComponent<Collider>();
        rb = puntoImantado.GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other == reqCollider && !obtenido)
        {
            obtenido = true;
            rb.isKinematic = true;
            puntoImantado.transform.SetParent(puntoIman);
            puntoImantado.transform.localPosition = Vector3.zero;
            collider.enabled = false;
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
            Debug.Log("SALE CABEZA");
        }
        else
        {
            choque = false;
        }
    }

    public void SoltarObjeto()
    {
        puntoImantado.transform.SetParent(null, true);
        StartCoroutine(conectarIman());
        rb.isKinematic = false;
        obtenido = false;
    }

    private IEnumerator conectarIman()
    {
        yield return new WaitForSeconds(1);
        collider.enabled = true;
    }
}
