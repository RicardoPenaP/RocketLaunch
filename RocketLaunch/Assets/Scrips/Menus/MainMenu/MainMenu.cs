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

    private void Start()
    {
        if (PlayMenu.Instance)
        {
            PlayMenu.Instance.OnGoBackButtonPressed += PlayMenu_OnGoBackButtonPressed;
        }

        if (CreditsMenu.Instance)
        {
            CreditsMenu.Instance.OnGoBackButtonPressed += CreditsMenu_OnGoBackButtonPressed;
        }

        if (InstructionsMenu.Instance)
        {
            InstructionsMenu.Instance.OnGoBackButtonPressed += InstructionsMenu_OnGoBackButtonPressed;
        }

        if (SettingsMenu.Instance)
        {
            SettingsMenu.Instance.OnGoBackButtonPressed += SettingsMenu_OnGoBackButtonPressed;
        }
        menuOpened = true;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
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

        if (PlayMenu.Instance)
        {
            PlayMenu.Instance.OnGoBackButtonPressed -= PlayMenu_OnGoBackButtonPressed;
        }

        if (CreditsMenu.Instance)
        {
            CreditsMenu.Instance.OnGoBackButtonPressed -= CreditsMenu_OnGoBackButtonPressed;
        }

        if (InstructionsMenu.Instance)
        {
            InstructionsMenu.Instance.OnGoBackButtonPressed -= InstructionsMenu_OnGoBackButtonPressed;
        }

        if (SettingsMenu.Instance)
        {
            SettingsMenu.Instance.OnGoBackButtonPressed -= SettingsMenu_OnGoBackButtonPressed;
        }
    }

    protected override void OpenMenu(Action onOpenAnimationEndedActions = null)
    {
        base.OpenMenu(onOpenAnimationEndedActions);
        this.onOpenAnimationEndedActions += () => { SetButtonsInteractable(true); };
    }

    protected override void CloseMenu(Action onCloseAnimationEndedActions = null)
    {
        base.CloseMenu(onCloseAnimationEndedActions);
        SetButtonsInteractable(false);
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

    private void PlayMenu_OnGoBackButtonPressed()
    {
        OpenMenu();
    }

    private void CreditsMenu_OnGoBackButtonPressed()
    {
        OpenMenu();
    }

    private void InstructionsMenu_OnGoBackButtonPressed()
    {
        OpenMenu();
    }

    private void SettingsMenu_OnGoBackButtonPressed()
    {
        OpenMenu();
    }

    private void SetButtonsInteractable(bool state)
    {
        playButton.interactable = state;
        instrucctionsButton.interactable = state;
        settingsButton.interactable = state;
        creditsButton.interactable = state;
        exitButton.interactable = state;
    }
}
