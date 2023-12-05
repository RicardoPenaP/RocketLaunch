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
    [SerializeField] private Button goBackButton;

    public event Action OnSelectMissionButtonPressed;
    public event Action OnUpgradeRocketButtonPressed;
    public event Action OnGoBackButtonPressed;

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

        if (goBackButton)
        {
            goBackButton.onClick.AddListener(GoBackButton_OnClick);
        }
    }

    private void Start()
    {
        if (MainMenu.Instance)
        {
            MainMenu.Instance.OnPlayButtonPressed += MainMenu_OnPlayButtonPressed;
        }

        if (SelectMissionMenu.Instance)
        {
            SelectMissionMenu.Instance.OnGoBackButtonPressed += SelectMissionMenu_OnGoBackButtonPressed;
        }

        if (UpgradeRocketMenu.Instance)
        {
            UpgradeRocketMenu.Instance.OnGoBackButtonPressed += UpgradeRocketMenu_OnGoBackButtonPressed;
        }

        gameObject.SetActive(false);
        menuOpened = false;
    }

    private void OnDestroy()
    {

        if (selectMissionButton)
        {
            selectMissionButton.onClick.RemoveListener(SelectMissionButton_OnClick);
        }

        if (upgradeRocketButton)
        {
            upgradeRocketButton.onClick.RemoveListener(UpgradeRocketButton_OnClick);
        }

        if (goBackButton)
        {
            goBackButton.onClick.RemoveListener(GoBackButton_OnClick);
        }

        if (goBackButton)
        {
            goBackButton.onClick.RemoveListener(GoBackButton_OnClick);
        }

        if (MainMenu.Instance)
        {
            MainMenu.Instance.OnPlayButtonPressed -= MainMenu_OnPlayButtonPressed;
        }

        if (SelectMissionMenu.Instance)
        {
            SelectMissionMenu.Instance.OnGoBackButtonPressed -= SelectMissionMenu_OnGoBackButtonPressed;
        }

        if (UpgradeRocketMenu.Instance)
        {
            UpgradeRocketMenu.Instance.OnGoBackButtonPressed -= UpgradeRocketMenu_OnGoBackButtonPressed;
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
        CloseMenu();
    }

    private void UpgradeRocketButton_OnClick()
    {
        OnUpgradeRocketButtonPressed?.Invoke();
        CloseMenu();
    }

    private void GoBackButton_OnClick()
    {
        OnGoBackButtonPressed?.Invoke();
        CloseMenu();
    }

    private void MainMenu_OnPlayButtonPressed()
    {
        OpenMenu();
    }

    private void SelectMissionMenu_OnGoBackButtonPressed()
    {
        OpenMenu();
    }

    private void UpgradeRocketMenu_OnGoBackButtonPressed()
    {
        OpenMenu();
    }

    private void SetButtonsInteractable(bool state)
    {
        selectMissionButton.interactable = state;
        upgradeRocketButton.interactable = state;
        goBackButton.interactable = state;        
    }
}
