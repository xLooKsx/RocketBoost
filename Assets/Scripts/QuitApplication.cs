using UnityEngine;
using UnityEngine.InputSystem;

public class QuitApplication : MonoBehaviour
{
    void Update()
    {
        if(Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            Debug.Log("ESC key was pressed");
            Application.Quit();
        }
    }
}
