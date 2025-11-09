using UnityEngine;

public class ClickAction : MonoBehaviour
{
    public GameObject requiredObject;
    public string objectID;

    void Start()
    {
    }

    public void Action()
    {
        if (Inventory.instance.HasItem(objectID))
        {
            requiredObject.SetActive(true);

            requiredObject.transform.SetParent(transform.parent);
            requiredObject.transform.localPosition = Vector3.zero;
            requiredObject.transform.localRotation = Quaternion.identity;
            requiredObject.transform.localScale = Vector3.one;

            Inventory.instance.RemoveItemByID(objectID);
            Debug.Log("Dial colocado");
        }
        else
        {
            Debug.Log("Aquí falta un dial.");
        }
    }
}
