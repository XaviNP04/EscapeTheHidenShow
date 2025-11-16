using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [Tooltip("Asigna las 5 Image (iconos) en el inspector en el orden del slot 0..4")]
    public Image[] slotImages; // tama�o 5

    private void OnEnable()
    {
        // si Inventory.instance ya existe, suscribimos; si no, intentamos mas tarde en Start
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
        // Si nos suscribimos en OnEnable y inventory no existia antes, intentar ahora
        if (Inventory.instance != null)
        {
            Inventory.instance.OnInventoryChanged -= UpdateUI; // evitar doble subscripci�n
            Inventory.instance.OnInventoryChanged += UpdateUI;
        }

        UpdateUI();
    }

    public void UpdateUI()
    {
        if (slotImages == null || slotImages.Length == 0) return;

        
        for (int i = 0; i < slotImages.Length; i++)
        {
            if (slotImages[i] == null) continue;

            
            if (Inventory.instance == null || i >= Inventory.instance.items.Count)
            {
                slotImages[i].sprite = null;
                slotImages[i].color = new Color(0.1f, 0.1f, 0.1f, 0.01f);
                continue;
            }

            
            var item = Inventory.instance.items[i];
            if (item != null)
            {
                slotImages[i].sprite = item.icon;
                slotImages[i].color = Color.white;

                
                slotImages[i].preserveAspect = false;
                RectTransform rt = slotImages[i].rectTransform;
                rt.anchorMin = Vector2.zero;
                rt.anchorMax = Vector2.one;
                rt.offsetMin = Vector2.zero;
                rt.offsetMax = Vector2.zero;
            }
        }
    }

}
