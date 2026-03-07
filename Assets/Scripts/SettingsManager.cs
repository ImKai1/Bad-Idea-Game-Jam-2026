using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using UnityEngine.PlayerLoop;
using System.Runtime.InteropServices;

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
    [SerializeField] TMP_Dropdown ddTargetFPS;
    [SerializeField] TMP_Dropdown ddResolution;
    // [SerializeField] Toggle FullScreenToggle;
    [SerializeField] TMP_Dropdown ddFullScreenMode;
    List<string> fps = new List<string>() {"10", "24", "30", "60", "120", "144", "240"};
    Resolution[] allResolutions;
    // private bool isFullScreen;
    FullScreenMode[] fullScreenModes = {FullScreenMode.ExclusiveFullScreen, 
                                        FullScreenMode.FullScreenWindow, 
                                        FullScreenMode.MaximizedWindow, 
                                        FullScreenMode.Windowed};
    int selectedFPS;
    int selectedResolution;
    int selectedFSM;
    List<Resolution> selectedResolutionList = new List<Resolution>();
    List<FullScreenMode> selectedFSMList = new List<FullScreenMode>();

    private void Awake()
    {
        FullScreenMode currentMode = Screen.fullScreenMode;
        // isFullScreen = true;
        allResolutions = Screen.resolutions;
        
        ddTargetFPS.AddOptions(fps);
        
        List<string> resolutionStringList = new List<string>();
        string newRes;
        int i = 0, currValRes = 0;
        foreach (Resolution res in allResolutions)
        {
            newRes = res.width.ToString() + " x " + res.height.ToString();
            if (!resolutionStringList.Contains(newRes))
            {
                resolutionStringList.Add(newRes);
                selectedResolutionList.Add(res);
            }

            if (res.width == Screen.currentResolution.width && res.height == Screen.currentResolution.height)
            {
                currValRes = i;
            }
            ++i;
        }
        ddResolution.AddOptions(resolutionStringList);

        int j = 0, curValFSM = 0;
        List<string> fsmStringList = new List<string>();
        foreach(FullScreenMode sm in fullScreenModes)
        {
            fsmStringList.Add(sm.ToString());
            selectedFSMList.Add(sm);

            if (sm == currentMode)
            {
                curValFSM = j;
            }
            ++j;
        }
        ddFullScreenMode.AddOptions(fsmStringList);

        ddTargetFPS.value = PlayerPrefs.GetInt("Target FPS");
        SetFPS();

        masterSlider.value = PlayerPrefs.GetFloat("MasterVolume", masterVolumeDefaultValue);
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", musicVolumeDefaultValue);
        sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume", sfxVolumeDefaultValue);
        voiceSlider.value = PlayerPrefs.GetFloat("VoiceVolume", voiceVolumeDefaultValue);
        SetMasterVolume();
        SetMusicVolume();
        SetSFXVolume();
        SetVoiceVolume();

        ddResolution.value = PlayerPrefs.GetInt("ScreenResolution", currValRes);
        // FullScreenToggle.isOn = PlayerPrefs.GetInt("FullScreen", 1) != 0;
        ddFullScreenMode.value = PlayerPrefs.GetInt("FullScreen", curValFSM);
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
    // public void SetQuality (int qualityIndex) {
    //     QualitySettings.SetQualityLevel(qualityIndex);
    // }

    public void SetFPS () {
        selectedFPS = ddTargetFPS.value;
        Application.targetFrameRate = selectedFPS;
        PlayerPrefs.SetInt("Target FPS", selectedFPS);
        PlayerPrefs.Save();
    }

    public void SetResolution()
    {
        selectedResolution = ddResolution.value;
        Screen.SetResolution(selectedResolutionList[selectedResolution].width, selectedResolutionList[selectedResolution].height, selectedFSMList[selectedFSM]);
        PlayerPrefs.SetInt("ScreenResolution", selectedResolution);
        PlayerPrefs.Save();
    }

    public void SetFullScreen()
    {
        selectedFSM = ddFullScreenMode.value;
        Screen.SetResolution(selectedResolutionList[selectedResolution].width, selectedResolutionList[selectedResolution].height, selectedFSMList[selectedFSM]);
        PlayerPrefs.SetInt("FullScreen", selectedFSM);
        PlayerPrefs.Save();
    }
}
