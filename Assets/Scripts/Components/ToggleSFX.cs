using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle))]
public class ToggleSFX : MonoBehaviour
{
    private Toggle _toggle;
    private void Awake()
    {
        _toggle = GetComponent<Toggle>();
        _toggle.onValueChanged.AddListener(PlayToggleSounds);
    }

    private void PlayToggleSounds (bool isOn) {
        if (isOn)
        {
            PlayTogggleOnSFX();
        }
        else
        {
            PlayTogggleOffSFX();
        }
    }

    private void PlayTogggleOnSFX()
    {
        GameEvents.Fire(GameEventKeys.CursorClick);
    }
    private void PlayTogggleOffSFX()
    {
        GameEvents.Fire(GameEventKeys.CursorClick);
    }
}