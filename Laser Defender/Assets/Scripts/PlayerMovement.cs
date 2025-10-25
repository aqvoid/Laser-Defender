using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;

    private InputAction moveAction;
    private Vector2 moveInput;

    private PlayerInput playerInput;
    private Rigidbody2D rb;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        moveAction = playerInput.actions["Move"];
        moveAction.performed += OnMove;
        moveAction.canceled += OnMove;
    }

    private void OnDisable()
    {
        moveAction.performed -= OnMove;
        moveAction.canceled -= OnMove;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void OnMove(InputAction.CallbackContext ctx) => moveInput = ctx.ReadValue<Vector2>();

    private void Move()
    {
        Vector2 velocity = (moveInput.normalized * moveSpeed) - rb.linearVelocity;
        rb.AddForce(velocity, ForceMode2D.Force);
    }
}
