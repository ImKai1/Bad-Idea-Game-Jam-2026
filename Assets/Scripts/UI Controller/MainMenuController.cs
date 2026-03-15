using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

public class MainMenuController : MonoBehaviour
{
    private void PlayButton()
    {
        SceneManager.LoadScene("KaisPlayground");
    }

    private void QuitButton()
    {
        Application.Quit();
    }
}
