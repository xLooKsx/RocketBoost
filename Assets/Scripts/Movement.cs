using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{

    [Header("Input data")]
    [SerializeField] InputAction thrust;
    [SerializeField] InputAction rotation;

    [Header("Input force")]
    [SerializeField] float rotationForce;
    [SerializeField] float thrustForce;

    AudioSource audioSource;
    Rigidbody myRigidbody;

    void OnEnable()
    {
        thrust.Enable();
        rotation.Enable();
    }

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
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
            StartRotation(Vector3.back);
        }
        else if (currentRotation < 0)
        {
            StartRotation(Vector3.forward);
        }

    }

    private void StartRotation(Vector3 direction)
    {
        myRigidbody.freezeRotation = true;
        transform.Rotate(rotationForce * Time.fixedDeltaTime * direction);
        myRigidbody.freezeRotation = false;
    }

    private void ProcessThrust()
    {
        if (thrust.IsPressed())
        {
            myRigidbody.AddRelativeForce(Vector3.up * thrustForce * Time.fixedDeltaTime);
            StartThrustSoundFx();
        }
        else
        {
            StopThrustSoundFx();    
        }
        
    }

    private void StartThrustSoundFx()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }
    private void StopThrustSoundFx()
    {

        audioSource.Stop();
    }
}
