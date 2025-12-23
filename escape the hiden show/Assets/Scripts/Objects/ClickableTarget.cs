using UnityEngine;

public class ClickableTarget : MonoBehaviour
{
    public Texture2D cursorTexture;
    public Vector2 hotSpot = new Vector2(10f, 0f);

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
