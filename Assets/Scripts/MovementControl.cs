using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementControl : MonoBehaviour
{
    public int speed = 1;
    public int rotateSpeed = 3;

    private Animator animator;
    private CharacterController controller;
    private Vector3 moveDirection;
    private  float gravity;
    private bool walking;
    private bool running;

    private int directionState;
    private int lastDirectionState;

    // Start is called before the first frame update
    void Start()
    {
        gravity = 9;
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
        if (controller.isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                if (running) animator.SetTrigger("Run01");
                else
                {
                    animator.SetTrigger("Walk01");
                    walking = true;
                }
            } else if (Input.GetKeyUp(KeyCode.W)) {
                if (!running) animator.SetTrigger("Idle01");
                walking = false;
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                speed = 2;
                walking = false;
                animator.SetTrigger("RunBack01");
            } else if (Input.GetKeyUp(KeyCode.S)) {
                speed =  1;
                if (!running) animator.SetTrigger("Idle01");
            }

            if (Input.GetKeyDown(KeyCode.A))
            {
                if (running) animator.SetTrigger("Run01");
                else
                {
                    animator.SetTrigger("Walk01");
                    walking = true;
                }
            } else if (Input.GetKeyUp(KeyCode.A)) {
                if (!running) animator.SetTrigger("Idle01");
                walking = false;
            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                if (running) animator.SetTrigger("Run01");
                else
                {
                    animator.SetTrigger("Walk01");
                    walking = true;
                }
            } else if (Input.GetKeyUp(KeyCode.D)) {
                if (!running) animator.SetTrigger("Idle01");
                walking = false;
            }

            if (Input.GetKeyDown(KeyCode.LeftShift) && walking) 
            {
                if (!Input.GetKeyDown(KeyCode.S))
                {
                    speed = 3;
                    running = true;
                    walking = false;
                    animator.SetTrigger("Run01");
                }
            }else if (Input.GetKeyUp(KeyCode.LeftShift)) {
                if (!Input.GetKeyDown(KeyCode.S))
                {
                    speed = 1;
                    running = false;
                    animator.SetTrigger("Idle01");
                }
            }
        }
        moveDirection = new Vector3(Input.GetAxis("Horizontal") * speed, 0, Input.GetAxis("Vertical") * speed);
        moveDirection = transform.TransformDirection(moveDirection);
        controller.Move(moveDirection * Time.deltaTime);
        transform.Rotate(0, Input.GetAxis("Horizontal") * rotateSpeed, 0);
    }
}
