using UnityEngine;

public class LeverInteractV2 : MonoBehaviour
{
    [Header("Lever Setup")]
    public Transform leverHandle;
    public float interactDistance = 3f;
    public float rotationAmount = 40f;
    public float rotationSpeed = 5f;

    [Header("Ejes configurables")]
    public Vector3 upRotationAxis = Vector3.right;
    public Vector3 downRotationAxis = Vector3.left;
    public Vector3 leftRotationAxis = Vector3.forward;
    public Vector3 rightRotationAxis = Vector3.back;

    private Camera playerCamera;
    private Quaternion neutralRotation;
    private Quaternion targetRotation;
    private bool isLookingAtLever;

    private enum LeverDirection { Center = 0, Right = 1, Down = 2, Left = 3, Up = 4 }
    private const int TOTAL_DIRECTIONS = 5; // Número total de estados en el enum

    public System.Action<LeverInteractV2, string> OnLeverMoved;
    private LeverDirection currentDirection = LeverDirection.Center;
    private LeverDirection previousDirection = LeverDirection.Center;

    void Start()
    {
        playerCamera = Camera.main;
        if (leverHandle == null)
            leverHandle = transform;

        neutralRotation = leverHandle.localRotation;
        targetRotation = neutralRotation;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, interactDistance))
            {
                if (hit.transform.gameObject == this.gameObject)
                {
                    // Lógica para avanzar al siguiente estado en el ciclo
                    int nextDirIndex = ((int)currentDirection + 1) % TOTAL_DIRECTIONS;
                    LeverDirection nextDirection = (LeverDirection)nextDirIndex;

                    previousDirection = currentDirection;

                    SetDirection(nextDirection);
                }
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, interactDistance))
            {
                if (hit.transform.gameObject == this.gameObject)
                {
                    SetDirection(previousDirection);
                }
            }
        }

        leverHandle.localRotation = Quaternion.Lerp(
            leverHandle.localRotation,
            targetRotation,
            Time.deltaTime * rotationSpeed
        );
    }

    void SetDirection(LeverDirection dir)
    {
        currentDirection = dir;

        switch (dir)
        {
            case LeverDirection.Center:
                targetRotation = neutralRotation;
                break;
            case LeverDirection.Up:
                targetRotation = Quaternion.AngleAxis(rotationAmount, upRotationAxis) * neutralRotation;
                break;
            case LeverDirection.Down:
                targetRotation = Quaternion.AngleAxis(rotationAmount, downRotationAxis) * neutralRotation;
                break;
            case LeverDirection.Left:
                targetRotation = Quaternion.AngleAxis(rotationAmount, leftRotationAxis) * neutralRotation;
                break;
            case LeverDirection.Right:
                targetRotation = Quaternion.AngleAxis(rotationAmount, rightRotationAxis) * neutralRotation;
                break;
        }

        Debug.Log($"Palanca movida a {dir}");

        if (OnLeverMoved != null)
            OnLeverMoved.Invoke(this, dir.ToString());

    }
}
