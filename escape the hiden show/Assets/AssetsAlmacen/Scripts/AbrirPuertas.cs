using UnityEngine;

public class AbrirPuertas : MonoBehaviour
{
    [SerializeField] private string objectID;
    [SerializeField] private Animator puertas;
    Camera _camera;
    private Material mat;

    void Start()
    {
        _camera = Camera.main;
        mat = GetComponent<Renderer>().material;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Vector3 point = new Vector3(_camera.pixelWidth / 2, _camera.pixelHeight / 2, 0);
            Ray ray = _camera.ScreenPointToRay(point);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 3f))
            {
                var hitObject = hit.transform.gameObject;
                if (hitObject == this.gameObject)
                {
                    if (Inventory.instance.HasItem(objectID))
                    {
                        mat.EnableKeyword("_EMISSION");
                        mat.SetColor("_EmissionColor", Color.green);
                        Inventory.instance.RemoveItemByID(objectID);
                        puertas.enabled = true;
                        Debug.Log("Llave Colocada");
                    }
                    else
                    {
                        Debug.Log("Parece que se necesita una tarjeta para abrir");
                    }
                }
            }
        }
    }
}
