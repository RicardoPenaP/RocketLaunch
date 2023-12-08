using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;


public class UpgradeRocketMenu : Menu<UpgradeRocketMenu>
{
    [Header("Upgrade Rocket Menu")]
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI remaningStatPointsText;
    [SerializeField] private Button goBackButton;

    public event Action OnGoBackButtonPressed;

    protected override void Awake()
    {
        base.Awake();

        if (goBackButton)
        {
            goBackButton.onClick.AddListener(GoBackButton_OnClick);
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

    private void OnDestroy()
    {
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

    private void GoBackButton_OnClick()
    {
        OnGoBackButtonPressed?.Invoke();
        CloseMenu();
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
