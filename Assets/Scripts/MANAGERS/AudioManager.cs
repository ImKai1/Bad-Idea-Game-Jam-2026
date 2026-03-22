using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    // public static AudioManager Instance { get; private set; }

    [Header("Audio Mixer")]
    [SerializeField] private AudioMixer masterMixer;

    [Header("Audio Source")]
    [SerializeField] private AudioSource masterSource;
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioSource voiceSource;

    [Header("Volume")]
    public float MasterVolume { get; private set; }
    public float MusicVolume { get; private set; }
    public float SFXVolume { get; private set; }
    public float VoiceVolume { get; private set; }

    // ============================================================================================== //
    private const float DB_CONVERSION_FACTOR = 20f;
    private const float LOG_SAFE_MINIMUM = 0.0001f;

    private void Awake()
    {
        // if (Instance != null && Instance != this)
        // {
        //     Destroy(gameObject);
        //     return;
        // }

        // Instance = this;
        // DontDestroyOnLoad(gameObject);
        LoadSavedVolumes();

    }

    private void Start()
    {
        VolumesInitialization();
    }

    // ============================================================================================== //

    public void LoadSavedVolumes()
    {
        MasterVolume = PlayerPrefs.GetFloat(PlayerPrefsKeys.MasterVolume, 1f);
        MusicVolume = PlayerPrefs.GetFloat(PlayerPrefsKeys.MusicVolume, 1f);
        SFXVolume = PlayerPrefs.GetFloat(PlayerPrefsKeys.SFXVolume, 1f);
        VoiceVolume = PlayerPrefs.GetFloat(PlayerPrefsKeys.VoiceVolume, 1f);
    }

    private void VolumesInitialization()
    {
        ApplyVolume("MasterAudio", MasterVolume);
        ApplyVolume("Music", MusicVolume);
        ApplyVolume("SFX", SFXVolume);
        ApplyVolume("Voice", VoiceVolume);
    }

    private void ApplyVolume(string mixerParameter, float volumeValue)
    {
        float db = Mathf.Log10(Mathf.Clamp(volumeValue, LOG_SAFE_MINIMUM, 1f)) * DB_CONVERSION_FACTOR;
        masterMixer.SetFloat(mixerParameter, db);
    }

    // ============================================================================================== //

    public void SetMasterVolume(float value)
    {
        MasterVolume = value;
        PlayerPrefs.SetFloat(PlayerPrefsKeys.MasterVolume, value);
        ApplyVolume("MasterAudio", value);
        PlayerPrefs.Save();
    }

    public void SetMusicVolume(float value)
    {
        MusicVolume = value;
        PlayerPrefs.SetFloat(PlayerPrefsKeys.MusicVolume, value);
        ApplyVolume("Music", value);
        PlayerPrefs.Save();
    }

    public void SetSFXVolume(float value)
    {
        SFXVolume = value;
        PlayerPrefs.SetFloat(PlayerPrefsKeys.SFXVolume, value);
        ApplyVolume("SFX", value);
        PlayerPrefs.Save();
    }

    public void SetVoiceVolume(float value)
    {
        VoiceVolume = value;
        PlayerPrefs.SetFloat(PlayerPrefsKeys.VoiceVolume, value);
        ApplyVolume("Voice", value);
        PlayerPrefs.Save();
    }

    // ============================================================================================== //
    public void PlayMusic(AudioClip clip, bool loop = true)
    {
        musicSource.clip = clip;
        musicSource.loop = loop;
        musicSource.Play();
    }
    internal void StopMusic()
    {
        musicSource.Stop();
    }

    internal void PauseMusic()
    {
        musicSource.Pause();
    }

    internal void ResumeMusic()
    {
        musicSource.UnPause();
    }

    // ============================================================================================== //

    public void PlaySFXConsistent(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }

    public void PlaySFXAutoRandomize(AudioClip clip, float randomVolumeChange, float randomPitchChange)
    {
        float randomVolumeValue = Random.Range(SFXVolume - randomVolumeChange, SFXVolume + randomVolumeChange);
        float randomPitchValue = Random.Range(1 - randomPitchChange, 1 + randomPitchChange);

        sfxSource.volume = randomVolumeValue;
        sfxSource.pitch = randomPitchValue;
        sfxSource.PlayOneShot(clip);
    }

    public void PlaySFXRandomizeList(AudioClip[] clips, float randomVolumeChange = 0f, float randomPitchChange = 0f)
    {
        int randomClip = Random.Range(0, clips.Length);
        float randomVolumeValue = Random.Range(1f - randomVolumeChange, 1f + randomVolumeChange);
        float randomPitchValue = Random.Range(1f - randomPitchChange, 1f + randomPitchChange);

        float originalPitch = sfxSource.pitch;
        sfxSource.pitch = randomPitchValue;
        sfxSource.PlayOneShot(clips[randomClip], randomVolumeValue);
        sfxSource.pitch = originalPitch;
    }

    // ============================================================================================== //

    public void PlayVoice(AudioClip clip)
    {
        voiceSource.clip = clip;
        voiceSource.Play();
    }

    internal void StopVoice()
    {
        voiceSource.Stop();
    }

}

