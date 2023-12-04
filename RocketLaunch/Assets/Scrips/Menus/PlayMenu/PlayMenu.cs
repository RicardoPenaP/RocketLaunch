using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class PlayMenu : Menu<PlayMenu>
{
    [Header("Play Menu")]
    [SerializeField] private Button selectMissionButton;
    [SerializeField] private Button upgradeRocketButton;
    [SerializeField] private Button mainMenuButton;

    public event Action OnSelectMissionButtonPressed;
    public event Action OnUpgradeRocketButtonPressed;
    public event Action OnMainMenuButtonPressed;

    protected override void Awake()
    {
        base.Awake();

        if (selectMissionButton)
        {
            selectMissionButton.onClick.AddListener(SelectMissionButton_OnClick);
        }

        if (upgradeRocketButton)
        {
            upgradeRocketButton.onClick.AddListener(UpgradeRocketButton_OnClick);
        }

        if (mainMenuButton)
        {
            mainMenuButton.onClick.AddListener(MainMenuButton_OnClick);
        }
    }

    private void Start()
    {
        if (MainMenu.Instance)
        {
            MainMenu.Instance.OnPlayButtonPressed += MainMenu_OnPlayButtonPressed;
        }
        gameObject.SetActive(false);
        menuOpened = false;
    }

    private void OnDestroy()
    {
        if (mainMenuButton)
        {
            mainMenuButton.onClick.RemoveListener(MainMenuButton_OnClick);
        }

        if (MainMenu.Instance)
        {
            MainMenu.Instance.OnPlayButtonPressed -= MainMenu_OnPlayButtonPressed;
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

    private void SelectMissionButton_OnClick()
    {
        OnSelectMissionButtonPressed?.Invoke();
    }

    private void UpgradeRocketButton_OnClick()
    {
        OnUpgradeRocketButtonPressed?.Invoke();
    }

    private void MainMenuButton_OnClick()
    {
        OnMainMenuButtonPressed?.Invoke();
        CloseMenu();
    }

    private void MainMenu_OnPlayButtonPressed()
    {
        OpenMenu();
    }

    private void SetButtonsInteractable(bool state)
    {
        selectMissionButton.interactable = state;
        upgradeRocketButton.interactable = state;
        mainMenuButton.interactable = state;        
    }
}
