using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioSettingsManager : MonoBehaviour
{
    public static AudioSettingsManager Instance { get; private set; }

    [Header("Audio Mixer")]
    [SerializeField] private AudioMixer masterMixer;

    [Header("Volume")]
    public float MasterVolume { get; private set; }
    public float MusicVolume { get; private set; }
    public float SFXVolume { get; private set; }
    public float VoiceVolume { get; private set; }

    // ============================================================================================== //
    private const float DB_CONVERSION_FACTOR = 20f;
    private const float LOG_SAFE_MINIMUM = 0.0001f;

    // ============================================================================================== //
    private const string MasterAudioParam = "MasterAudio";
    private const string MusicOutputParam = "Music";
    private const string SFXOutputParam = "SFX";
    private const string VoiceOutputParam = "Voice";

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        LoadSavedVolumes();

    }
    private void Start()
    {
        VolumesInitialization();
    }

    // ============================================================================================== //

    private void LoadSavedVolumes()
    {
        MasterVolume = PlayerPrefs.GetFloat(PlayerPrefsKeys.MasterVolume, 1f);
        MusicVolume = PlayerPrefs.GetFloat(PlayerPrefsKeys.MusicVolume, 1f);
        SFXVolume = PlayerPrefs.GetFloat(PlayerPrefsKeys.SFXVolume, 1f);
        VoiceVolume = PlayerPrefs.GetFloat(PlayerPrefsKeys.VoiceVolume, 1f);
    }

    private void VolumesInitialization()
    {
        ApplyVolume(MasterAudioParam, MasterVolume);
        ApplyVolume(MusicOutputParam, MusicVolume);
        ApplyVolume(SFXOutputParam, SFXVolume);
        ApplyVolume(VoiceOutputParam, VoiceVolume);
    }
    private void ApplyVolume(string mixerParameter, float volumeValue)
    {
        float db = Mathf.Log10(Mathf.Clamp(volumeValue, LOG_SAFE_MINIMUM, 1f)) * DB_CONVERSION_FACTOR;
        masterMixer.SetFloat(mixerParameter, db);
    }

    // ============================================================================================== //
    internal void SetMasterVolume(float value)
    {
        MasterVolume = value;
        PlayerPrefs.SetFloat(PlayerPrefsKeys.MasterVolume, value);
        ApplyVolume(MasterAudioParam, value);
        PlayerPrefs.Save();
    }

    internal void SetMusicVolume(float value)
    {
        MusicVolume = value;
        PlayerPrefs.SetFloat(PlayerPrefsKeys.MusicVolume, value);
        ApplyVolume(MusicOutputParam, value);
        PlayerPrefs.Save();
    }

    internal void SetSFXVolume(float value)
    {
        SFXVolume = value;
        PlayerPrefs.SetFloat(PlayerPrefsKeys.SFXVolume, value);
        ApplyVolume(SFXOutputParam, value);
        PlayerPrefs.Save();
    }

    internal void SetVoiceVolume(float value)
    {
        VoiceVolume = value;
        PlayerPrefs.SetFloat(PlayerPrefsKeys.VoiceVolume, value);
        ApplyVolume(VoiceOutputParam, value);
        PlayerPrefs.Save();
    }
}
