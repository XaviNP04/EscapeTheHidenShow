using UnityEngine;
[RequireComponent(typeof(CharacterController))] // obliga a que el GameObject tenga cierto componente
public class PlayerMovement : MonoBehaviour
{

    private CharacterController _charController;
    private Camera _camera;

    public float walkSpeed = 6f;
    public float runSpeed = 12f;
    public float crounchSpeed = 3f;
    public float gravity = -19.4f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    public Transform ceilingCheck;
    public float ceilingDistance = 0.4f;
    public LayerMask ceilingMask;


    private Vector3 velocity;
    private bool isGrounded;
    private bool isCrouching;
    private float alturaNormal;
    private float alturaCamaraNormal;

    void Start()
    {
        _charController = GetComponent<CharacterController>();
        _camera = _camera = GetComponentInChildren<Camera>();
        Cursor.lockState = CursorLockMode.Locked; // deja el ratón en el centro de la ventana
        Cursor.visible = false;

        alturaNormal = _charController.height;
        alturaCamaraNormal = _camera.transform.localPosition.y;
    }

    bool CanUncrouch()
    {
        if (ceilingCheck == null) return true;

        // Check if there's enough space to stand up
        bool hasLowCeiling = Physics.CheckSphere(ceilingCheck.position, ceilingDistance, ceilingMask);

        return !hasLowCeiling;
    }

    void Update()
    {
        float deltaX = Input.GetAxis("Horizontal"); // Las teclas asociadas están en:
        float deltaZ = Input.GetAxis("Vertical"); // Edit\Project Settings\Input

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        //ceilingAbove = Physics.CheckSphere(ceilingCheck.position, ceilingDistance, ceilingMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        // agacharse
        bool wantsToCrouch = Input.GetKey(KeyCode.LeftControl);
        bool canUncrouch = CanUncrouch();

        // Determine target height
        float targetHeight;
        if (wantsToCrouch)
        {
            targetHeight = 0.3f * alturaNormal;
            isCrouching = true;
        }
        else if (canUncrouch)
        {
            targetHeight = alturaNormal;
            isCrouching = false;
        }
        else
        {
            // Can't uncrouch due to low ceiling, stay crouched
            targetHeight = 0.3f * alturaNormal;
            isCrouching = true;
        }

        // Store previous height for position adjustment
        float previousHeight = _charController.height;

        // Smoothly interpolate character controller height
        _charController.height = Mathf.Lerp(_charController.height, targetHeight, 5 * Time.deltaTime);

        // Adjust camera height proportionally to character height
        float cameraHeightRatio = _charController.height / alturaNormal;
        Vector3 cameraPos = _camera.transform.localPosition;
        cameraPos.y = alturaCamaraNormal * cameraHeightRatio;
        _camera.transform.localPosition = cameraPos;

        // Adjust position to account for height change
        float heightChange = _charController.height - previousHeight;
        transform.position += new Vector3(0, heightChange * 0.5f, 0);

        // correr
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float currentSpeed = isRunning ? runSpeed : walkSpeed;

        Vector3 movement = new Vector3(deltaX, 0, deltaZ);
        movement = Vector3.ClampMagnitude(movement, 1.0f) * currentSpeed;

        // gravedad (para escaleras)
        velocity.y += gravity * Time.deltaTime;

        _charController.Move(velocity * Time.deltaTime);

        movement = transform.TransformDirection(movement); // convierte desde el sistema local al global
        _charController.Move(movement * Time.deltaTime); // no movemos el transform para que se calculen las colisiones 
    } 
}
