using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
public class _DisplaySettingsManagerFix2 : MonoBehaviour
{
    [Header("RESOLUTION")]
    [SerializeField] TMP_Dropdown ddTargetFPS;
    [SerializeField] TMP_Dropdown ddResolution;
    // [SerializeField] Toggle FullScreenToggle;
    [SerializeField] TMP_Dropdown ddFullScreenMode;
    [SerializeField] Toggle vSyncToggle;
    [System.NonSerialized] public List<string> fps = new List<string>() {"10", "30", "60", "120", "144", "240", "360"};
    Resolution[] allResolutions;
    // private bool isFullScreen;
    FullScreenMode[] fullScreenModes = {FullScreenMode.ExclusiveFullScreen, 
                                        FullScreenMode.FullScreenWindow, 
                                        FullScreenMode.MaximizedWindow, 
                                        FullScreenMode.Windowed};
    int selectedFPSInt, selectedResolution, selectedFSM;
    public List<Resolution> selectedResolutionList = new List<Resolution>();
    public List<FullScreenMode> selectedFSMList = new List<FullScreenMode>();

    int currValRes = 0, curValFSM = 0;

    private void Awake()
    {
        FullScreenMode currentMode = Screen.fullScreenMode;
        // isFullScreen = true;
        allResolutions = Screen.resolutions;
        
        ddTargetFPS.AddOptions(fps);
        
        List<string> resolutionStringList = new List<string>();
        string newRes;
        int i = 0;
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

        int j = 0;
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

        Application.targetFrameRate = -1;

        ddTargetFPS.value = PlayerPrefs.GetInt("TargetFPS");
        SetFPS();

        // Debug.Log("FPS IS LOADED");

        ddResolution.value = PlayerPrefs.GetInt("ScreenResolution", currValRes);
        // FullScreenToggle.isOn = PlayerPrefs.GetInt("FullScreen", 1) != 0;
        ddFullScreenMode.value = PlayerPrefs.GetInt("WindowMode", curValFSM);
        SetResolution();

        vSyncToggle.onValueChanged.AddListener(OnVSyncToggled);
        vSyncToggle.SetIsOnWithoutNotify(PlayerPrefs.GetInt("VSync", 1) != 0);
        ApplyVSync();
    }

    private void Start () {
        // vSyncToggle.isOn = PlayerPrefs.GetInt("VSync", 1) != 0;
        // SetVSync();
    }

    // public void SetQuality (int qualityIndex) {
    //     QualitySettings.SetQualityLevel(qualityIndex);
    // }

    public void SetFPS () {
        if (int.TryParse(fps[ddTargetFPS.value], out selectedFPSInt))
        {            
            Debug.Log("FPS ENTEREDDD: " + selectedFPSInt);
            Application.targetFrameRate = selectedFPSInt;
        }
        else
        {
            Debug.LogWarning("FPS Input is NOT valid");
        }
        PlayerPrefs.SetInt("TargetFPS", ddTargetFPS.value);
        PlayerPrefs.Save();
    }

    public void SetResolution()
    {
        selectedResolution = ddResolution.value;
        Screen.SetResolution(selectedResolutionList[selectedResolution].width, selectedResolutionList[selectedResolution].height, selectedFSMList[selectedFSM]);
        PlayerPrefs.SetInt("ScreenResolution", selectedResolution);
        PlayerPrefs.Save();
    }

    public void SetWindowMode()
    {
        selectedFSM = ddFullScreenMode.value;
        Screen.SetResolution(selectedResolutionList[selectedResolution].width, selectedResolutionList[selectedResolution].height, selectedFSMList[selectedFSM]);
        PlayerPrefs.SetInt("WindowMode", selectedFSM);
        PlayerPrefs.Save();
    }

    // public void SetVSync()
    // {
    //     Debug.Log("Enter VSync Change");
    //     int vSyncValue = vSyncToggle.isOn ? 1 : 0;
    //     QualitySettings.vSyncCount = vSyncValue;
    //     Debug.Log("VSync VAL: " + vSyncValue);
    //     PlayerPrefs.SetInt("VSync", vSyncValue);
    //     PlayerPrefs.Save();
    // }

    private void OnVSyncToggled(bool isOn)
    {
        ApplyVSync();
    }

    private void ApplyVSync()
    {
        int vSyncValue = vSyncToggle.isOn ? 1 : 0;
        QualitySettings.vSyncCount = vSyncValue;
        Debug.Log("VSync VAL: " + vSyncValue + " | Unity reports: " + QualitySettings.vSyncCount);
        PlayerPrefs.SetInt("VSync", vSyncValue);
        PlayerPrefs.Save();
    }

}
