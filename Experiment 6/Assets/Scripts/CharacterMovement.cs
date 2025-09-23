using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class CharacterMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float walkSpeed = 5f;
    public float runSpeed = 8f;
    public float jumpHeight = 2f;
    public float gravity = -9.81f;
    public float groundCheckDistance = 0.1f;

    [Header("Mouse Look Settings")]
    public float mouseSensitivity = 2f;
    public float maxLookAngle = 80f;
    public Transform cameraTransform;

    [Header("Ground Check")]
    public LayerMask groundMask = 1;

    // Private variables
    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;
    private bool isRunning;
    private float xRotation = 0f;

    // Input variables
    private float horizontal;
    private float vertical;
    private bool jumpInput;
    private bool runInput;

    void Start()
    {
        // Get the CharacterController component
        controller = GetComponent<CharacterController>();

        // If no camera is assigned, try to find the main camera
        if (cameraTransform == null)
        {
            Camera mainCamera = Camera.main;
            if (mainCamera != null)
                cameraTransform = mainCamera.transform;
        }

        // Lock cursor to center of screen
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        HandleInput();
        HandleMouseLook();
        HandleMovement();
        HandleJump();
    }

    void HandleInput()
    {
        // Movement input
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        // Jump input
        jumpInput = Input.GetButtonDown("Jump");

        // Run input (Left Shift)
        runInput = Input.GetKey(KeyCode.LeftShift);

        // Toggle cursor lock with Escape
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Cursor.lockState == CursorLockMode.Locked)
                Cursor.lockState = CursorLockMode.None;
            else
                Cursor.lockState = CursorLockMode.Locked;
        }
    }

    void HandleMouseLook()
    {
        if (Cursor.lockState == CursorLockMode.Locked && cameraTransform != null)
        {
            // Get mouse input
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

            // Rotate the character body left and right
            transform.Rotate(Vector3.up * mouseX);

            // Rotate the camera up and down
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -maxLookAngle, maxLookAngle);
            cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        }
    }

    void HandleMovement()
    {
        // Check if grounded
        isGrounded = Physics.CheckSphere(transform.position, groundCheckDistance, groundMask);

        // Reset velocity when grounded
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Small downward force to keep grounded
        }

        // Determine current speed
        isRunning = runInput && isGrounded;
        float currentSpeed = isRunning ? runSpeed : walkSpeed;

        // Calculate movement direction
        Vector3 direction = transform.right * horizontal + transform.forward * vertical;
        direction = direction.normalized;

        // Move the character
        controller.Move(direction * currentSpeed * Time.deltaTime);
    }

    void HandleJump()
    {
        // Jump
        if (jumpInput && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // Apply gravity
        velocity.y += gravity * Time.deltaTime;

        // Apply vertical movement
        controller.Move(velocity * Time.deltaTime);
    }

    // Draw ground check sphere in scene view
    void OnDrawGizmosSelected()
    {
        Gizmos.color = isGrounded ? Color.green : Color.red;
        Gizmos.DrawWireSphere(transform.position, groundCheckDistance);
    }

    // Public methods for external scripts
    public bool IsGrounded()
    {
        return isGrounded;
    }

    public bool IsRunning()
    {
        return isRunning;
    }

    public Vector3 GetMovementInput()
    {
        return new Vector3(horizontal, 0, vertical);
    }
}