using UnityEngine;

public class ClickAction : MonoBehaviour
{
    [SerializeField] private GameObject requiredObject;
    [SerializeField] private string objectID;

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

            requiredObject.tag = "Untagged";

            Inventory.instance.RemoveItemByID(objectID);

            requiredObject.GetComponent<PickupItem>().enabled = false;

            Debug.Log("Dial colocado");
        }
        else
        {
            Debug.Log("Aquí falta un dial.");
        }
    }
}
