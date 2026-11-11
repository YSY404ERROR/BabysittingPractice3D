using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement:MonoBehaviour
{
    [Header("-------Movement-------")]
    [SerializeField] private float moveSpeed = 5.5f;
    [SerializeField] private Transform orientation;
    [SerializeField] private float JumpForce = 8.0f;
    [SerializeField] private float RunningSpeed = 9.0f;
    [SerializeField] private float Gravity = 20.0f;

    private InputAction moveInput;
    private InputAction jumpInput;
    private bool jumped = false;
    private InputAction runInput;

    private float currentMoveSpeed = 0.0f;
    private Vector3 movementDirection = Vector3.zero;

    private CharacterController characterController;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();

        moveInput = InputSystem.actions.FindAction("Move");
        jumpInput = InputSystem.actions.FindAction("Jump");
        jumpInput.started += Jumped;
        runInput = InputSystem.actions.FindAction("Sprint");

        currentMoveSpeed = moveSpeed;
    }

    private void Update()
    {
        MyInput();
    }

    private void MyInput()
    {
        Vector2 moveVector = moveInput.ReadValue<Vector2>();

        if (!characterController.isGrounded)
            jumped = false;

        currentMoveSpeed = runInput.IsPressed() ? RunningSpeed : moveSpeed;
        HandleMovement(moveVector);
    }

    private void HandleMovement(Vector2 moveVector)
    {
        Vector3 forward = orientation.forward;
        Vector3 right = orientation.right;

        forward.y = 0;
        right.y = 0;

        forward.Normalize();
        right.Normalize();
        float oldY = movementDirection.y;

        Vector2 newSpeed = new Vector2(moveVector.y * currentMoveSpeed, moveVector.x * currentMoveSpeed);

        movementDirection = (forward * newSpeed.x) + (right * newSpeed.y);
        if (jumped && characterController.isGrounded)
            movementDirection.y = JumpForce;
        else
            movementDirection.y = oldY;

        if (!characterController.isGrounded)
            movementDirection.y -= Gravity * Time.deltaTime;
            
        characterController.Move(movementDirection * Time.deltaTime);
    }

    private void Jumped(InputAction.CallbackContext _)
    {
        jumped = true;
    }
}