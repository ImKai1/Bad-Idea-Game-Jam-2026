using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

public class MainMenuController : MonoBehaviour
{
    private readonly string _mainScene = "KaisPlayground";
    private void PlayButton()
    {
        SceneManager.LoadScene(_mainScene);
    }

    private void QuitButton()
    {
        Application.Quit();
    }
}
