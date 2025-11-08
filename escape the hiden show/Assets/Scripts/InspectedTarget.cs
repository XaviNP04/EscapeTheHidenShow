using UnityEngine;

public class InspectedTarget : MonoBehaviour
{
    private Transform inspectionPoint;

    // Velocidad de rotación
    public float rotationSpeed = 1000f;
    public float minDistanceZ = -0.5f;
    public float maxDistanceZ = 1.0f;
    public float zoomSpeed = 5.0f;

    private bool isInspecting = false;

    // Para guardar la posición y rotación originales al iniciar la inspección.
    private Vector3 originalPosition;
    private Quaternion originalRotation;
    private Transform originalParent;
    
    public void Inspect(Transform iPoint)
    {
        inspectionPoint = iPoint;

        // Guardar el estado original del objeto:
        originalParent = transform.parent;
        originalPosition = transform.position;
        originalRotation = transform.rotation;

        // Deshabilitar su Collier (opcional, pero ayuda a que no bloquee otros Raycasts):
        // GetComponent<Collider>().enabled = false; 

        // Posicionar en el punto de inspección:
        transform.SetParent(inspectionPoint); // Lo hace hijo del InspectionPoint
        transform.localPosition = Vector3.zero; // Posiciona el objeto exactamente en el punto
        transform.localRotation = Quaternion.identity; // Opcional: resetea su rotación local

        isInspecting = true;
    }

    void Update()
    {
        if(isInspecting)
        {
            float RotationX = 0f;
            float RotationY = 0f;

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
            Vector3 pivot = transform.position;

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

                // Volver a habilitar su Colisionador:
                //GetComponent<Collider>().enabled = true; 

                // Desactivar el modo de inspección y desbloquear al jugador:
                isInspecting = false;
            }
        }
    }
}
