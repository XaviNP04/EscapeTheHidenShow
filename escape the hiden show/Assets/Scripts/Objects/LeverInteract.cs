using UnityEngine;

public class LeverInteract : MonoBehaviour
{
    [Header("Lever Setup")]
    public Transform leverHandle;
    public float interactDistance = 3f;
    public float rotationAmount = 40f;
    public float rotationSpeed = 5f;

    [Header("Ejes configurables (por si tu modelo está rotado)")]
    public Vector3 upRotationAxis = Vector3.right;
    public Vector3 downRotationAxis = Vector3.left;
    public Vector3 leftRotationAxis = Vector3.forward;
    public Vector3 rightRotationAxis = Vector3.back;

    private Camera playerCamera;
    private Quaternion neutralRotation;
    private Quaternion targetRotation;
    private bool isLookingAtLever;

    private enum LeverDirection { Center, Up, Down, Left, Right }

    public System.Action<LeverInteract, string> OnLeverMoved;

    private LeverDirection currentDirection = LeverDirection.Center;

    // Para evitar que cambie de dirección sin pasar por el centro
    private bool mustReturnToCenter = false;

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
        DetectPlayerLooking();

        if (isLookingAtLever && Input.GetKey(KeyCode.E))
        {
            if (Input.GetKeyDown(KeyCode.UpArrow)) TrySetDirection(LeverDirection.Up);
            else if (Input.GetKeyDown(KeyCode.DownArrow)) TrySetDirection(LeverDirection.Down);
            else if (Input.GetKeyDown(KeyCode.LeftArrow)) TrySetDirection(LeverDirection.Left);
            else if (Input.GetKeyDown(KeyCode.RightArrow)) TrySetDirection(LeverDirection.Right);
        }

        leverHandle.localRotation = Quaternion.Lerp(
            leverHandle.localRotation,
            targetRotation,
            Time.deltaTime * rotationSpeed
        );
    }

    void DetectPlayerLooking()
    {
        isLookingAtLever = false;
        if (playerCamera == null) return;

        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, interactDistance))
        {
            if (hit.collider.gameObject == gameObject)
                isLookingAtLever = true;
        }
    }

    void TrySetDirection(LeverDirection dir)
    {
        // Si debe volver al centro, forzamos eso primero
        if (mustReturnToCenter)
        {
            SetDirection(LeverDirection.Center);
            mustReturnToCenter = false;
            Debug.Log("Palanca volviendo al centro");
            return;
        }

        // Si está centrada, permitimos movimiento
        if (currentDirection == LeverDirection.Center)
        {
            SetDirection(dir);
            mustReturnToCenter = true; // ahora debe volver al centro antes de moverse otra vez
        }
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
