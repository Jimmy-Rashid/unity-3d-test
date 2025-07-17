using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed;
    private Rigidbody rb;
    public PlayerInputActions playerControls;
    private InputAction move;
    private InputAction jump;
    private Vector2 moveDirection = Vector2.zero;

    private void Awake() {
        playerControls = new PlayerInputActions();
    }

    private void OnEnable()
    {
        move = playerControls.Player.Move;
        jump = playerControls.Player.Jump;
        move.Enable();
        jump.Enable();
    }

    private void OnDisable()
    {
        move.Disable();
        jump.Disable();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        moveDirection = move.ReadValue<Vector2>();
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector3(moveDirection.x * movementSpeed, rb.linearVelocity.y, moveDirection.y * movementSpeed);
    }

    private void Jump(InputAction.CallbackContext context) {
        if (context.performed) {
            rb.AddForce(Vector3.up * rb.mass * 100);
        }
    }
}
