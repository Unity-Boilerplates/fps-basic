using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class PlayerMovementScript : MonoBehaviour
{

    [SerializeField] InputReader inputReader;
    [SerializeField] CharacterController controller;
    [SerializeField] float speed = 12f;
    [SerializeField] float gravity = -9.81f;
    [SerializeField] float jumpHeight = 3f;
    [SerializeField] Transform groundCheck;
    [SerializeField] float groundDistance = 0.4f;
    [SerializeField] LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded;
    Vector2 movementInput;
    bool JumpInput;


    private void OnEnable()
    {
        inputReader.moveEvent += OnUpdatePosition;
        inputReader.fireEvent += OnFire;
        inputReader.jumpEvent += OnJumpInitiated;
        inputReader.jumpCanceledEvent += OnJumpCancelled;

    }

    void Update()
    {
        handleMovement();
        handleGravity();
    }

    void handleMovement()
    { 
        Vector3 move = transform.right * movementInput.x + transform.forward * movementInput.y;
        controller.Move(move * speed * Time.deltaTime);

        
    }

    void handleGravity()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded && velocity.y < 0) velocity.y = -2f;

        if (this.JumpInput && isGrounded) velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    void OnJumpInitiated()
    {
        // TODO: Make jump height proportional to jump input time
        this.JumpInput = true;
    }

    void OnJumpCancelled()
    {
        // TODO: Make jump height proportional to jump input time
        this.JumpInput = false;
    }

    void OnFire()
    {
        this.Log("Firing!");
    }

    void OnUpdatePosition(Vector2 movement)
    {
        movementInput = movement;
    }
}
