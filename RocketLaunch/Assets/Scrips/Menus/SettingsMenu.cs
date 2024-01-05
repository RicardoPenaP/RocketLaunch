using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using Settings;
using GameSceneManagement;

public class SettingsMenu : Menu<SettingsMenu>
{
    [Header("Settings Menu")]
    [SerializeField] private Button goBackButton;
    [SerializeField] private Button resetButton;
    [SerializeField] private Button deleteSavedDataButton;

    public event Action OnGoBackButtonPressed;
    public event Action OnResetButtonPressed;
    public event Action OnDeleteSavedDataButtonPressed;

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
        if (deleteSavedDataButton)
        {
            deleteSavedDataButton.onClick.AddListener(DeleteSavedDataButton_OnClick);
        }

        DeleteSavedDataPanel.OnDeleteAllButtonPressed += DeleteSavedDataPanel_OnDeleteAllButtonPressed;
    }

    private void Start()
    {
        if (MainMenu.Instance)
        {
            MainMenu.Instance.OnSettingsButtonPressed += MainMenu_OnSettingsButtonPressed;
        }

        if (SceneManagement.GetCurrentScene() != GameScene.MainMenu)
        {
            deleteSavedDataButton.gameObject.SetActive(false);
        }

        if (PauseMenu.Instance)
        {
            PauseMenu.Instance.OnSettingsButtonPressed += PauseMenu_OnSettingsButtonPressed;
            PauseMenu.Instance.OnPauseMenuClose += PauseMenu_OnPauseMenuClose;
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

        if (deleteSavedDataButton)
        {
            deleteSavedDataButton.onClick.RemoveListener(DeleteSavedDataButton_OnClick);
        }

        if (MainMenu.Instance)
        {
            MainMenu.Instance.OnSettingsButtonPressed -= MainMenu_OnSettingsButtonPressed;
        }

        if (PauseMenu.Instance)
        {
            PauseMenu.Instance.OnSettingsButtonPressed -= PauseMenu_OnSettingsButtonPressed;
            PauseMenu.Instance.OnPauseMenuClose -= PauseMenu_OnPauseMenuClose;
        }

        DeleteSavedDataPanel.OnDeleteAllButtonPressed -= DeleteSavedDataPanel_OnDeleteAllButtonPressed;
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

    private void DeleteSavedDataButton_OnClick()
    {
        OnDeleteSavedDataButtonPressed?.Invoke();
    }

    private void MainMenu_OnSettingsButtonPressed()
    {
        OpenMenu();
    }

    private void PauseMenu_OnSettingsButtonPressed()
    {
        OpenMenu();
    }

    private void PauseMenu_OnPauseMenuClose()
    {
        CloseMenu();
    }

    private void DeleteSavedDataPanel_OnDeleteAllButtonPressed()
    {
        OnGoBackButtonPressed?.Invoke();
        CloseMenu();
    }

}
