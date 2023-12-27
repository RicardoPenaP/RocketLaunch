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
    [SerializeField] private Button plus10Button;
    [SerializeField] private Button minus10Button;

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

        if (plus10Button)
        {
            plus10Button.onClick.AddListener(Plus10Button_OnClick);
        }

        if (minus10Button)
        {
            minus10Button.onClick.AddListener(Minus10Button_OnClick);
        }
    }

    private void Start()
    {
        if (UpgradeRocketMenu.Instance)
        {
            UpgradeRocketMenu.Instance.OnMenuOpened += UpgradeRocketMenu_OnMenuOpened;
        }

        if (RocketStatsMananger.Instance)
        {
            RocketStatsMananger.Instance.OnCurrentStatPointsChanged += RocketStatMananger_OnCurrentStatPointsChanged;
            RocketStatsMananger.Instance.OnResetStatPoints += RocketStatMananger_OnResetStatPoints;
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

        if (plus10Button)
        {
            plus10Button.onClick.RemoveListener(Plus10Button_OnClick);
        }

        if (minus10Button)
        {
            minus10Button.onClick.RemoveListener(Minus10Button_OnClick);
        }

        if (UpgradeRocketMenu.Instance)
        {
            UpgradeRocketMenu.Instance.OnMenuOpened -= UpgradeRocketMenu_OnMenuOpened;
        }

        if (RocketStatsMananger.Instance)
        {
            RocketStatsMananger.Instance.OnCurrentStatPointsChanged -= RocketStatMananger_OnCurrentStatPointsChanged;
            RocketStatsMananger.Instance.OnResetStatPoints -= RocketStatMananger_OnResetStatPoints;
        }
    }

    private void LevelUpButton_OnClick()
    {
        rocketStat.LevelUp();
        UpdateCurrentLevelText();
        OnAnyLevelUpButtonPressed?.Invoke();
    }

    private void LevelDownButton_OnClick()
    {
        rocketStat.LevelDown();
        UpdateCurrentLevelText();
        OnAnyLevelDownButtonPressed?.Invoke();
    }

    private void Plus10Button_OnClick()
    {
        for (int i = 0; i < 10 && rocketStat.GetStatLevel() < RocketStat.MAX_STAT_LEVEL; i++)
        {
            rocketStat.LevelUp();
            OnAnyLevelUpButtonPressed?.Invoke();
        }
        UpdateCurrentLevelText();
    }

    private void Minus10Button_OnClick()
    {
        for (int i = 0; i < 10 && rocketStat.GetStatLevel() > RocketStat.MIN_STAT_LEVEL; i++)
        {
            rocketStat.LevelDown();
            OnAnyLevelDownButtonPressed?.Invoke();
        }
        UpdateCurrentLevelText();
    }

    private void UpgradeRocketMenu_OnMenuOpened()
    {
        if (RocketStatsMananger.Instance)
        {
            if (rocketStat == null)
            {
                rocketStat = RocketStatsMananger.Instance.GetRocketStat(statToControl);
            }                      
            UpdateCurrentLevelText();

            SetLevelUpButtonActiveState(RocketStatsMananger.Instance.GetCurrentStatPoints() > 0 && rocketStat.GetStatLevel() < RocketStat.MAX_STAT_LEVEL);
            SetLevelDownButtonActiveState(rocketStat.GetStatLevel() > RocketStat.MIN_STAT_LEVEL);
            SetPlus10ButtonActiveState(RocketStatsMananger.Instance.GetCurrentStatPoints() > 9 && rocketStat.GetStatLevel() < RocketStat.MAX_STAT_LEVEL - 10);
            SetMinus10ButtonActiveState(rocketStat.GetStatLevel() > RocketStat.MIN_STAT_LEVEL + 9);
        }
    }

    private void RocketStatMananger_OnCurrentStatPointsChanged(int currentStatPoints)
    {
        SetLevelUpButtonActiveState(currentStatPoints > 0 && rocketStat.GetStatLevel() < RocketStat.MAX_STAT_LEVEL);
        SetLevelDownButtonActiveState(rocketStat.GetStatLevel() > RocketStat.MIN_STAT_LEVEL);
        SetPlus10ButtonActiveState(currentStatPoints > 9 && rocketStat.GetStatLevel() < RocketStat.MAX_STAT_LEVEL - 10);
        SetMinus10ButtonActiveState(rocketStat.GetStatLevel() > RocketStat.MIN_STAT_LEVEL + 9);
    }

    private void RocketStatMananger_OnResetStatPoints()
    {
        for (int i = rocketStat.GetStatLevel(); i > RocketStat.MIN_STAT_LEVEL; i--)
        {
            rocketStat.LevelDown();
            OnAnyLevelDownButtonPressed?.Invoke();                      
        }
        UpdateCurrentLevelText();
    }

    private void UpdateCurrentLevelText()
    {
        currentLevelText.text = $"Level: {rocketStat.GetStatLevel()}";
    }

    private void SetLevelUpButtonActiveState(bool state)
    {
        if (state != levelUpButton.gameObject.activeInHierarchy)
        {
            levelUpButton.gameObject.SetActive(state);
        }       
    }

    private void SetLevelDownButtonActiveState(bool state)
    {
        if (state != levelDownButton.gameObject.activeInHierarchy)
        {
            levelDownButton.gameObject.SetActive(state);
        }       
    }

    private void SetPlus10ButtonActiveState(bool state)
    {
        plus10Button.gameObject.SetActive(state);
    }

    private void SetMinus10ButtonActiveState(bool state)
    {
        minus10Button.gameObject.SetActive(state);
    }
}
