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
    [SerializeField] float jumpForce = 1.5f;
    [SerializeField] Transform groundCheck;
    [SerializeField] float groundDistance = 0.4f;
    [SerializeField] LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded;
    Vector2 movementInput;
    bool jumping;
    bool crouching;
    float currentHeight;


    private void OnEnable()
    {
        this.inputReader.moveEvent += OnUpdatePosition;
        this.inputReader.fireEvent += OnFire;
        this.inputReader.jumpEvent += OnJumpInitiated;
        this.inputReader.jumpCanceledEvent += OnJumpCancelled;
        this.inputReader.crouchEvent += OnCrouch;
        this.inputReader.crouchEventCancelled += OnCrouchCancelled;

    }

     void Start()
    {
        this.currentHeight = 0f;
    }

    void Update()
    {
        this.movementHandler();
        this.gravityHandler();
    }

    void movementHandler()
    { 
        Vector3 move = transform.right * this.movementInput.x + transform.forward * this.movementInput.y;
        this.controller.Move(move * this.speed * Time.deltaTime);

        
    }

    void gravityHandler()
    {
        performGroundedActions();
        if (this.jumping && this.currentHeight < this.jumpHeight) this.performJump();
        else this.cancelJump();
        this.controller.Move(velocity * Time.deltaTime);

    }

    void cancelJump()
    {
        if (!this.checkIfGrounded() && this.currentHeight <= this.jumpHeight)
            this.currentHeight = this.jumpHeight;
        this.velocity.y += this.gravity * Time.deltaTime;
    }

    void performJump()
    {
        this.currentHeight += this.jumpForce * Time.deltaTime;
        this.velocity.y = Mathf.Sqrt(this.currentHeight * -2f * this.gravity);
    }

    void performGroundedActions()
    {
        if (this.checkIfGrounded() && !this.jumping) this.currentHeight = 0f;
        if (this.checkIfGrounded() && this.velocity.y < 0) this.velocity.y = -2f;
    }

    bool checkIfGrounded()
    {
        return Physics.CheckSphere(this.groundCheck.position, this.groundDistance, this.groundMask);
    }

    void OnJumpInitiated()
    {
        this.jumping = true;
    }

    void OnJumpCancelled()
    {
        this.jumping = false;
    }

    void OnCrouch()
    {
        this.crouching = true;
        this.transform.localScale = new Vector3(this.transform.localScale.x, this.transform.localScale.y / 2, this.transform.localScale.z);
    }

    void OnCrouchCancelled()
    {
        this.crouching = false;
        this.transform.localScale = new Vector3(this.transform.localScale.x, this.transform.localScale.y * 2, this.transform.localScale.z);

    }
    void OnFire()
    {
        this.Log("Firing!");
    }

    void OnUpdatePosition(Vector2 movement)
    {
        this.movementInput = movement;
    }
}
