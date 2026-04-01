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
        GameEvents.Subscribe(GameEventKeys.GameplayLoaded, OngameplayLoad);
        GameEvents.Subscribe(GameEventKeys.CursorClick, OnCursorClickSFX);
        GameEvents.Subscribe(GameEventKeys.PickUpGeneral, OnPickUpGeneralSFX);
        GameEvents.Subscribe(GameEventKeys.Interact, OnInteractSFX);
        GameEvents.Subscribe(GameEventKeys.CauldronStart, OnCauldronStart);
        GameEvents.Subscribe(GameEventKeys.BottlePickUp, OnBottlePickUpSFX);
        GameEvents.Subscribe(GameEventKeys.BottlePutDown, OnBottlePutDownSFX);
    }

    private void OnDisable()
    {
        GameEvents.Unsubscribe(GameEventKeys.GameplayLoaded, OngameplayLoad);
        GameEvents.Unsubscribe(GameEventKeys.CursorClick, OnCursorClickSFX);
        GameEvents.Unsubscribe(GameEventKeys.PickUpGeneral, OnPickUpGeneralSFX);
        GameEvents.Unsubscribe(GameEventKeys.Interact, OnInteractSFX);
        GameEvents.Unsubscribe(GameEventKeys.CauldronStart, OnCauldronStart);
        GameEvents.Unsubscribe(GameEventKeys.BottlePickUp, OnBottlePickUpSFX);
        GameEvents.Unsubscribe(GameEventKeys.BottlePutDown, OnBottlePutDownSFX);
    }

    // ============================================================================================== //

    public void PlayMainMenuMusic() => playback.PlayMusic(audioLibrary_SO.musicUI.MainMenuBGM);
    public void OngameplayLoad() => playback.PlayMusic(audioLibrary_SO.musicUI.GameplayBGM);
    

    // ============================================================================================== //

    public void OnCursorClickSFX() => playback.PlaySFXRandomizeList(audioLibrary_SO.uiSFX.cursorClickSFX);
    public void OnPickUpGeneralSFX() => playback.PlaySFXRandomizeList(audioLibrary_SO.inGameSFX.pickup);
    public void OnInteractSFX() => playback.PlaySFXAutoRandomize(audioLibrary_SO.inGameSFX.generalInteractSFX, 0.1f, 0.1f);
    public void OnCauldronStart() => playback.PlayAmbience(audioLibrary_SO.inGameAmbience.cauldronConstantBoiling);
    public void OnBottlePickUpSFX() => playback.PlaySFXAutoRandomize(audioLibrary_SO.inGameSFX.bottlePickUp, 0.05f, 0.05f);
    public void OnBottlePutDownSFX() => playback.PlaySFXAutoRandomize(audioLibrary_SO.inGameSFX.bottlePutDown, 0.05f, 0.05f);
}
