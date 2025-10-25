using UnityEngine;

public class PickupItem : MonoBehaviour
{
    public ItemSO itemData;          // Arrastrar aquí tu ItemSO de la llave
    public float interactDistance = 3f; // Distancia máxima para recoger

    private Camera playerCamera;

    void Start()
    {
        playerCamera = Camera.main; // detecta la cámara principal
        if (playerCamera == null)
            Debug.LogError("No se encontró Camera.main en la escena!");
    }

    void Update()
    {
        // Detecta tecla E
        if (Input.GetKeyDown(KeyCode.E))
        {
            TryPickup();
        }
    }

    void TryPickup()
    {
        if (playerCamera == null) return;

        // Raycast desde la cámara hacia adelante
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactDistance))
        {
            // Solo recoge si estás mirando exactamente la llave
            if (hit.collider.gameObject == this.gameObject)
            {
                if (Inventory.instance == null)
                {
                    Debug.LogError("No hay Inventory.instance en la escena!");
                    return;
                }

                // Añade la llave al inventario
                if (Inventory.instance.AddItem(itemData))
                {
                    Debug.Log("Has recogido: " + itemData.displayName);
                    Destroy(gameObject); // desaparece del mundo
                }
                else
                {
                    Debug.Log("Inventario lleno.");
                }
            }
        }
    }
}
