using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using GameSceneManagement;


public class UpgradeRocketMenu : Menu<UpgradeRocketMenu>
{
    public static event Action OnSaveTheStatsData;
    public static event Action OnUpgradeRocketMenuOpened;

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

        if (LevelCompletedMenu.Instance)
        {
            LevelCompletedMenu.Instance.OnUpgradeRocketButtonPressed += LevelCompletedMenu_OnUpgradeRocketButtonPressed;
        }

        gameObject.SetActive(false);
        menuOpened = false;
    }

    protected override void OnDestroy()
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

        if (LevelCompletedMenu.Instance)
        {
            LevelCompletedMenu.Instance.OnUpgradeRocketButtonPressed -= LevelCompletedMenu_OnUpgradeRocketButtonPressed;
        }

        base.OnDestroy();
    }

    protected override void OpenMenu(Action onOpenAnimationEndedActions = null)
    {
        OnUpgradeRocketMenuOpened?.Invoke();
        if (RocketLevelMananger.Instance)
        {
            levelText.text = $"Level: {RocketLevelMananger.Instance.GetCurrentLevel()}";
        }

        if (RocketStatsMananger.Instance)
        {
            remaningStatPointsText.text = $"Remaining Points: {RocketStatsMananger.Instance.GetCurrentStatPoints()}";
        }

        if (SceneManagement.GetCurrentScene() != GameScene.MainMenu && TransitionFade.Instance)
        {
            TransitionFade.Instance.FadeIn();
        }
        base.OpenMenu(onOpenAnimationEndedActions);
    }

    protected override void CloseMenu(Action onCloseAnimationEndedActions = null)
    {
        if (SceneManagement.GetCurrentScene() != GameScene.MainMenu && TransitionFade.Instance)
        {
            TransitionFade.Instance.FadeOut();
        }
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
        OpenMenu();
    }

    private void RocketStatMananger_OnCurrentStatPointsChanged(int currentStatPoints)
    {
        remaningStatPointsText.text = $"Remaining Points: {currentStatPoints}";
    }

    private void LevelCompletedMenu_OnUpgradeRocketButtonPressed()
    {
        OpenMenu();
    }
}
