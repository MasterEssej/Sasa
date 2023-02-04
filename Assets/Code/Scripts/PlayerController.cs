using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private InputMaster input;

    private InputAction movement;
    private InputAction interact;

    private Vector2 movementInput = Vector2.zero;

    private void OnEnable()
    {
        input ??= new InputMaster();

        movement = input.Player.Movement;
        interact = input.Player.Interact;

        input.Enable();

        movement.performed += OnMove;
        movement.canceled += OnMove;
        interact.performed += OnInteract;
    }

    private void OnDisable()
    {
        movement.performed += OnMove;
        movement.canceled += OnMove;
        interact.performed += OnInteract;

        input.Disable();
    }

    private void OnMove(InputAction.CallbackContext ctx)
    {
        Debug.Log($"moveInput{ctx.ReadValue<Vector2>()}");

        movementInput = ctx.ReadValue<Vector2>();

    }

    private void OnInteract(InputAction.CallbackContext ctx)
    {
        Debug.Log("Interacting...");
    }


    private void Move()
    {
        if (movementInput == Vector2.zero)
            return;

        var translation = new Vector3(movementInput.x, 0, movementInput.y) * 0.1f;
        var newPosition = transform.position + translation;
        transform.position = newPosition;
    }

    private Vector2 GetMouseDelta(InputAction.CallbackContext ctx)
    {
        return ctx.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        Move();
    }

}
