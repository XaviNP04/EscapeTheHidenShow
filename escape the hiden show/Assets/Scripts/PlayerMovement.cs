using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController controller;

    public float walkSpeed = 6f;
    public float runSpeed = 12f;
    public float gravity = -9.81f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded;
    float alturaNormal;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        alturaNormal = controller.height;
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        // agacharse

        float h = alturaNormal;

        if (Input.GetKey("c"))
        {
            h = 0.3f * alturaNormal;
        }

        var ultAltura = controller.height;
        controller.height = Mathf.Lerp(controller.height, h, 5 * Time.deltaTime); // que se agache lentamente
        transform.position = transform.position + new Vector3(transform.position.x, (controller.height-ultAltura)/2, transform.position.z);

        // fin agacharse

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float currentSpeed = isRunning ? runSpeed : walkSpeed;

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * currentSpeed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

    }
}
