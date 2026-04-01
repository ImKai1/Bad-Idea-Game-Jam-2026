using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;
using Unity.VisualScripting;

public class MainMenuController : MonoBehaviour
{
    private void PlayButton()
    {
        SceneManager.LoadScene(SceneNameKeys.GameplayScene);
        GameEvents.Fire(GameEventKeys.GameplayLoaded);
    }

    private void QuitButton()
    {
        Application.Quit();
    }
}
