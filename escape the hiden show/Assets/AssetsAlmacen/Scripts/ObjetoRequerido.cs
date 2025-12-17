using UnityEngine;

public class ObjetoRequerido : MonoBehaviour
{
    [SerializeField] private string objectID;
    Camera _camera;
    private Renderer visibilidad;
    private Collider collider; 
    
    [SerializeField] private GameObject luz;
    private Material mat;


    [Header("Diálogo al fallar")]
    [TextArea(2, 5)]
    [SerializeField] private string[] dialogueLines;

    private int intentosSinLlave = 0;

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
                        intentosSinLlave++;

                        Debug.Log("Parece que se necesita una llave para usar el panel");

                        if (intentosSinLlave == 1 || intentosSinLlave % 5 == 0)
                        {
                            if (DialogueSystem.instance != null)
                            {
                                DialogueSystem.instance.StartDialogue(dialogueLines);
                            }
                        }
                    }
                }
            }
        }
    }
}
