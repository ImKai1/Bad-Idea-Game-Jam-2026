using UnityEngine;
using UnityEngine.InputSystem;

public class TestingScript : MonoBehaviour
{
    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void Update()
    {
        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            Debug.Log("E Pressed");
            // audioManager.PlaySFX(audioManager.pouringSFX);
        }

        if (Keyboard.current.fKey.wasPressedThisFrame)
        {
            Debug.Log("F Pressed");
            // audioManager.PlayVoice(audioManager.voice1);
        }
    }
}
