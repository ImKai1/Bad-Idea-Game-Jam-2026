using UnityEngine;
using UnityEngine.Audio;

public class _GameLoadSetupScript : MonoBehaviour
{
    [SerializeField] private AudioMixer masterMixer;
    _DisplaySettingsManagerFix2 displaySetting;
    void Awake()
    {
        // DontDestroyOnLoad(gameObject);
        displaySetting = GameObject.FindGameObjectWithTag("Display").GetComponent<_DisplaySettingsManagerFix2>();
        Debug.Log("Awake1 done");
    }
    void Start()
    {
        InitializeDisplay();
        InitializeVolume();
        Debug.Log("Start1 done");
    }
    public void InitializeDisplay () {
        Debug.Log("DISPLAY INIT");
        int savedTargetFPS = PlayerPrefs.GetInt("TargetFPS");
        int savedResolution = PlayerPrefs.GetInt("ScreenResolution");
        int savedWindowMode = PlayerPrefs.GetInt("WindowMode");

        if (int.TryParse(displaySetting.fps[savedTargetFPS], out int fps))
        {            
            Application.targetFrameRate = fps;
        }
        Screen.SetResolution(displaySetting.selectedResolutionList[savedResolution].width, 
                                displaySetting.selectedResolutionList[savedResolution].height,
                                displaySetting.selectedFSMList[savedWindowMode]);
    }

    public void InitializeVolume () {
        Debug.Log("AUDIO INIT");
        float savedMasterVolume = Mathf.Log10(Mathf.Clamp(PlayerPrefs.GetFloat("MasterVolume", 1), 0.0001f, 1f)) * 20;
        float savedMusicVolume = Mathf.Log10(Mathf.Clamp(PlayerPrefs.GetFloat("MusicVolume", 1), 0.0001f, 1f)) * 20;
        float savedSFXVolume = Mathf.Log10(Mathf.Clamp(PlayerPrefs.GetFloat("SFXVolume", 1), 0.0001f, 1f)) * 20;
        float savedVoiceVolume = Mathf.Log10(Mathf.Clamp(PlayerPrefs.GetFloat("VoiceVolume", 1), 0.0001f, 1f)) * 20;        

        masterMixer.SetFloat("MasterAudio", savedMasterVolume);
        masterMixer.SetFloat("Music", savedMusicVolume);
        masterMixer.SetFloat("SFX", savedSFXVolume);
        masterMixer.SetFloat("Voice", savedVoiceVolume);
    }

}
