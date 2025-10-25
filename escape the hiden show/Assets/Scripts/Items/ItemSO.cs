using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Inventory/Item")]
public class ItemSO : ScriptableObject
{
    public string itemID;      // id único (ej: "llave_metal")
    public string displayName; // nombre visible (ej: "Llave vieja")
    public Sprite icon;        // sprite para UI
}
