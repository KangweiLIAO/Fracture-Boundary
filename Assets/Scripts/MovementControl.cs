using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementControl : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 position;
    public  float gravity;

    // Start is called before the first frame update
    void Start()
    {
        gravity = 9;
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
            position = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        }
        position += Vector3.down * gravity * Time.deltaTime;
        controller.Move(position * Time.deltaTime);
    }
}
