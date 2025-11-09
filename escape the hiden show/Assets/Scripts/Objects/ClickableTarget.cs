using UnityEngine;

public class ClickableTarget : MonoBehaviour
{
    public Texture2D cursorTexture;
    public Vector2 hotSpot = Vector2.zero;

    // Se llama cuando el ratón entra en el Collider del objeto
    void OnMouseEnter()
    {
        // Cambia el cursor a la nueva textura
        Cursor.SetCursor(cursorTexture, hotSpot, CursorMode.Auto);
    }

    // Se llama cuando el ratón sale del Collider del objeto
    void OnMouseExit()
    {
        // Restaura el cursor al valor predeterminado (o nulo)
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }
}
