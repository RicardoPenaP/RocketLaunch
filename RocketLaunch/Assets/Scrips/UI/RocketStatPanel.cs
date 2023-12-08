using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class RocketStatPanel : MonoBehaviour
{
    public static event Action OnAnyLevelUpButtonPressed;
    public static event Action OnAnyLevelDownButtonPressed;

    [Header("Rocket Stat Panel")]
    [SerializeField] private StatType statToControl;

    [Header("References Settings")]
    [SerializeField] private TextMeshProUGUI currentLevelText;
    [SerializeField] private Button levelUpButton;
    [SerializeField] private Button levelDownButton;

    private RocketStat rocketStat;

    private void Awake()
    {
        if (levelUpButton)
        {
            levelUpButton.onClick.AddListener(LevelUpButton_OnClick);
        }

        if (levelDownButton)
        {
            levelDownButton.onClick.AddListener(LevelDownButton_OnClick);
        }
    }

    private void Start()
    {
        if (UpgradeRocketMenu.Instance)
        {
            UpgradeRocketMenu.Instance.OnMenuOpened += UpgradeRocketMenu_OnMenuOpened;
        }
    }

    private void OnDestroy()
    {
        if (levelUpButton)
        {
            levelUpButton.onClick.RemoveListener(LevelUpButton_OnClick);
        }

        if (levelDownButton)
        {
            levelDownButton.onClick.RemoveListener(LevelDownButton_OnClick);
        }

        if (UpgradeRocketMenu.Instance)
        {
            UpgradeRocketMenu.Instance.OnMenuOpened -= UpgradeRocketMenu_OnMenuOpened;
        }
    }

    private void LevelUpButton_OnClick()
    {
        OnAnyLevelUpButtonPressed?.Invoke();
        rocketStat.LevelUp();
    }

    private void LevelDownButton_OnClick()
    {
        OnAnyLevelDownButtonPressed?.Invoke();
        rocketStat.LevelDown();
    }

    private void UpgradeRocketMenu_OnMenuOpened()
    {
        if (RocketStatsMananger.Instance)
        {
            rocketStat = RocketStatsMananger.Instance.GetRocketStat(statToControl);           
            UpdateCurrentLevelText();
        }
    }

    private void UpdateCurrentLevelText()
    {
        currentLevelText.text = $"Level: {rocketStat.GetStatLevel()}";
    }
}
