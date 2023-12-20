using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using Settings;

public class SettingsMenu : Menu<SettingsMenu>
{
    [Header("Settings Menu")]
    [SerializeField] private Button goBackButton;
    [SerializeField] private Button resetButton;

    public event Action OnGoBackButtonPressed;
    public event Action OnResetButtonPressed;

    protected override void Awake()
    {
        base.Awake();
        if (goBackButton)
        {
            goBackButton.onClick.AddListener(GoBackButton_OnClick);
        }
        if (resetButton)
        {
            resetButton.onClick.AddListener(ResetButton_OnClick);
        }
    }

    private void Start()
    {
        if (MainMenu.Instance)
        {
            MainMenu.Instance.OnSettingsButtonPressed += MainMenu_OnSettingsButtonPressed;
        }
        gameObject.SetActive(false);
        menuOpened = false;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        if (goBackButton)
        {
            goBackButton.onClick.RemoveListener(GoBackButton_OnClick);
        }

        if (resetButton)
        {
            resetButton.onClick.RemoveListener(ResetButton_OnClick);
        }

        if (MainMenu.Instance)
        {
            MainMenu.Instance.OnSettingsButtonPressed -= MainMenu_OnSettingsButtonPressed;
        }


    }

    protected override void CloseMenu(Action onCloseAnimationEndedActions = null)
    {
        SettingsDataLoader.SaveSettingsData();
        base.CloseMenu(onCloseAnimationEndedActions);

    }

    private void GoBackButton_OnClick()
    {
        OnGoBackButtonPressed?.Invoke();
        CloseMenu();
    }

    private void ResetButton_OnClick()
    {
        OnResetButtonPressed?.Invoke();
    }

    private void MainMenu_OnSettingsButtonPressed()
    {
        OpenMenu();
    }

}
