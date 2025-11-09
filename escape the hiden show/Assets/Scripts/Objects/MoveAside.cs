using UnityEngine;

public class MoveAside : MonoBehaviour, IInteractable
{
    public enum MoveDirection { Forward, Backward, Left, Right, Up, Down }

    [Header("Movement Settings")]
    public MoveDirection direction = MoveDirection.Left;
    public float moveDistance = 1f;
    private bool moved = false;

    public void Interact()
    {
        if (!moved) {
            moved = true;
            gameObject.tag = "Untagged";
            Vector3 moveVector = GetMoveDirection();
            transform.position += moveVector * moveDistance;
        }
    }

    private Vector3 GetMoveDirection()
    {
        switch (direction)
        {
            case MoveDirection.Forward:
                return transform.forward;
            case MoveDirection.Backward:
                return -transform.forward;
            case MoveDirection.Left:
                return -transform.right;
            case MoveDirection.Right:
                return transform.right;
            case MoveDirection.Up:
                return transform.up;
            case MoveDirection.Down:
                return -transform.up;
            default:
                return -transform.right; // Default
        }
    }
}
