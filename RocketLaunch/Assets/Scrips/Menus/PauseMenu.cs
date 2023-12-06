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

    protected override void Awake()
    {
        base.Awake();
        resumeButton.onClick.AddListener(ResumeButton_OnClick);
        settingsButton.onClick.AddListener(SettingsButton_OnClick);
        mainMenuButton.onClick.AddListener(MainMenuButton_OnClick);        
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

    private void OnDestroy()
    {
        if (InputMananger.Instance)
        {
            InputMananger.Instance.OnPauseInputTriggered -= InputMananger_OnPauseInputTriggered;
        }
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
        //Open settings menu
        Debug.Log("Still working on that");
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
