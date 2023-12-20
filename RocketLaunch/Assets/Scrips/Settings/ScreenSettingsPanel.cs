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

    private bool fullScreenButtonSelected = true;
    private bool fpsCounterButtonSelected = false;

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

    private void Start()
    {
        if (SettingsMenu.Instance)
        {
            SettingsMenu.Instance.OnMenuOpened += SettingsMenu_OnMenuOpened;
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

        if (SettingsMenu.Instance)
        {
            SettingsMenu.Instance.OnMenuOpened -= SettingsMenu_OnMenuOpened;
        }

    }

    private void QualityDropdown_OnValueChanged(int value)
    {
        SettingsController.SetQuality((QualityOptions)value);
    }

    private void MaxFPSDropdown_OnValueChanged(int value)
    {
        SettingsController.SetTargetFPS((TargetFPSOptions)value);
    }

    private void FullScreenButton_OnClick()
    {
        fullScreenButtonSelected = !fullScreenButtonSelected;
        fullScreenButton.transform.GetChild(0).gameObject.SetActive(fullScreenButtonSelected);
        SettingsController.SetFullScreen(fullScreenButtonSelected);
    }

    private void FPSCounterButton_OnClick()
    {
        fpsCounterButtonSelected = !fpsCounterButtonSelected;
        fpsCounterButton.transform.GetChild(0).gameObject.SetActive(fpsCounterButtonSelected);
        SettingsController.SetFPSCounterState(fpsCounterButtonSelected);
    }

    private void SettingsMenu_OnMenuOpened()
    {
        qualityDropdown.value = (int)SettingsController.CurrentQualityOption;
        maxFPSDropdown.value = (int)GetTargetFPSOption();

        fullScreenButtonSelected = Screen.fullScreen;
        fullScreenButton.transform.GetChild(0).gameObject.SetActive(fullScreenButtonSelected);

        fpsCounterButtonSelected = SettingsController.FPSCounterIsActive;
        fpsCounterButton.transform.GetChild(0).gameObject.SetActive(fpsCounterButtonSelected);
    }

    private TargetFPSOptions GetTargetFPSOption()
    {
        switch (SettingsController.CurrentTargetFPS)
        {
            case 30:
                return TargetFPSOptions.Low;                
            case 60:
                return TargetFPSOptions.Mid;
            case 120:
                return TargetFPSOptions.High;
            case 240:
                return TargetFPSOptions.Ultra;
            case -1:
                return TargetFPSOptions.Unlimited;
            default:
                break;
        }

        return TargetFPSOptions.Low;
    }


}
