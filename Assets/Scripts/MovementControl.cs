using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementControl : MonoBehaviour
{
    // public vars
    public float gravity;
    public float currentSpeed;
    public float walkSpeed;
    public float runSpeed;
    public float jumpSpeed;

    public bool idling;
    public bool walking;
    public bool running;
    public bool runningBack;

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
        MovementDetection();
    }

    void MovementDetection()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        if (controller.isGrounded)
        {
            moveDirection = new Vector3(horizontalInput, 0, verticalInput);
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= currentSpeed;
            // Walk/Idle detection
            if ((Mathf.Abs(horizontalInput) >= 0.01 || Mathf.Abs(verticalInput) >= 0.01) && (!running && !runningBack))
            {
                SetMovement("walk");
            }
            if (Mathf.Abs(horizontalInput) <= 0.01 && Mathf.Abs(verticalInput) <= 0.01)
            {
                SetMovement("idle");
            }

            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                SetMovement("run");
            }
            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                running = false;
                animator.SetBool("Run1", false);
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                SetMovement("runback");
            }
            if (Input.GetKeyUp(KeyCode.S))
            {
                runningBack = false;
                animator.SetBool("Runback1", false);
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                moveDirection.y += jumpSpeed;
                SetAnimation("jump");
            }

            if (idling) SetAnimation("idle");

            if (walking)
            {
                SetAnimation("walk");
                currentSpeed = walkSpeed;
            }
            if (running)
            {
                SetAnimation("run");
                currentSpeed = runSpeed;
            }
            if (runningBack)
            {
                SetAnimation("runback");
                currentSpeed = walkSpeed + 1;
            }
        }

        // Moving
        moveDirection.y -= gravity * Time.deltaTime;        // Gravity
        controller.Move(moveDirection * Time.deltaTime);
    }

    void SetMovement(string state)
    {
        switch (state){
            case "idle":
                idling = true;
                walking = false;
                running = false;
                runningBack = false;
                break;
            case "walk":
                idling = false;
                walking = true;
                running = false;
                runningBack = false;
                break;
            case "run":
                idling = false;
                walking = false;
                running = true;
                runningBack = false;
                break;
            case "runback":
                idling = false;
                walking = false;
                running = false;
                runningBack = true;
                break;
        }
    }

    void SetAnimation(string movement)
    {
        switch (movement)
        {
            case "idle":
                animator.SetBool("Walk1", false);
                animator.SetBool("Run1", false);
                animator.SetBool("Runback1", false);
                animator.SetBool("Idle1", true);
                break;
            case "walk":
                animator.SetBool("Walk1", true);
                animator.SetBool("Run1", false);
                animator.SetBool("Runback1", false);
                animator.SetBool("Idle1", false);
                break;
            case "run":
                animator.SetBool("Walk1", false);
                animator.SetBool("Run1", true);
                animator.SetBool("Runback1", false);
                animator.SetBool("Idle1", false);
                break;
            case "runback":
                animator.SetBool("Walk1", false);
                animator.SetBool("Run1", false);
                animator.SetBool("Runback1", true);
                animator.SetBool("Idle1", false);
                break;
            case "jump":
                animator.SetBool("Walk1", false);
                animator.SetBool("Run1", false);
                animator.SetBool("Runback1", false);
                animator.SetBool("Idle1", false);
                animator.Play("BasicMotions@Jump01", 0, 0);
                break;
        }
    }
}
