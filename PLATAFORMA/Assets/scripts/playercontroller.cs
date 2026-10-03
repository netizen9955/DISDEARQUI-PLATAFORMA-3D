using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private InputAction moveAction;
    private InputAction jumpAction;

    public Vector2 MoveValue { get; private set; }
    public bool IsJumpPressed { get; private set; }

    void Awake()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        jumpAction = InputSystem.actions.FindAction("Jump");
    }

    void Update()
    {
        if (moveAction != null)
        {
            MoveValue = moveAction.ReadValue<Vector2>();
        }

        if (jumpAction != null && jumpAction.WasPressedThisFrame())
        {
            IsJumpPressed = true;
        }
    }

    public void UseJump()
    {
        IsJumpPressed = false;
    }
}