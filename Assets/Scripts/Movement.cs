using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{

    [SerializeField] InputAction thrust;
    [SerializeField] InputAction rotation;
    [SerializeField] float rotationForce;
    [SerializeField] float thrustForce;

    Rigidbody myRigidbody;

    void OnEnable()
    {
        thrust.Enable();
        rotation.Enable();
    }

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        ProcessThrust();
        ProcessRotation();
    }

    private void ProcessRotation()
    {
        float currentRotation = rotation.ReadValue<float>();
        if (currentRotation > 0)
        {
            transform.Rotate(Vector3.back * rotationForce * Time.fixedDeltaTime);
        }
        else if (currentRotation < 0)
        {
            transform.Rotate(Vector3.forward * rotationForce * Time.fixedDeltaTime);
        }

    }

    private void ProcessThrust()
    {
        if (thrust.IsPressed())
        {
            myRigidbody.AddRelativeForce(Vector3.up * thrustForce * Time.fixedDeltaTime);
        }
    }
}
