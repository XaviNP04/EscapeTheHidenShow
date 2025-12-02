using UnityEngine;

public class MontarManiqui : MonoBehaviour
{
    [SerializeField] private GameObject cabeza;
    [SerializeField] private GameObject brazoDer;
    [SerializeField] private GameObject brazoIzq;
    [SerializeField] private GameObject piernaIzq;
    [SerializeField] private string cabezaID;
    [SerializeField] private string brazoDerID;
    [SerializeField] private string brazoIzqID;
    [SerializeField] private string piernaIzqID;

    private bool completo = false;
    private int partesActivas = 0;
    [SerializeField] private GameObject tarjetaAcceso;

    private Camera _camera;

    void Start()
    {
        _camera = Camera.main;
        cabeza.SetActive(false);
        brazoDer.SetActive(false);
        brazoIzq.SetActive(false);
        piernaIzq.SetActive(false);
        tarjetaAcceso.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !completo)
        {
            Vector3 point = new Vector3(_camera.pixelWidth / 2, _camera.pixelHeight / 2, 0);
            Ray ray = _camera.ScreenPointToRay(point);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 5f))
            {
                var hitObject = hit.transform.gameObject;
                if (hitObject == this.gameObject)
                {
                    if (Inventory.instance.HasItem(cabezaID))
                    {
                        cabeza.SetActive(true);
                        Inventory.instance.RemoveItemByID(cabezaID);
                        partesActivas++;
                    }
                    if (Inventory.instance.HasItem(brazoDerID))
                    {
                        brazoDer.SetActive(true);
                        Inventory.instance.RemoveItemByID(brazoDerID);
                        partesActivas++;
                    }
                    if (Inventory.instance.HasItem(brazoIzqID))
                    {
                        brazoIzq.SetActive(true);
                        Inventory.instance.RemoveItemByID(brazoIzqID);
                        partesActivas++;
                    }
                    if (Inventory.instance.HasItem(piernaIzqID))
                    {
                        piernaIzq.SetActive(true);
                        Inventory.instance.RemoveItemByID(piernaIzqID);
                        partesActivas++;
                    }

                    if (partesActivas == 4)
                    {
                        tarjetaAcceso.SetActive(true);
                        completo = true;
                    }
                }
            }
        }
    }

}
