using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : Menu<PauseMenu>
{
    [Header("Pause Menu")]
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button mainMenuButton;

    bool menuState = false;

    private void Start()
    {
        if (InputMananger.Instance)
        {
            InputMananger.Instance.OnPauseInputTriggered += InputMananger_OnPauseInputTriggered;
        }

        gameObject.SetActive(false);
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
        menuState = !menuState;
        if (menuState)
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
        //Resume the pause
    }

    private void SettingsButton_OnClick()
    {
        //Open settings menu
    }

    private void MainMenuButton_OnClick()
    {
        //Go back to main menu scene
    }
}
