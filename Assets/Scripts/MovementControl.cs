using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementControl : MonoBehaviour
{
    public int speed = 1;

    private Animator animator;
    private CharacterController controller;
    private Vector3 position;
    private  float gravity;

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
        Walk();
    }

    void Walk()
    {
        if (controller.isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                animator.SetTrigger("Walk01");
            }
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                speed = 3;
                animator.SetTrigger("Run01");
            }
            if (Input.GetKeyUp(KeyCode.W))
            {
                speed = 1;
                animator.SetTrigger("Idle01");
            }
                position = new Vector3(Input.GetAxis("Horizontal") * speed, 0, Input.GetAxis("Vertical") * speed);
        }
        position += Vector3.down * gravity * Time.deltaTime;
        controller.Move(position * Time.deltaTime);
    }
}
