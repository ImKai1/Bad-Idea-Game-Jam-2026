using UnityEngine;
using UnityEngine.UI;

public class AudioSettingsController : MonoBehaviour
{
    // [SerializeField] private AudioManager audioManager;
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Slider voiceSlider;

    private bool _isInitialized = false;

    private void Start()
    {
        Debug.Log("AuSetCon");
        masterSlider.onValueChanged.AddListener(AudioEventHandler.Instance.SetMasterVolume);
        musicSlider.onValueChanged.AddListener(AudioEventHandler.Instance.SetMusicVolume);
        sfxSlider.onValueChanged.AddListener(AudioEventHandler.Instance.SetSFXVolume);
        voiceSlider.onValueChanged.AddListener(AudioEventHandler.Instance.SetVoiceVolume);

        SyncUI();
        _isInitialized = true;
    }

    private void OnEnable()
    {
        if (!_isInitialized) return;
        SyncUI();
    }

    private void SyncUI()
    {
        Debug.Log("SyncUI - Master: " + AudioEventHandler.Instance.MasterVolume 
        + " Music: " + AudioEventHandler.Instance.MusicVolume
        + " SFX: " + AudioEventHandler.Instance.SFXVolume
        + " Voice: " + AudioEventHandler.Instance.VoiceVolume);

        masterSlider.SetValueWithoutNotify(AudioEventHandler.Instance.MasterVolume);
        musicSlider.SetValueWithoutNotify(AudioEventHandler.Instance.MusicVolume);
        sfxSlider.SetValueWithoutNotify(AudioEventHandler.Instance.SFXVolume);
        voiceSlider.SetValueWithoutNotify(AudioEventHandler.Instance.VoiceVolume);
    }
}

