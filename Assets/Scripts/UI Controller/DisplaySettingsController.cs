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
    private bool _isInitialized = false;

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

        ddFPS.onValueChanged.AddListener(DisplaySettingsManager.Instance.SetFPS);
        ddResolution.onValueChanged.AddListener(DisplaySettingsManager.Instance.SetResolution);
        ddWindowMode.onValueChanged.AddListener(DisplaySettingsManager.Instance.SetWindowMode);
        vSyncToggle.onValueChanged.AddListener(DisplaySettingsManager.Instance.SetVSync);

        SyncUI();
        _isInitialized = true;
    }

    private void OnEnable()
    {
        if (!_isInitialized) return;
        SyncUI();
    }

    private void SyncUI()
    {
        ddFPS.SetValueWithoutNotify(DisplaySettingsManager.Instance.TargetFPS);
        ddResolution.SetValueWithoutNotify(DisplaySettingsManager.Instance.ResolutionIndex);
        ddWindowMode.SetValueWithoutNotify((int)DisplaySettingsManager.Instance.WindowMode);
        vSyncToggle.SetIsOnWithoutNotify(DisplaySettingsManager.Instance.VSync);
    }
}