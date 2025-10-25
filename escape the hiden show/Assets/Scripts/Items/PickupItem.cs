using UnityEngine;

public class PickupItem : MonoBehaviour
{
    public ItemSO itemData;          // Arrastrar aqu� tu ItemSO de la llave
    public float interactDistance = 3f; // Distancia m�xima para recoger

    private Camera playerCamera;

    void Start()
    {
        playerCamera = Camera.main; // detecta la c�mara principal
        if (playerCamera == null)
            Debug.LogError("No se encontr� Camera.main en la escena!");
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

        // Raycast desde la c�mara hacia adelante
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactDistance))
        {
            // Solo recoge si est�s mirando exactamente la llave
            if (hit.collider.gameObject == this.gameObject)
            {
                if (Inventory.instance == null)
                {
                    Debug.LogError("No hay Inventory.instance en la escena!");
                    return;
                }

                // A�ade la llave al inventario
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
