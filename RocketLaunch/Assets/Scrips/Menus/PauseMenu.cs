using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameSceneManagement;

public class PauseMenu : Menu<PauseMenu>
{
    [Header("Pause Menu")]
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button mainMenuButton;

    public event Action OnSettingsButtonPressed;
    public event Action OnPauseMenuClose;

    protected override void Awake()
    {
        base.Awake();
        if (resumeButton)
        {
            resumeButton.onClick.AddListener(ResumeButton_OnClick);
        }

        if (settingsButton)
        {
            settingsButton.onClick.AddListener(SettingsButton_OnClick);
        }

        if (mainMenuButton)
        {
            mainMenuButton.onClick.AddListener(MainMenuButton_OnClick);
        }   
    }

    private void Start()
    {
        if (InputMananger.Instance)
        {
            InputMananger.Instance.OnPauseInputTriggered += InputMananger_OnPauseInputTriggered;
        }

        gameObject.SetActive(false);
        menuOpened = false;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();

        if (resumeButton)
        {
            resumeButton.onClick.RemoveListener(ResumeButton_OnClick);
        }

        if (settingsButton)
        {
            settingsButton.onClick.RemoveListener(SettingsButton_OnClick);
        }

        if (mainMenuButton)
        {
            mainMenuButton.onClick.RemoveListener(MainMenuButton_OnClick);
        }

        if (InputMananger.Instance)
        {
            InputMananger.Instance.OnPauseInputTriggered -= InputMananger_OnPauseInputTriggered;
        }
    }

    protected override void CloseMenu(Action onCloseAnimationEndedActions = null)
    {
        OnPauseMenuClose?.Invoke();
        base.CloseMenu(onCloseAnimationEndedActions);
    }

    private void InputMananger_OnPauseInputTriggered()
    {        
        if (!menuOpened)
        {
            OpenMenu();
        }
        else
        {
            CloseMenu();
        }
    }

    private void ResumeButton_OnClick()
    {             
        CloseMenu();
    }

    private void SettingsButton_OnClick()
    {
        OnSettingsButtonPressed?.Invoke();
    }

    private void MainMenuButton_OnClick()
    {
        CloseMenu(LoadMainMenuScene);
    }

    private void LoadMainMenuScene()
    {
        SceneManagement.LoadScene(GameScene.MainMenu);
    }
    
}
