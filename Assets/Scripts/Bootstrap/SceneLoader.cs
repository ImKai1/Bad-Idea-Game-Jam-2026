using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneLoader
{
    private const string BOOTSTRAP_SCENE = "Bootstrap";
    private const string MAIN_MENU_SCENE = "Little-Bunny-Playground";

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void EnsureBootstrapLoaded()
    {
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            if (SceneManager.GetSceneAt(i).name == BOOTSTRAP_SCENE)
                return;
        }

        SceneManager.LoadScene(BOOTSTRAP_SCENE, LoadSceneMode.Additive);
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    private static void EnsureMainMenuLoadedIfBootstrapIsActive()
    {
        if (SceneManager.GetActiveScene().name == BOOTSTRAP_SCENE)
        {
            SceneManager.LoadScene(MAIN_MENU_SCENE, LoadSceneMode.Additive);
            SceneManager.SetActiveScene(
                SceneManager.GetSceneByName(MAIN_MENU_SCENE));
        }
    }

    public static void LoadMainMenu()
    {
        SceneManager.LoadScene(MAIN_MENU_SCENE, LoadSceneMode.Single);
    }

    public static void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }
}