using UnityEngine;
using System.Collections;

public class ItemGlow : MonoBehaviour
{
    [Header("Settings")]
    public float maxDistance = 10f;
    public Color highlightColor = Color.yellow;

    private Camera playerCamera;
    private GameObject currentHighlightedItem;
    private Color originalColor;
    private Renderer itemRenderer;

    void Start()
    {
        playerCamera = GetComponent<Camera>();
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
                return;
            }
        }

        RemoveHighlight();
    }

    void HighlightItem(GameObject item)
    {
        itemRenderer = item.GetComponent<Renderer>();
        if (itemRenderer != null)
        {
            currentHighlightedItem = item;
            originalColor = itemRenderer.material.color;
            itemRenderer.material.color = highlightColor;
        }
    }

    void RemoveHighlight()
    {
        if (currentHighlightedItem != null && itemRenderer != null)
        {
            itemRenderer.material.color = originalColor;
            currentHighlightedItem = null;
        }
    }
}
