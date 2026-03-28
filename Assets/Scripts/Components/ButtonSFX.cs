using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonSFX : MonoBehaviour
{
    private Button _button;
    private void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(PlaySFX);
    }

    private void PlaySFX()
    {
        GameEvents.Fire(GameEventKeys.CursorClick);
    }
}
