using UnityEngine;

public class InspectedTarget : MonoBehaviour
{
    private Transform inspectionPoint;

    // Velocidad de rotación
    public float rotationSpeed = 300f;
    public float minDistanceZ = -0.5f;
    public float maxDistanceZ = 1.0f;
    public float zoomSpeed = 5.0f;

    private bool isInspecting = false;

    // Para guardar la posición y rotación originales al iniciar la inspección.
    private Vector3 originalPosition;
    private Quaternion originalRotation;
    private Transform originalParent;
    private Collider collider;
    private Vector3 centerOffset;
    
    public void Inspect(Transform iPoint)
    {
        inspectionPoint = iPoint;

        // Guardar el estado original del objeto:
        originalParent = transform.parent;
        originalPosition = transform.position;
        originalRotation = transform.rotation;

        collider = GetComponent<Collider>();
        centerOffset = transform.position - collider.bounds.center;

        // Deshabilitar su Collier (opcional, pero ayuda a que no bloquee otros Raycasts):
        // GetComponent<Collider>().enabled = false; 

        // Posicionar en el punto de inspección:
        transform.SetParent(inspectionPoint); // Lo hace hijo del InspectionPoint
        transform.localPosition = Vector3.zero + centerOffset; // Posiciona el objeto exactamente en el punto
        transform.localRotation = Quaternion.identity; // Opcional: resetea su rotación local

        isInspecting = true;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void Update()
    {
        if(isInspecting)
        {
            float RotationX = 0f;
            float RotationY = 0f;

            // Quitar el action display
            gameObject.tag = "Untagged";

            // Rotación Vertical (Eje X) con W y S
            if (Input.GetKey(KeyCode.W))
            {
                RotationX = 1f; // Rota hacia arriba (o adelante)
            }
            else if (Input.GetKey(KeyCode.S))
            {
                RotationX = -1f; // Rota hacia abajo (o atrás)
            }

            // Rotación Horizontal (Eje Y) con A y D
            if (Input.GetKey(KeyCode.D))
            {
                RotationY = 1f; // Rota hacia la derecha
            }
            else if (Input.GetKey(KeyCode.A))
            {
                RotationY = -1f; // Rota hacia la izquierda
            }

            // El punto de pivote es la posición del objeto
            Vector3 pivot = collider.bounds.center;

            // La cantidad de rotación por frame
            float rotAmount = rotationSpeed * Time.deltaTime;

            // Rotación Horizontal (Eje Y)
            if (RotationY != 0f)
            {
                // Rotar alrededor del Eje Y del InspectionPoint
                Vector3 rotationAxisY = inspectionPoint.up;

                transform.RotateAround(
                    pivot,                  // El punto central de la rotación (donde está el InspectionPoint)
                    rotationAxisY,          // El eje Y (vertical) sobre el que gira
                    -RotationY * rotAmount 
                );
            }

            // Rotación Vertical (Eje X)
            if (RotationX != 0f)
            {
                // Rotar alrededor del Eje X del InspectionPoint
                Vector3 rotationAxisX = inspectionPoint.right;

                transform.RotateAround(
                    pivot,                  // El punto central de la rotación
                    rotationAxisX,          // El eje X (horizontal) sobre el que gira
                    RotationX * rotAmount 
                );
            }


            float scrollInput = Input.GetAxis("Mouse ScrollWheel");

            if (scrollInput != 0)
            {
                // Calcular la nueva posición Z del InspectionPoint
                Vector3 newPos = transform.localPosition;

                // Disminuimos la posición Z para ACERCAR el objeto a la cámara (scrollInput > 0)
                newPos.z -= scrollInput * zoomSpeed * Time.deltaTime * 50f;
                // El factor *50f es para hacer el zoom más sensible, ajústalo si es necesario.

                // Aplicar los límites de zoom
                newPos.z = Mathf.Clamp(newPos.z, minDistanceZ, maxDistanceZ);

                // Actualizar la posición local del InspectionPoint
                transform.localPosition = newPos;
            }


            if (Input.GetMouseButton(1))
            {
                // Restaurar el estado original del objeto:
                transform.SetParent(originalParent); // Devuelve al padre original (null si estaba suelto)
                transform.position = originalPosition;
                transform.rotation = originalRotation;

                // Para volver a abilitar el display ('INSPECCIONAR')
                gameObject.tag = "ImportantItem";

                // Volver a habilitar su Colisionador:
                //GetComponent<Collider>().enabled = true; 

                // Desactivar el modo de inspección y desbloquear al jugador:
                isInspecting = false;

                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }

            if (Input.GetMouseButton(0))
            {
                HandleComponentClick();
            }
        }
    }

    private void HandleComponentClick()
    {
        // Lanza un rayo desde la posición del ratón en la pantalla.
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 5f))
        {
            var hitObject = hit.transform.gameObject;
            var target = hitObject.GetComponent<ClickAction>();

            if (target != null)
            {
                // El componente golpeado pertenece al objeto que estamos inspeccionando
                if (target.transform.IsChildOf(this.gameObject.transform))
                {
                    target.Action();
                }
            }
        }
    }
}
