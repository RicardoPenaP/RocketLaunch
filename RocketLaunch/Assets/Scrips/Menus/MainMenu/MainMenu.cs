using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class MainMenu : Menu<MainMenu>
{
    [Header("Main Menu")]
    [Header("Buttons references")]
    [SerializeField] private Button playButton;
    [SerializeField] private Button instrucctionsButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button creditsButton;
    [SerializeField] private Button exitButton;

    public event Action OnPlayButtonPressed;
    public event Action OnInstructionsButtonPressed;
    public event Action OnSettingsButtonPressed;
    public event Action OnCreditsButtonPressed;


    protected override void Awake()
    {
        base.Awake();

        if (playButton)
        {
            playButton.onClick.AddListener(PlayButton_OnClick);
        }

        if (instrucctionsButton)
        {
            instrucctionsButton.onClick.AddListener(InstructionsButton_OnClick);
        }

        if (settingsButton)
        {
            settingsButton.onClick.AddListener(SettingsButton_OnClick);
        }

        if (creditsButton)
        {
            creditsButton.onClick.AddListener(CreditsButton_OnClick);
        }

        if (exitButton)
        {
            exitButton.onClick.AddListener(ExitButton_OnClick);
        }
        
    }

    private void OnDestroy()
    {
        if (playButton)
        {
            playButton.onClick.RemoveListener(PlayButton_OnClick);
        }

        if (instrucctionsButton)
        {
            instrucctionsButton.onClick.RemoveListener(InstructionsButton_OnClick);
        }

        if (settingsButton)
        {
            settingsButton.onClick.RemoveListener(SettingsButton_OnClick);
        }

        if (creditsButton)
        {
            creditsButton.onClick.AddListener(CreditsButton_OnClick);
        }

        if (exitButton)
        {
            exitButton.onClick.RemoveListener(ExitButton_OnClick);
        }
    }

    private void PlayButton_OnClick()
    {
        OnPlayButtonPressed?.Invoke();
        CloseMenu();
    }

    private void InstructionsButton_OnClick()
    {
        OnInstructionsButtonPressed?.Invoke();
        CloseMenu();
    }

    private void SettingsButton_OnClick()
    {
        OnSettingsButtonPressed?.Invoke();
        CloseMenu();
    }

    private void CreditsButton_OnClick()
    {
        OnCreditsButtonPressed?.Invoke();
        CloseMenu();
    }

    private void ExitButton_OnClick()
    {
        Application.Quit();
    }
}
