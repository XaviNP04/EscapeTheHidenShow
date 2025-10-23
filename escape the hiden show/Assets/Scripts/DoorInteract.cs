using UnityEngine;

public class DoorInteract : MonoBehaviour
{
    public string requiredKeyID = "key_001";
    public Transform doorHinge;
    public float openAngle = 90f;
    public float openSpeed = 2f;
    public float interactDistance = 3f;

    private bool isOpen = false;
    private bool alreadyUnlocked = false;
    private Quaternion closedRotation;
    private Quaternion openRotation;
    private Camera playerCamera;

    void Start()
    {
        closedRotation = doorHinge.localRotation;
        openRotation = closedRotation * Quaternion.Euler(0, openAngle, 0);
        playerCamera = Camera.main;
    }

    void Update()
    {
        // Animación de apertura suave
        if (isOpen)
            doorHinge.localRotation = Quaternion.Slerp(doorHinge.localRotation, openRotation, Time.deltaTime * openSpeed);

        // Detecta tecla E
        if (Input.GetKeyDown(KeyCode.E))
        {
            TryOpenDoor();
        }
    }

    void TryOpenDoor()
    {
        if (alreadyUnlocked) return; // ya abierta

        if (playerCamera == null) return;

        // Raycast desde la cámara
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactDistance))
        {
            if (hit.collider.gameObject == this.gameObject)
            {
                // Comprueba inventario
                if (Inventory.instance.HasItem(requiredKeyID))
                {
                    Inventory.instance.RemoveItemByID(requiredKeyID);
                    isOpen = true;
                    alreadyUnlocked = true;
                    Debug.Log("Puerta abierta con llave!");
                }
                else
                {
                    Debug.Log("La puerta está cerrada. Necesitas una llave.");
                }
            }
        }
    }
}
