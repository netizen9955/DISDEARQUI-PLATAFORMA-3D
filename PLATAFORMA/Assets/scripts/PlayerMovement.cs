using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float velocity = 5f;
    [SerializeField] private float jumpForce = 3f;
    [SerializeField] private float rotationSpeed = 20f;

    [Header("Doble Salto & Suelo")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    public bool canDoubleJump = false;
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
        Jump();
    }

    private void CheckGround()
    {
        if (groundCheck != null)
        {
            isGrounded = Physics.CheckSphere(groundCheck.position, 0.2f, groundLayer);
        }
        else
        {
            isGrounded = Mathf.Abs(rb.linearVelocity.y) < 0.05f;
        }

        if (isGrounded)
        {
            hasDoubleJumped = false;
        }
    }

    private void Move()
    {
        float speed = velocity * currentVelocityMultiplier;
       Vector2 input = playerController.MoveValue;

if (input.sqrMagnitude > 1f)
{
    input.Normalize();
}

Vector3 targetVelocity = new Vector3(
    input.x * speed,
    rb.linearVelocity.y,
    input.y * speed
);

rb.linearVelocity = Vector3.Lerp(
    rb.linearVelocity,
    targetVelocity,
    10f * Time.fixedDeltaTime
);
    }

    private void Rotate()
    {
        Vector2 moveInput = playerController.MoveValue;

        if (moveInput.sqrMagnitude > 0.01f)
        {
            Vector3 direction = new Vector3(moveInput.x, 0f, moveInput.y);
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            rb.MoveRotation(
                Quaternion.Slerp(rb.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime)
            );
        }
    }

    private void Jump()
    {
        if (playerController.IsJumpPressed)
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

            playerController.IsJumpPressed = false;
        }
    }

    private void ExecuteJump()
    {
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpForce, rb.linearVelocity.z);
    }

    public void SetSpeedMultiplier(float multiplier)
    {
        currentVelocityMultiplier = multiplier;
    }
}