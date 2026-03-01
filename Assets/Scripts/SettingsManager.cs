using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class SettingsManager : MonoBehaviour
{
    [Header("VOLUME")]
    [SerializeField] private AudioMixer masterMixer;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;
    private int volumeMultiplier = 20;
    private float musicVolumeDefaultValue = 1;
    private float sfxVolumeDefaultValue = 1;

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
    }

    private void Start()
    {
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", musicVolumeDefaultValue);
        sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume", sfxVolumeDefaultValue);
        SetMusicVolume();
        SetSFXVolume();

        ddResolution.value = PlayerPrefs.GetInt("ScreenResolution");
        FullScreenToggle.isOn = PlayerPrefs.GetInt("FullScreen", 1) != 0;
        SetResolution();
    }

    public void SetMusicVolume()
    {
        float volume = musicSlider.value;
        masterMixer.SetFloat("Music", Mathf.Log10(volume) * volumeMultiplier);
        PlayerPrefs.SetFloat("MusicVolume", volume);
    }
    public void SetSFXVolume()
    {
        float volume = sfxSlider.value;
        masterMixer.SetFloat("SFX", Mathf.Log10(volume) * volumeMultiplier);
        PlayerPrefs.SetFloat("SFXVolume", volume);
    }

    public void SetResolution()
    {
        selectedResolution = ddResolution.value;
        Screen.SetResolution(selectedResolutionList[selectedResolution].width, selectedResolutionList[selectedResolution].height, isFullScreen);
        PlayerPrefs.SetInt("ScreenResolution", selectedResolution);
    }

    public void SetFullScreen()
    {
        isFullScreen = FullScreenToggle.isOn;
        Screen.SetResolution(selectedResolutionList[selectedResolution].width, selectedResolutionList[selectedResolution].height, isFullScreen);
        PlayerPrefs.SetInt("FullScreen", isFullScreen ? 1 : 0);
    }
}
