using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [Tooltip("Asigna las 5 Image (iconos) en el inspector en el orden del slot 0..4")]
    public Image[] slotImages; // tamaño 5

    private void OnEnable()
    {
        // si Inventory.instance ya existe, suscribimos; si no, intentamos más tarde en Start
        if (Inventory.instance != null)
            Inventory.instance.OnInventoryChanged += UpdateUI;
    }

    private void OnDisable()
    {
        if (Inventory.instance != null)
            Inventory.instance.OnInventoryChanged -= UpdateUI;
    }

    private void Start()
    {
        // Si nos suscribimos en OnEnable y inventory no existía antes, intentar ahora
        if (Inventory.instance != null)
        {
            Inventory.instance.OnInventoryChanged -= UpdateUI; // evitar doble subscripción
            Inventory.instance.OnInventoryChanged += UpdateUI;
        }

        UpdateUI();
    }

    public void UpdateUI()
    {
        // ocultar todos
        for (int i = 0; i < slotImages.Length; i++)
        {
            if (slotImages[i] == null) continue;
            slotImages[i].sprite = null;
            slotImages[i].enabled = false;
        }

        // rellenar con items actuales
        if (Inventory.instance == null) return;

        for (int i = 0; i < Inventory.instance.items.Count && i < slotImages.Length; i++)
        {
            var item = Inventory.instance.items[i];
            if (slotImages[i] != null && item != null)
            {
                slotImages[i].sprite = item.icon;
                slotImages[i].enabled = true;
            }
        }
    }
}
