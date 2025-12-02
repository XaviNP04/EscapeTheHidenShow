using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class ItemGlow : MonoBehaviour
{
    [Header("Highlight Settings")]
    public float maxDistance = 10f;
    public Color highlightColor = Color.grey;

    [Header("UI Settings")]
    public GameObject interactionUI; 
    public KeyCode interactionKey = KeyCode.E;
    public string defaultInteractionText = "Action"; 

    // Referenciar al Text component
    private TextMeshProUGUI interactionTextComponent;
    private Camera playerCamera;
    private GameObject currentHighlightedItem;
    private Color originalColor;
    //private Renderer itemRenderer;
    private Outline outline;
    private bool isLookingAtItem = false;

    void Start()
    {
        playerCamera = GetComponent<Camera>();

        if (interactionUI != null)
        {
            interactionTextComponent = interactionUI.GetComponentInChildren<TextMeshProUGUI>();
            if (interactionTextComponent == null)
            {
                Debug.LogError("No Text component found in interactionUI!");
            }
        }

        // Esconder UI al principio
        if (interactionUI != null)
            interactionUI.SetActive(false);
    }

    void OnGUI()
    { // se ejecuta despu�s de dibujar el frame del juego
        int size = 20;
        float posX = playerCamera.pixelWidth / 2 - size / 4;
        float posY = playerCamera.pixelHeight / 2 - size / 2;
        GUI.Label(new Rect(posX, posY, size, size), "+"); // puede mostrar texto e im�genes
    }

    void Update()
    {
        Ray ray = playerCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, maxDistance))
        {
            if (hit.collider.CompareTag("ImportantItem"))
            {
                if (currentHighlightedItem != hit.collider.gameObject)
                {
                    RemoveHighlight();
                    HighlightItem(hit.collider.gameObject);
                }

                // Actualizar posicion de UI
                UpdateUIPosition(hit.point);

                ActionText interactable = hit.collider.GetComponent<ActionText>();
                if (interactable != null)
                {
                    Debug.Log("Setting text to: " + interactable.interactionText);
                    ShowInteractionUI(interactable.interactionText);
                }
                else
                {
                    ShowInteractionUI(defaultInteractionText);
                }

                isLookingAtItem = true;
                return;
            }
        }

        // Si no estamos mirando hacia un objeto, esconder UI y quitar el highlight
        if (isLookingAtItem)
        {
            HideInteractionUI();
            RemoveHighlight();
            isLookingAtItem = false;
        }
    }

    void HighlightItem(GameObject item)
    {
        currentHighlightedItem = item;
        outline = item.GetComponent<Outline>();
        if (outline != null) outline.enabled = true;
    }

    void RemoveHighlight()
    {

        if (currentHighlightedItem != null && outline != null)
        {
            outline.enabled = false;
            currentHighlightedItem = null;
        }
    }

    void UpdateUIPosition(Vector3 worldPosition)
    {
        if (interactionUI != null)
        {
            // Convert world position to screen position
            Vector3 screenPosition = playerCamera.WorldToScreenPoint(worldPosition);

            // Offset the UI above the object
            screenPosition.y += 50f; // Adjust this value as needed

            interactionUI.transform.position = screenPosition;
        }
    }

    void ShowInteractionUI(string textToShow)
    {
        if (interactionUI != null)
        {
            interactionUI.SetActive(true);

            // Update interaction text
            if (interactionTextComponent != null)
            {
                interactionTextComponent.text = textToShow;
                Debug.Log("TextMeshPro component updated to: " + interactionTextComponent.text);
            }
            else
            {
                Debug.LogError("TextMeshPro component reference is null!");
            }
        }
    }

    void HideInteractionUI()
    {
        if (interactionUI != null)
            interactionUI.SetActive(false);
    }
}