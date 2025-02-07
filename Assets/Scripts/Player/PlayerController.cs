/*
    File: PlayerController.cs
    Developer: Emanuel Juracic
    First Version: February 06, 2025
    Description:
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    //properties

    private PlayerStats playerStats;
    private Vector2 playerMovementInput;
    private Rigidbody2D playerRigidBody;
    private PlayerInputActions playerInputActions;

    private float moveInput;

    private Camera mainCamera;




    private void Awake()
    {
        //Get Components
        playerInputActions = new PlayerInputActions();
        playerRigidBody = GetComponent<Rigidbody2D>();
        playerStats = GetComponent<PlayerStats>();
        mainCamera = Camera.main;

        //Events
        playerInputActions.Player.Move.performed += MovePlayer;
        playerInputActions.Player.Move.canceled += _ => StopMovingPlayer();
        playerInputActions.Player.Look.performed += ctx => LookRotation(ctx.ReadValue<Vector2>());
        moveInput = 0;
    }

    private void OnEnable()
    {
        playerInputActions.Enable();
    }

    private void OnDisable()
    {
        playerInputActions.Disable();
    }

    private void Start()
    {
        playerStats.Health = 100;
        playerStats.Shield = 100;
        playerStats.Speed = 0;
        playerStats.MaxSpeed = 5f;
        playerStats.Lives = 3;
        playerStats.Score = 0;
    }
    private void FixedUpdate()
    {
        if (moveInput > 0) // Holding W - Accelerate
        {
            playerStats.Speed = Mathf.Min(playerStats.Speed + Time.fixedDeltaTime * 10, playerStats.MaxSpeed);
        }
        else if (moveInput < 0) // Holding S - Slow down (but no reverse)
        {
            playerStats.Speed = Mathf.Max(playerStats.Speed - Time.fixedDeltaTime * 10, 0f);
        }

        playerRigidBody.velocity = transform.up * playerStats.Speed; // Move forward at current speed
    }
    private void MovePlayer(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<float>(); // Store W (1) or S (-1)
    }

    private void StopMovingPlayer()
    {
        // Slowly decelerate when no input is given
        playerStats.Speed = Mathf.Max(playerStats.Speed - Time.deltaTime * 5, 0f);
    }


    private void LookRotation(Vector2 MousePointer)
    {
        Vector3 mousePosition = Mouse.current.position.ReadValue();
        mousePosition = mainCamera.ScreenToWorldPoint(mousePosition);
        mousePosition.z = 0;

        Vector2 lookDirection = (mousePosition - transform.position).normalized;

        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90f;

        transform.rotation = Quaternion.Euler(0, 0, angle);

    }
}
