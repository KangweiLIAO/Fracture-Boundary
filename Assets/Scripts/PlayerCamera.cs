using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    // public vars
    public float rotateSpeed = 3f;
    public bool lockCursor = true;

    // private vars
    private Vector3 offset;

    // Start is called before the first frame update
    void Awake()
    {
        Cursor.lockState = lockCursor ? CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible = !lockCursor;
    }

    // Update is called once per frame
    void Update()
    {
        offset += new Vector3(0, Input.GetAxis("Mouse X"), 0) * rotateSpeed;
        transform.eulerAngles = offset;
        if (lockCursor && Input.GetMouseButtonUp(0))
        {
            Cursor.lockState = lockCursor ? CursorLockMode.Locked : CursorLockMode.None;
            Cursor.visible = !lockCursor;
        }
    }
}
