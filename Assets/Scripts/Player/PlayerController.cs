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

    private Camera mainCamera;




    private void Awake()
    {
        //Get Components
        playerInputActions = new PlayerInputActions();
        playerRigidBody = GetComponent<Rigidbody2D>();
        playerStats = GetComponent<PlayerStats>();
        mainCamera = Camera.main;

        //Events
        playerInputActions.Player.Move.performed += ctx => MovePlayer(ctx.ReadValue<Vector2>());
        playerInputActions.Player.Move.canceled += ctx => StopMovingPlayer();
        playerInputActions.Player.Look.performed += ctx => LookRotation(ctx.ReadValue<Vector2>());
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
        playerStats.Speed = 5;
        playerStats.Lives = 3;
        playerStats.Score = 0;
    }

    private void FixedUpdate()
    {
        playerRigidBody.velocity = playerMovementInput * playerStats.Speed;
    }

    private void MovePlayer(Vector2 direction)
    {
        playerMovementInput = direction;
    }

    private void StopMovingPlayer()
    {
        playerMovementInput = Vector2.zero;
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
