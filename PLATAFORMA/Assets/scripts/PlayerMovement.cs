using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float velocity = 5f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float rotationSpeed = 20f;

    [Header("Doble Salto & Suelo")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    public bool canDoubleJump = true;
    private bool hasDoubleJumped = false;
    private bool isGrounded;
    private float currentVelocityMultiplier = 1f;

    private void Start()
    {
        if (rb == null) rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        rb.angularVelocity = Vector3.zero; 
        CheckGround();
        Move();
        Rotate();
        HandleJumpPhysics();
    }

    private void CheckGround()
    {
        if (groundCheck != null)
        {
            isGrounded = Physics.CheckSphere(groundCheck.position, 0.25f, groundLayer);
        }
        else
        {
            isGrounded = Mathf.Abs(rb.linearVelocity.y) < 0.1f;
        }

        if (isGrounded)
        {
            hasDoubleJumped = false;
        }
    }

    private Vector3 GetCameraRelativeDirection()
    {
        Vector2 input = playerController.MoveValue;
        if (input.sqrMagnitude > 1f) input.Normalize();

        Transform mainCam = Camera.main.transform;
        Vector3 camForward = mainCam.forward;
        Vector3 camRight = mainCam.right;

        camForward.y = 0f;
        camRight.y = 0f;
        camForward.Normalize();
        camRight.Normalize();

        return (camRight * input.x) + (camForward * input.y);
    }

    private void Move()
    {
        float speed = velocity * currentVelocityMultiplier;
        Vector3 moveDir = GetCameraRelativeDirection();

        Vector3 targetVelocity = new Vector3(
            moveDir.x * speed,
            rb.linearVelocity.y,
            moveDir.z * speed
        );

        rb.linearVelocity = Vector3.Lerp(
            rb.linearVelocity,
            targetVelocity,
            15f * Time.fixedDeltaTime
        );
    }

    private void Rotate()
    {
        Vector3 moveDir = GetCameraRelativeDirection();

        if (moveDir.sqrMagnitude > 0.01f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDir);

            rb.MoveRotation(
                Quaternion.Slerp(rb.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime)
            );
        }
    }

    private void HandleJumpPhysics()
    {
        if (playerController != null && playerController.IsJumpPressed)
        {
            if (isGrounded)
            {
                ExecuteJump();
            }
            else if (canDoubleJump && !hasDoubleJumped)
            {
                ExecuteJump();
                hasDoubleJumped = true;
            }
        }
    }

    private void ExecuteJump()
    {
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpForce, rb.linearVelocity.z);
        
        playerController.UseJump();
    }

    public void SetSpeedMultiplier(float multiplier)
    {
        currentVelocityMultiplier = multiplier;
    }
}