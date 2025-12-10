using UnityEngine;

public class ObjetoRequerido : MonoBehaviour
{
    [SerializeField] private string objectID;
    Camera _camera;
    private Renderer visibilidad;
    private Collider collider; 
    
    [SerializeField] private GameObject luz;
    private Material mat;

    void Start()
    {
        _camera = Camera.main;
        visibilidad = GetComponent<Renderer>();
        visibilidad.enabled = false;
        collider = GetComponent<Collider>();

        mat = luz.GetComponent<Renderer>().material;
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
                        visibilidad.enabled = true;
                        Inventory.instance.RemoveItemByID(objectID);
                        collider.enabled = false;
                        Debug.Log("Llave Colocada");
                    }
                    else
                    {
                        Debug.Log("Parece que se necesita una llave para usar el panel");
                    }
                }
            }
        }
    }
}
