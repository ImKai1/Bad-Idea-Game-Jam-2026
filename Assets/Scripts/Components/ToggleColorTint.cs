using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle))]
public class ToggleColorTint : MonoBehaviour
{
    private Toggle _toggle;
    private Image _backgroundImage;
    [SerializeField] Color32 onColor = new(180, 180, 180, 255);
    [SerializeField] Color32 offColor = new(255, 255, 255, 255);
    private void Awake()
    {
        _toggle = GetComponent<Toggle>();
        _backgroundImage = GetComponent<Image>();
        _toggle.onValueChanged.AddListener(ToggleTint);
    }

    private void ToggleTint(bool isOn)
    {
        _backgroundImage.color = isOn ? onColor : offColor;
    }
}
