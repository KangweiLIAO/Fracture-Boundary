using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementControl : MonoBehaviour
{
    // public vars
    public float walkSpeed;
    public float rotateSpeed;
    public float jumpSpeed;
    public float gravity;
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
            }
            if (Input.GetKeyUp(KeyCode.LeftShift)) {
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
            if (Input.GetKeyUp(KeyCode.S)) backrunning = false;

            // Jump
            if (Input.GetKeyDown(KeyCode.Space))
            {
                moveDirection.y += jumpSpeed;
                if (running)
                {
                    setAllMovement(false);
                    running = true;
                }else setAllMovement(false);
                jumping = true;
            }
            
            // Walking detection
            if ((Mathf.Abs(horizontalInput) >= 0.01 || Mathf.Abs(verticalInput) >= 0.01) && (!running && !backrunning && !jumping))
            {
                setAllMovement(false);
                walking = true;
            }
            if (Mathf.Abs(horizontalInput) <= 0.01 && Mathf.Abs(verticalInput) <= 0.01 && (!backrunning && !jumping))
            {
                setAllMovement(false);
                idling = true;
            }

            // Animation controls:
            StopAllAnimation();
            if (idling) animator.SetBool("Idle1", true);
            else if (walking) animator.SetBool("Walk1", true);
            else if (running) animator.SetBool("Run1", true);
            else if (backrunning) animator.SetBool("Runback1", true);
            if (jumping)
            {
                StopAllAnimation();
                animator.SetBool("Jump1", true);
                jumping = false;
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
