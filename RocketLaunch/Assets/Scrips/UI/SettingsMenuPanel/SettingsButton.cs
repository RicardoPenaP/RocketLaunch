using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class SettingsButton : MonoBehaviour
{
    public event Action<SettingsPanel> OnAnySettingsButtonPressed;

    [Header("Settings Button")]
    [SerializeField] private Button button;
    [SerializeField] private SettingsPanel settingsPanelPrefab;

    private void Awake()
    {
        if (button)
        {
            button.onClick.AddListener(Button_OnClick);
        }
    }

    private void OnDestroy()
    {
        if (button)
        {
            button.onClick.RemoveListener(Button_OnClick);
        }
    }

    private void Button_OnClick()
    {
        OnAnySettingsButtonPressed?.Invoke(settingsPanelPrefab);
    }


}
