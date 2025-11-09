using UnityEngine;

public class Interactor : MonoBehaviour
{
    public float InteractRange;  
  
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Ray  r = new Ray(transform.position, transform.forward);
            if (Physics.Raycast(r, out RaycastHit hitInfo, InteractRange))
            {
                if (hitInfo.collider.gameObject.TryGetComponent(out IInteractable interactObj))
                {
                    interactObj.Interact();
                }

            }
        }
    }
}
