using UnityEngine;
using UnityEngine.UI;

public class AudioSettingsController : MonoBehaviour
{
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Slider voiceSlider;

    private bool _isInitialized = false;

    private void Start()
    {
        Debug.Log("AuSetCon");
        masterSlider.onValueChanged.AddListener(AudioSettingsManager.Instance.SetMasterVolume);
        musicSlider.onValueChanged.AddListener(AudioSettingsManager.Instance.SetMusicVolume);
        sfxSlider.onValueChanged.AddListener(AudioSettingsManager.Instance.SetSFXVolume);
        voiceSlider.onValueChanged.AddListener(AudioSettingsManager.Instance.SetVoiceVolume);

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
        masterSlider.SetValueWithoutNotify(AudioSettingsManager.Instance.MasterVolume);
        musicSlider.SetValueWithoutNotify(AudioSettingsManager.Instance.MusicVolume);
        sfxSlider.SetValueWithoutNotify(AudioSettingsManager.Instance.SFXVolume);
        voiceSlider.SetValueWithoutNotify(AudioSettingsManager.Instance.VoiceVolume);
    }
}

