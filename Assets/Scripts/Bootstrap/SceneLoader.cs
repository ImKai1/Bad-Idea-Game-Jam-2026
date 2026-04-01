using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneLoader
{

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void EnsureBootstrapLoaded()
    {
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            if (SceneManager.GetSceneAt(i).name == SceneNameKeys.BootstrapScene)
                return;
        }

        SceneManager.LoadScene(SceneNameKeys.BootstrapScene, LoadSceneMode.Additive);
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    private static void EnsureMainMenuLoadedIfBootstrapIsActive()
    {
        if (SceneManager.GetActiveScene().name == SceneNameKeys.BootstrapScene)
        {
            SceneManager.LoadScene(SceneNameKeys.MainMenuScene, LoadSceneMode.Additive);
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(SceneNameKeys.MainMenuScene));
        }
    }

    public static void LoadMainMenu()
    {
        SceneManager.LoadScene(SceneNameKeys.MainMenuScene, LoadSceneMode.Single);
    }

    public static void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }
}