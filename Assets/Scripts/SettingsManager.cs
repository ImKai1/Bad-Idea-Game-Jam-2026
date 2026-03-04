using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class SettingsManager : MonoBehaviour
{
    [Header("VOLUME")]
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

    // ============================================
    [Header("RESOLUTION")]
    [SerializeField] TMP_Dropdown ddResolution;
    [SerializeField] Toggle FullScreenToggle;
    Resolution[] allResolutions;
    private bool isFullScreen;
    int selectedResolution;
    List<Resolution> selectedResolutionList = new List<Resolution>();

    private void Awake()
    {

        isFullScreen = true;
        allResolutions = Screen.resolutions;

        List<string> resolutionStringList = new List<string>();
        string newRes;
        foreach (Resolution res in allResolutions)
        {
            newRes = res.width.ToString() + " x " + res.height.ToString();
            if (!resolutionStringList.Contains(newRes))
            {
                resolutionStringList.Add(newRes);
                selectedResolutionList.Add(res);
            }
        }

        ddResolution.AddOptions(resolutionStringList);

        masterSlider.value = PlayerPrefs.GetFloat("MasterVolume", masterVolumeDefaultValue);
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", musicVolumeDefaultValue);
        sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume", sfxVolumeDefaultValue);
        voiceSlider.value = PlayerPrefs.GetFloat("VoiceVolume", voiceVolumeDefaultValue);
        SetMasterVolume();
        SetMusicVolume();
        SetSFXVolume();
        SetVoiceVolume();

        ddResolution.value = PlayerPrefs.GetInt("ScreenResolution");
        FullScreenToggle.isOn = PlayerPrefs.GetInt("FullScreen", 1) != 0;
        SetResolution();
    }

    public void SetMasterVolume()
    {
        float volume = masterSlider.value;
        float normalizedVolume = Mathf.Log10(Mathf.Clamp(volume, 0.0001f, 1f)) * volumeMultiplier;
        masterMixer.SetFloat("MasterAudio", normalizedVolume);
        PlayerPrefs.SetFloat("MasterVolume", volume);
        PlayerPrefs.Save();
    }
    public void SetMusicVolume()
    {
        float volume = musicSlider.value;
        float normalizedVolume = Mathf.Log10(Mathf.Clamp(volume, 0.0001f, 1f)) * volumeMultiplier;
        masterMixer.SetFloat("Music", normalizedVolume);
        PlayerPrefs.SetFloat("MusicVolume", volume);
        PlayerPrefs.Save();
    }
    public void SetSFXVolume()
    {
        float volume = sfxSlider.value;
        float normalizedVolume = Mathf.Log10(Mathf.Clamp(volume, 0.0001f, 1f)) * volumeMultiplier;
        masterMixer.SetFloat("SFX", normalizedVolume);
        PlayerPrefs.SetFloat("SFXVolume", volume);
        PlayerPrefs.Save();
    }
    public void SetVoiceVolume()
    {
        float volume = voiceSlider.value;
        float normalizedVolume = Mathf.Log10(Mathf.Clamp(volume, 0.0001f, 1f)) * volumeMultiplier;
        masterMixer.SetFloat("Voice", normalizedVolume);
        PlayerPrefs.SetFloat("VoiceVolume", volume);
        PlayerPrefs.Save();
    }

    // =================================================
    public void SetResolution()
    {
        selectedResolution = ddResolution.value;
        Screen.SetResolution(selectedResolutionList[selectedResolution].width, selectedResolutionList[selectedResolution].height, isFullScreen);
        PlayerPrefs.SetInt("ScreenResolution", selectedResolution);
        PlayerPrefs.Save();
    }

    public void SetFullScreen()
    {
        isFullScreen = FullScreenToggle.isOn;
        Screen.SetResolution(selectedResolutionList[selectedResolution].width, selectedResolutionList[selectedResolution].height, isFullScreen);
        PlayerPrefs.SetInt("FullScreen", isFullScreen ? 1 : 0);
        PlayerPrefs.Save();
    }
}
