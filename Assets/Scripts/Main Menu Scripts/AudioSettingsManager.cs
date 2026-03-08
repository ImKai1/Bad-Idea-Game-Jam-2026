using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioSettingsManager : MonoBehaviour
{

    // [Header("VOLUME")]
    [SerializeField] private AudioMixer masterMixer;
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Slider voiceSlider;
    private int volumeMultiplier = 20;
    private float masterVolumeDefaultValue = 1;
    private float musicVolumeDefaultValue = 1;
    private float sfxVolumeDefaultValue = 1;
    private float voiceVolumeDefaultValue = 1;

    void Start()
    {
        masterSlider.value = PlayerPrefs.GetFloat("MasterVolume", masterVolumeDefaultValue);
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", musicVolumeDefaultValue);
        sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume", sfxVolumeDefaultValue);
        voiceSlider.value = PlayerPrefs.GetFloat("VoiceVolume", voiceVolumeDefaultValue);
        Debug.Log("Audio Settings Manager Start");
    }

    public void SetMasterVolume()
    {
        Debug.Log("Enter SetMasterV");
        float volume = masterSlider.value;
        float normalizedVolume = Mathf.Log10(Mathf.Clamp(volume, 0.0001f, 1f)) * volumeMultiplier;
        masterMixer.SetFloat("MasterAudio", normalizedVolume);
        PlayerPrefs.SetFloat("MasterVolume", volume);
        PlayerPrefs.Save();
    }
    public void SetMusicVolume()
    {
        Debug.Log("Enter SetMusicV");
        float volume = musicSlider.value;
        float normalizedVolume = Mathf.Log10(Mathf.Clamp(volume, 0.0001f, 1f)) * volumeMultiplier;
        masterMixer.SetFloat("Music", normalizedVolume);
        PlayerPrefs.SetFloat("MusicVolume", volume);
        PlayerPrefs.Save();
    }
    public void SetSFXVolume()
    {
        Debug.Log("Enter SetSFXV");
        float volume = sfxSlider.value;
        float normalizedVolume = Mathf.Log10(Mathf.Clamp(volume, 0.0001f, 1f)) * volumeMultiplier;
        masterMixer.SetFloat("SFX", normalizedVolume);
        PlayerPrefs.SetFloat("SFXVolume", volume);
        PlayerPrefs.Save();
    }
    public void SetVoiceVolume()
    {
        Debug.Log("Enter SetVoiceV");
        float volume = voiceSlider.value;
        float normalizedVolume = Mathf.Log10(Mathf.Clamp(volume, 0.0001f, 1f)) * volumeMultiplier;
        masterMixer.SetFloat("Voice", normalizedVolume);
        PlayerPrefs.SetFloat("VoiceVolume", volume);
        PlayerPrefs.Save();
    }
}
