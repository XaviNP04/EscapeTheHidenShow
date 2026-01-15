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
    private float alturaNormal;
    private float alturaCamaraNormal;

    private AudioSource audio;
    private float timer = 0f;
    [SerializeField] private float walkInterval = 0.2f;
    [SerializeField] private float runInterval = 0.6f;
    private float interval;

    Vector3 lastPosition;
    float horizontalSpeed;

    void Start()
    {
        _charController = GetComponent<CharacterController>();
        _camera = _camera = GetComponentInChildren<Camera>();
        Cursor.lockState = CursorLockMode.Locked; // deja el rat�n en el centro de la ventana
        Cursor.visible = false;

        alturaNormal = _charController.height;
        alturaCamaraNormal = _camera.transform.localPosition.y;

        audio = GetComponent<AudioSource>();

        lastPosition = transform.position;
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
        if (DialogueSystem.dialogueActive)
            return;

        float deltaX = Input.GetAxis("Horizontal"); // Las teclas asociadas est�n en:
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
        }
        else if (canUncrouch)
        {
            targetHeight = alturaNormal;
        }
        else
        {
            // Can't uncrouch due to low ceiling, stay crouched
            targetHeight = 0.3f * alturaNormal;
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


        // Sonido andar
        if (isRunning)
        {
            interval = walkInterval;
        } else
        {
            interval = runInterval;
        }

        float currentSpeed = isRunning ? runSpeed : walkSpeed;


        Vector3 movement = new Vector3(deltaX, 0, deltaZ);

        movement = Vector3.ClampMagnitude(movement, 1.0f) * currentSpeed;

        Vector3 horizontalMovement = transform.position - lastPosition;
        horizontalMovement.y = 0; // ignore vertical

        horizontalSpeed = horizontalMovement.magnitude / Time.deltaTime;

        if (horizontalSpeed > 5)
        {
            timer += Time.deltaTime;

            if (timer >= interval)
            {
                audio.Play();
                timer = 0f;
            }
        }

        lastPosition = transform.position;


        // gravedad (para escaleras)
        velocity.y += gravity * Time.deltaTime;
        if (!isGrounded && velocity.y < -20f)
        {
            velocity.y = -20f;
        }

        _charController.Move(velocity * Time.deltaTime);

        movement = transform.TransformDirection(movement); // convierte desde el sistema local al global
        _charController.Move(movement * Time.deltaTime); // no movemos el transform para que se calculen las colisiones 

    } 
}