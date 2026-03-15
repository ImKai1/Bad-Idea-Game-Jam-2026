using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class DisplaySettingsController : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown ddFPS;
    [SerializeField] private TMP_Dropdown ddResolution;
    [SerializeField] private TMP_Dropdown ddWindowMode;
    [SerializeField] private Toggle vSyncToggle;

    private void Start()
    {
        ddFPS.AddOptions(DisplaySettingsManager.Instance.GetFPSOptions());

        List<string> resOptions = new List<string>();
        foreach (Resolution res in DisplaySettingsManager.Instance.UniqueResolutions)
        {   
            resOptions.Add(res.width + " x " + res.height);
        }

        ddResolution.AddOptions(resOptions);

        ddWindowMode.AddOptions(DisplaySettingsManager.Instance.GetWindowModeOptions());
    }

    private void OnEnable () {
        ddFPS.SetValueWithoutNotify(DisplaySettingsManager.Instance.TargetFPS);
        ddResolution.SetValueWithoutNotify(DisplaySettingsManager.Instance.ResolutionIndex);
        ddWindowMode.SetValueWithoutNotify((int)DisplaySettingsManager.Instance.WindowMode);
        vSyncToggle.SetIsOnWithoutNotify(DisplaySettingsManager.Instance.VSync);
    }

    public void OnFPSChanged(int index) => DisplaySettingsManager.Instance.SetFPS(index);
    public void OnResolutionChanged(int index) => DisplaySettingsManager.Instance.SetResolution(index);
    public void OnWindowModeChanged(int index) => DisplaySettingsManager.Instance.SetWindowMode(index);
    public void OnVSyncChanged(bool value) => DisplaySettingsManager.Instance.SetVSync(value);
}