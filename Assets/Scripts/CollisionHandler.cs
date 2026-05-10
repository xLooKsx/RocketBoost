using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{

    [SerializeField] float levelLoadDelay;

    void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("This is my friend :)");
                break;

            case "Finish":
                StartLeveLoadSequence(nameof(LoadNextLevel));
                break;

            default:
                StartLeveLoadSequence(nameof(ReloadLevel));
                break;
        }
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
        if(nextScene == SceneManager.sceneCountInBuildSettings)
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
}
