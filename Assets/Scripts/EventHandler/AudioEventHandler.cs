using UnityEngine;

public class AudioEventHandler : MonoBehaviour
{
    public static AudioEventHandler Instance { get; private set; }

    [SerializeField] private AudioManager audioManager;

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
        audioManager.PlayMusic(audioLibrary_SO.musicUI.MainMenuBGM);
    }

    public float MasterVolume => audioManager.MasterVolume;
    public float MusicVolume => audioManager.MusicVolume;
    public float SFXVolume => audioManager.SFXVolume;
    public float VoiceVolume => audioManager.VoiceVolume;

    public void SetMasterVolume(float value) => audioManager.SetMasterVolume(value);
    public void SetMusicVolume(float value) => audioManager.SetMusicVolume(value);
    public void SetSFXVolume(float value) => audioManager.SetSFXVolume(value);
    public void SetVoiceVolume(float value) => audioManager.SetVoiceVolume(value);

    public void PlayCursorClick()
    {
        audioManager.PlaySFXRandomizeList(audioLibrary_SO.uiSFX.cursorClickSFX);
    }
}
