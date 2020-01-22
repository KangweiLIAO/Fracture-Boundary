using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementControl : MonoBehaviour
{
    // public vars
    public int walkSpeed = 1;
    public int rotateSpeed = 4;
    public int jumpSpeed = 100;
    public float gravity = 9;
    public bool walking;
    public bool running;
    public bool idling;
    public bool jumping;
    public bool backrunning;

    // private vars


    // components
    private Animator animator;
    private CharacterController controller;
    private Vector3 moveDirection;

    // Start is called before the first frame update
    void Start()
    {
        idling = true;
        walking = running = false;

        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    void Movement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        if (controller.isGrounded)
        {
            transform.Rotate(0, horizontalInput * rotateSpeed, 0);
            moveDirection = new Vector3(horizontalInput, 0, verticalInput);
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= walkSpeed;

            // Run
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                walkSpeed = 3;
                setAllMovement(false);
                running = true;
            } else if (Input.GetKeyUp(KeyCode.LeftShift)) {
                walkSpeed = 1;
                running = false;
            }

            // Run back
            if (Input.GetKeyDown(KeyCode.S))
            {
                walkSpeed = 2;
                setAllMovement(false);
                backrunning = true;
            }
            else if (Input.GetKeyUp(KeyCode.S)) backrunning = false;

            // Jump
            if (Input.GetKeyDown(KeyCode.Space))
            {
                moveDirection.y += jumpSpeed;
                setAllMovement(false);
                jumping = true;
            }
            else if (Input.GetKeyUp(KeyCode.Space)) jumping = false;

            // Walking detection
            if ((Mathf.Abs(horizontalInput) >= 0.01 || Mathf.Abs(verticalInput) >= 0.01) && (!running && !backrunning && !jumping))
            {
                setAllMovement(false);
                walking = true;
            }
            if (Mathf.Abs(horizontalInput) <= 0.1 && Mathf.Abs(verticalInput) <= 0.1)
            {
                setAllMovement(false);
                idling = true;
            }

            // Animation controls:
            StopAllAnimation();
            if (idling)
            {
                animator.SetBool("Idle1", true);
            }
            if (walking)
            {
                animator.SetBool("Walk1", true);
            }
            if (running)
            {
                animator.SetBool("Run1", true);
            }
            if (backrunning)
            {
                animator.SetBool("Runback1", true);
            }
            if (jumping)
            {
                animator.SetBool("Jump1", true);
            }
        }
        moveDirection.y -= gravity * Time.deltaTime;     //gravity
        controller.Move(moveDirection * Time.deltaTime);
    }

    void setAllMovement(bool state)
    {
        walking = state;
        running = state;
        idling = state;
        jumping = state;
        backrunning = state;
    }

    void StopAllAnimation()
    {
        animator.SetBool("Walk1", false);
        animator.SetBool("Run1", false);
        animator.SetBool("Runback1", false);
        animator.SetBool("Idle1", false);
        animator.SetBool("Jump1", false);
    }
}
