using UnityEngine;

public class ViewedTarget : MonoBehaviour
{
    private Camera _camera;

    private bool isInspecting = false;

    // Para guardar la posici�n y rotaci�n originales al iniciar la inspecci�n.
    private Vector3 originalPosition;
    private Quaternion originalRotation;
    private Vector3 originalScale;
    private Transform originalParent;
    private Collider collider;
    private GameObject Player;
    void Start()
    {
        Player = GameObject.FindWithTag("player");
        if (Player == null)
        {
            Debug.LogWarning("Player with tag 'player' not found in scene.");
        }
    }

    [SerializeField] private Transform viewPoint;

    public void View()
    {
        _camera = Camera.main;

        // Guardar el estado original del objeto:
        originalParent = _camera.transform.parent;
        originalPosition = _camera.transform.position;
        originalRotation = _camera.transform.rotation;
        originalScale = _camera.transform.localScale;

        collider = GetComponent<Collider>();

        // Deshabilitar su Collier:
        collider.enabled = false; 

        // Posicionar en el punto de inspecci�n:
        _camera.transform.SetParent(viewPoint); // Lo hace hijo del InspectionPoint
        _camera.transform.localPosition = Vector3.zero; // Posiciona el objeto exactamente en el punto
        _camera.transform.localRotation = Quaternion.identity; // Opcional: Resetea su rotaci�n local
        _camera.transform.localScale = Vector3.one; // Forzar a que la escala sea 1 para evitar posibles deformaciones

        isInspecting = true;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void Update()
    {
        if (isInspecting)
        {
            // Quitar el action display
            gameObject.tag = "Untagged";

            if (Input.GetKeyDown(KeyCode.Q))
            {
                // Restaurar el estado original del objeto:
                _camera.transform.SetParent(originalParent); // Devuelve al padre original (null si estaba suelto)
                _camera.transform.position = originalPosition;
                _camera.transform.rotation = originalRotation;
                _camera.transform.localScale = originalScale;

                // Para volver a abilitar el display ('INSPECCIONAR')
                gameObject.tag = "ImportantItem";

                // Volver a habilitar su Colisionador:
                collider.enabled = true; 

                // Desactivar el modo de inspecci�n y desbloquear al jugador:
                isInspecting = false;

                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }

        }
    }
}
