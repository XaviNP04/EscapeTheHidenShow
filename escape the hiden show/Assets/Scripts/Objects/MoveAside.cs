using UnityEngine;

public class MoveAside : MonoBehaviour, IInteractable
{
    public float moveDistance = 1f;
    private bool moved = false;

    public void Interact()
    {
        if (!moved) {
            moved = true;
            gameObject.tag = "Untagged";
            Vector3 localLeft = transform.TransformDirection(Vector3.left);
            transform.position += localLeft * moveDistance;
        }
    }
}
