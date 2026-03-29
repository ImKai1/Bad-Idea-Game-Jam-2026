using UnityEngine;

public class AudioEventHandler : MonoBehaviour
{
    public static AudioEventHandler Instance { get; private set; }

    [SerializeField] private AudioPlaybackManager playback;

    [SerializeField] private AudioLibrary_SO audioLibrary_SO;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        PlayMainMenuMusic();
    }

    private void OnEnable()
    {
        GameEvents.Subscribe(GameEventKeys.Interact, OnInteractSFX);
        GameEvents.Subscribe(GameEventKeys.CursorClick, OnCursorClickSFX);
    }

    private void OnDisable()
    {
        GameEvents.Unsubscribe(GameEventKeys.Interact, OnInteractSFX);
        GameEvents.Unsubscribe(GameEventKeys.CursorClick, OnCursorClickSFX);
    }

    // ============================================================================================== //

    public void PlayMainMenuMusic() => playback.PlayMusic(audioLibrary_SO.musicUI.MainMenuBGM);
    

    // ============================================================================================== //

    public void OnCursorClickSFX() => playback.PlaySFXRandomizeList(audioLibrary_SO.uiSFX.cursorClickSFX);

    public void OnInteractSFX() => playback.PlaySFXAutoRandomize(audioLibrary_SO.inGameSFX.generalInteractSFX, 0.1f, 0.1f);
}
