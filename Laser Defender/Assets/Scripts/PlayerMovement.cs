using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;

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
        float threshold = Mathf.Epsilon;

        if (moveInput.magnitude > threshold)
        {
            moveInput = moveInput.normalized * ((moveInput.magnitude - threshold) / (1f - threshold));
            Vector2 newPos = rb.position + moveInput * moveSpeed * Time.fixedDeltaTime;
            rb.MovePosition(newPos);
        }
    }
}
