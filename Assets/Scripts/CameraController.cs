using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float sensativity = 5f;

    public CinemachineComposer composer;
    
    // Start is called before the first frame update
    void Start()
    {
        composer = GetComponent<Cinemachine.CinemachineVirtualCamera>()
            .GetCinemachineComponent<Cinemachine.CinemachineComposer>();
    }

    // Update is called once per frame
    void Update()
    {
        float vertical = Input.GetAxis("Mouse Y") * sensativity * Time.deltaTime;
        composer.m_TrackedObjectOffset.y += vertical;
        composer.m_TrackedObjectOffset.y = Mathf.Clamp(composer.m_TrackedObjectOffset.y, 1.8f, 5);
    }
}
