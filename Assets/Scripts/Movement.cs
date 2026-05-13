using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{

    [Header("Input data")]
    [SerializeField] InputAction thrustInput;
    [SerializeField] InputAction rotationInput;

    [Header("Input force")]
    [SerializeField] float rotationForce;
    [SerializeField] float thrustForce;

    [Header("Audio vfx")]
    [SerializeField] AudioClip thrustEngine;

    [Header("Particles")]
    [SerializeField] ParticleSystem thrustParticle;
    [SerializeField] ParticleSystem LeftrotationParticle;
    [SerializeField] ParticleSystem RightrotationParticle;

    AudioSource audioSource;
    Rigidbody myRigidbody;

    void OnEnable()
    {
        thrustInput.Enable();
        rotationInput.Enable();
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
        float currentRotation = rotationInput.ReadValue<float>();
        if (currentRotation > 0)
        {
            RightrotationParticle.Stop();
            StartRotation(Vector3.back, LeftrotationParticle);
        }
        else if (currentRotation < 0)
        {
            LeftrotationParticle.Stop();
            StartRotation(Vector3.forward, RightrotationParticle);
        }
        else
        {
            LeftrotationParticle.Stop();
            RightrotationParticle.Stop();
        }

    }

    private void StartRotation(Vector3 direction, ParticleSystem particle)
    {
        particle.Play();
        myRigidbody.freezeRotation = true;
        transform.Rotate(rotationForce * Time.fixedDeltaTime * direction);
        myRigidbody.freezeRotation = false;
        
    }

    private void ProcessThrust()
    {
        if (thrustInput.IsPressed())
        {
            myRigidbody.AddRelativeForce(Vector3.up * thrustForce * Time.fixedDeltaTime);
            StartSoundFx(thrustEngine);
            thrustParticle.Play();
        }
        else
        {
            StopSoundFx();    
            thrustParticle.Stop();
        }
        
    }

    private void StartSoundFx(AudioClip audioClip)
    {
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(audioClip);
        }
    }
    private void StopSoundFx()
    {

        audioSource.Stop();
    }
}
