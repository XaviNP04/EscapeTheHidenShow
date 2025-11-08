using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;

    public int maxSlots = 5;
    public List<ItemSO> items = new List<ItemSO>();

    // Evento para notificar cambios (UI se suscribe)
    public event Action OnInventoryChanged;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    public bool AddItem(ItemSO item)
    {
        if (items.Count >= maxSlots)
        {
            Debug.Log("Inventario lleno");
            return false;
        }

        items.Add(item);
        OnInventoryChanged?.Invoke();
        Debug.Log($"Añadido: {item.displayName} ({item.itemID})");
        return true;
    }

    public bool RemoveItemByID(string itemID)
    {
        var item = items.Find(i => i.itemID == itemID);
        if (item != null)
        {
            items.Remove(item);
            OnInventoryChanged?.Invoke();
            Debug.Log($"Eliminado: {item.displayName} ({item.itemID})");
            return true;
        }
        return false;
    }

    public bool HasItem(string itemID)
    {
        return items.Exists(i => i.itemID == itemID);
    }

    // opcional: obtener ItemSO por id
    public ItemSO GetItemByID(string itemID)
    {
        return items.Find(i => i.itemID == itemID);
    }
}
