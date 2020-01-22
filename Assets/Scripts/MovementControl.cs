using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementControl : MonoBehaviour
{
    // public vars
    public float walkSpeed;
    public float runSpeed;
    public float currentSpeed;
    public float jumpSpeed;
    public float rotateSpeed;
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
        currentSpeed = walkSpeed;
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Movement();
    }

    void Movement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        if (controller.isGrounded)
        {
            // transform.Rotate(horizontalInput * rotateSpeed, 0, verticalInput * rotateSpeed);
            moveDirection = new Vector3(horizontalInput , 0, verticalInput);
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= currentSpeed;

            // Run
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                currentSpeed = runSpeed;
                setAllMovement(false);
                running = true;
            }

            // Run back
            if (Input.GetKeyDown(KeyCode.S))
            {
                currentSpeed = walkSpeed+1;
                setAllMovement(false);
                backrunning = true;
            }

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
            if ((Mathf.Abs(horizontalInput) <= 0.01 && Mathf.Abs(verticalInput) <= 0.01) && (!backrunning && !jumping))
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

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            currentSpeed = walkSpeed;
            running = false;
        }
        if (Input.GetKeyUp(KeyCode.S)) backrunning = false;
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
