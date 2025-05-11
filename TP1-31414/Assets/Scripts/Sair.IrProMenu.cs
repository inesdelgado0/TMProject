using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class SairAplicacao : MonoBehaviour
{
    void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            SceneManager.LoadScene("menu");
        }
    }
}