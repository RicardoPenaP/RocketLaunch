using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Settings;


public class ScreenSettingsPanel : MonoBehaviour
{
    [Header("Screen Settings Controller")]
    [SerializeField] private TMP_Dropdown qualityDropdown;
    [SerializeField] private TMP_Dropdown maxFPSDropdown;
    [SerializeField] private Button fullScreenButton;
    [SerializeField] private Button fpsCounterButton;

    private void Awake()
    {
        if (qualityDropdown)
        {
            qualityDropdown.onValueChanged.AddListener(QualityDropdown_OnValueChanged);
        }

        if (maxFPSDropdown)
        {
            maxFPSDropdown.onValueChanged.AddListener(MaxFPSDropdown_OnValueChanged);
        }

        if (fullScreenButton)
        {
            fullScreenButton.onClick.AddListener(FullScreenButton_OnClick);
        }

        if (fpsCounterButton)
        {
            fpsCounterButton.onClick.AddListener(FPSCounterButton_OnClick);
        }

    }

    private void OnDestroy()
    {
        if (qualityDropdown)
        {
            qualityDropdown.onValueChanged.RemoveListener(QualityDropdown_OnValueChanged);
        }

        if (maxFPSDropdown)
        {
            maxFPSDropdown.onValueChanged.RemoveListener(MaxFPSDropdown_OnValueChanged);
        }

        if (fullScreenButton)
        {
            fullScreenButton.onClick.RemoveListener(FullScreenButton_OnClick);
        }

        if (fpsCounterButton)
        {
            fpsCounterButton.onClick.RemoveListener(FPSCounterButton_OnClick);
        }

    }

    private void QualityDropdown_OnValueChanged(int value)
    {
        SettingsController.ChangeQuality((QualityOptions)value);
    }

    private void MaxFPSDropdown_OnValueChanged(int value)
    {
        SettingsController.SetTargetFPS((TargetFPSOptions)value);
    }

    private void FullScreenButton_OnClick()
    {
        
    }

    private void FPSCounterButton_OnClick()
    {

    }


}
