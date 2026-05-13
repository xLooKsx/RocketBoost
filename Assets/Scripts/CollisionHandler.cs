using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{

    [SerializeField] float levelLoadDelay;

    [Header("Audio sfx")]
    [SerializeField] AudioClip shipExplosionSFX;
    [SerializeField] AudioClip landingPadSFX;

    [Header("Particles")]
    [SerializeField] ParticleSystem shipExplosionParticles;
    [SerializeField] ParticleSystem landingPadParticles;

    bool isPlayerOnControl;
    [SerializeField] bool isOnGodMode;

    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        isPlayerOnControl = true;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!isPlayerOnControl || isOnGodMode) { return; }

        switch (collision.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("This is my friend :)");
                break;

            case "Finish":
                HandlePlayerCollision(landingPadSFX, nameof(LoadNextLevel), landingPadParticles);                
                break;

            default:                
                HandlePlayerCollision(shipExplosionSFX, nameof(ReloadLevel), shipExplosionParticles);                                
                break;
        }
    }

    void Update()
    {
        RespondToDebugKeys();
    }

    private void RespondToDebugKeys()
    {
        if (Keyboard.current.lKey.wasPressedThisFrame)
        {
            LoadNextLevel();
        }else if (Keyboard.current.cKey.wasPressedThisFrame)
        {
            isOnGodMode = !isOnGodMode;
            Debug.Log("GOD mode on");
        }
    }

    private void HandlePlayerCollision(AudioClip clipToPlay, string methodToInvoke, ParticleSystem particles)
    {
        isPlayerOnControl = false;
        StartSoundSfx(clipToPlay);
        particles.Play();
        StartLeveLoadSequence(methodToInvoke);
    }

    private void StartLeveLoadSequence(String methodToInvoke)
    {
        GetComponent<Movement>().enabled = false;
        Invoke(methodToInvoke, levelLoadDelay);
    }

    private void LoadNextLevel()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        int nextScene = ++currentScene;
        if (nextScene == SceneManager.sceneCountInBuildSettings)
        {
            nextScene = 0;
        }
        SceneManager.LoadScene(nextScene);
    }

    private void ReloadLevel()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
    }

    private void StartSoundSfx(AudioClip audioClip)
    {
        StopSoundFx();
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(audioClip);
        }
    }

    private void StopSoundFx()
    {

        audioSource.Stop();
    }

    void OnDestroy()
    {
        StopSoundFx();
    }
}
