using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController2 : MonoBehaviour
{
    //Public variables
    public float movementSpeed = 5f;
    public float backwardMovementSpeed = 3f;
    public float gravity = -9.81f;
    public float mouseSensativity = 15f;
    public float runningPeed = 10f;
    public float jumpHeight = 3f;
    public float turnSpeed = 150f;

    //Components
    private Animator animator;
    private CharacterController controller;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        controller = GetComponent<CharacterController>();

        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    void Movement()
    {
        float horizontalInput = Input.GetAxis("Mouse X") * mouseSensativity * Time.deltaTime;
        float verticalInput = Input.GetAxis("Vertical");

           //Animates character based on the speed he's going
        animator.SetFloat("Speed", verticalInput);

        transform.Rotate(Vector3.up, horizontalInput * turnSpeed * Time.deltaTime);

        if(verticalInput != 0)
        {
            float moveSpeedToUse = verticalInput > 0 ? movementSpeed : backwardMovementSpeed;
            controller.SimpleMove(transform.forward * moveSpeedToUse * verticalInput);
        }
                       
    }
}
