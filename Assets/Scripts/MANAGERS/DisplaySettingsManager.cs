using System.Collections.Generic;
using ERP.Discord;
using UnityEngine;

public class DisplaySettingsManager : MonoBehaviour
{
    public static DisplaySettingsManager Instance { get; private set;}

    private static readonly int[] _fpsOptions = {10, 24, 30, 60, 120, 144, 240, 360};

    private static readonly FullScreenMode[] _windowModes = {
        FullScreenMode.ExclusiveFullScreen,
        FullScreenMode.FullScreenWindow,
        FullScreenMode.MaximizedWindow,
        FullScreenMode.Windowed
    };
    public int TargetFPS {get; private set;}
    public int ResolutionIndex {get; private set;}
    public FullScreenMode WindowMode {get; private set;}
    public bool VSync {get; private set;}
    
    public List<Resolution> UniqueResolutions {get; private set;}

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        BuildResolutionList();
        LoadAll();
    }

    private void BuildResolutionList () {
        UniqueResolutions = new List<Resolution>();
        HashSet<string> seen = new HashSet<string>();

        foreach (Resolution res in Screen.resolutions)
        {
            string key = res.width + "x" + res.height;

            if (seen.Add(key))
            {
                UniqueResolutions.Add(res);
            }
        }
    }  

    private void LoadAll()
    {
        TargetFPS = PlayerPrefs.GetInt(PlayerPrefsKeys.TargetFPS, 3);
        ResolutionIndex = PlayerPrefs.GetInt(PlayerPrefsKeys.ScreenResolution, 0);
        WindowMode = (FullScreenMode)PlayerPrefs.GetInt(PlayerPrefsKeys.WindowMode, 0);
        VSync = PlayerPrefs.GetInt(PlayerPrefsKeys.VSync, 1) != 0;

        DisplayInitialization();
    }


    public void DisplayInitialization () {
        ApplyResolutionAndWindowMode();
        ApplyVSync();
        ApplyFPS();
    }

    // ============================================================================================== //
    public List<string> GetFPSOptions()
    {
        List<string> options = new List<string>();
        foreach (int fps in _fpsOptions)
        {        
            options.Add(fps.ToString());
        }
        return options;
    }

    public List<string> GetWindowModeOptions()
    {
        List<string> options = new List<string>();
        foreach (FullScreenMode mode in _windowModes)
        {
            options.Add(mode.ToString());
        }
        return options;
    }
    // ============================================================================================== //    

    public void ApplyResolutionAndWindowMode () {
        if (ResolutionIndex < UniqueResolutions.Count)
        {
            Screen.SetResolution(
                UniqueResolutions[ResolutionIndex].width,
                UniqueResolutions[ResolutionIndex].height,
                WindowMode
            );
        }
    }

    public void ApplyFPS () {

        if (TargetFPS < _fpsOptions.Length)
        {
            Application.targetFrameRate = _fpsOptions[TargetFPS];
        }
    }

    public void ApplyVSync () {
        QualitySettings.vSyncCount = VSync ? 1 : 0;
    }

    // ============================================================================================== //

    public void SetFPS(int index)
    {
        TargetFPS = index;
        PlayerPrefs.SetInt(PlayerPrefsKeys.TargetFPS, index);
        PlayerPrefs.Save();
        ApplyFPS();
    }
    public void SetResolution (int index) {
        ResolutionIndex = index;
        PlayerPrefs.SetInt(PlayerPrefsKeys.ScreenResolution, index);
        PlayerPrefs.Save();
        ApplyResolutionAndWindowMode();
    }
    public void SetWindowMode (int index) {
        WindowMode = _windowModes[index];
        PlayerPrefs.SetInt(PlayerPrefsKeys.WindowMode, (int)WindowMode);
        PlayerPrefs.Save();
        ApplyResolutionAndWindowMode();
    }
    public void SetVSync(bool value)
    {
        VSync = value;
        PlayerPrefs.SetInt(PlayerPrefsKeys.VSync, value ? 1 : 0);
        PlayerPrefs.Save();
        ApplyVSync();
    }
}
