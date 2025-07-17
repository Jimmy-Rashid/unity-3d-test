using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed;
    public float jumpHeight;
    private Rigidbody rb;
    public PlayerInputActions playerControls;
    private InputAction move;
    private InputAction jump;
    private Vector2 moveDirection = Vector2.zero;

    private void Awake()
    {
        playerControls = new PlayerInputActions();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    void Start()
    {
        move = playerControls.Player.Move;
        jump = playerControls.Player.Jump;
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        moveDirection = move.ReadValue<Vector2>();
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector3(moveDirection.x * movementSpeed, rb.linearVelocity.y, moveDirection.y * movementSpeed);
        //transform.LookAt(transform.position + rb.linearVelocity);

        if (playerControls.Player.Jump.triggered && Mathf.Abs(rb.linearVelocity.y) < 0.02f)
        {
            rb.AddForce(Vector3.up * rb.mass * jumpHeight, ForceMode.Impulse);
        }
    }
}
