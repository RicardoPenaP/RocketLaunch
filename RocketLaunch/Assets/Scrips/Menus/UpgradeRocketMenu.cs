using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;


public class UpgradeRocketMenu : Menu<UpgradeRocketMenu>
{
    public static event Action OnSaveTheStatsData;

    [Header("Upgrade Rocket Menu")]
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI remaningStatPointsText;
    [SerializeField] private Button goBackButton;
    [SerializeField] private Button resetStatsButton;

    public event Action OnGoBackButtonPressed;
    public event Action OnResetStatsButtonPressed;

    protected override void Awake()
    {
        base.Awake();

        if (goBackButton)
        {
            goBackButton.onClick.AddListener(GoBackButton_OnClick);
        }

        if (resetStatsButton)
        {
            resetStatsButton.onClick.AddListener(ResetStatsButton_OnClick);
        }

    }

    private void Start()
    {
        if (PlayMenu.Instance)
        {
            PlayMenu.Instance.OnUpgradeRocketButtonPressed += PlayMenu_OnUpgradeRocketButtonPressed;
        }

        if (RocketStatsMananger.Instance)
        {
            RocketStatsMananger.Instance.OnCurrentStatPointsChanged += RocketStatMananger_OnCurrentStatPointsChanged;
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

        if (PlayMenu.Instance)
        {
            PlayMenu.Instance.OnUpgradeRocketButtonPressed -= PlayMenu_OnUpgradeRocketButtonPressed;
        }

        if (RocketStatsMananger.Instance)
        {
            RocketStatsMananger.Instance.OnCurrentStatPointsChanged -= RocketStatMananger_OnCurrentStatPointsChanged;
        }
    }

    protected override void CloseMenu(Action onCloseAnimationEndedActions = null)
    {
        OnSaveTheStatsData?.Invoke();
        base.CloseMenu(onCloseAnimationEndedActions);
    }

    private void GoBackButton_OnClick()
    {
        OnGoBackButtonPressed?.Invoke();
        CloseMenu();
    }

    private void ResetStatsButton_OnClick()
    {
        OnResetStatsButtonPressed?.Invoke();
    }

    private void PlayMenu_OnUpgradeRocketButtonPressed()
    {
        if (RocketLevelMananger.Instance)
        {
            levelText.text = $"Level: {RocketLevelMananger.Instance.GetCurrentLevel()}";
        }

        if (RocketStatsMananger.Instance)
        {
            remaningStatPointsText.text = $"Remaining Points: {RocketStatsMananger.Instance.GetCurrentStatPoints()}";
        }
        OpenMenu();
    }

    private void RocketStatMananger_OnCurrentStatPointsChanged(int currentStatPoints)
    {
        remaningStatPointsText.text = $"Remaining Points: {currentStatPoints}";
    }


}
