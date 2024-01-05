using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameSceneManagement;
using TMPro;
using System;

public class LevelCompletedMenu : Menu<LevelCompletedMenu>
{
    [Header("Level Completed Menu")]
    [Header("Buttons references")]
    [SerializeField] private Button playAgainButton;
    [SerializeField] private Button nextLevelButton;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button upgradeRocketButton;
    
    [Header("Text references")]
    [SerializeField] private TextMeshProUGUI expecienceAmountText;
    [SerializeField] private TextMeshProUGUI lifesRemainingAmountText;
    [SerializeField] private TextMeshProUGUI landingTriesAmountText;
    [SerializeField] private TextMeshProUGUI landingScoreAmountText;
    [SerializeField] private TextMeshProUGUI totalAmountText;

    public event Action OnNextLevelButtonPressed;
    public event Action OnUpgradeRocketButtonPressed;

    protected override void Awake()
    {
        base.Awake();

        if (playAgainButton)
        {
            playAgainButton.onClick.AddListener(PlayAgainButton_OnClick);
        }

        if (nextLevelButton)
        {
            nextLevelButton.onClick.AddListener(NextLevelButton_OnClick);
        }

        if (mainMenuButton)
        {
            mainMenuButton.onClick.AddListener(MainMenuButton_OnClick);
        }
        
        if (upgradeRocketButton)
        {
            upgradeRocketButton.onClick.AddListener(UpgradeRocketButton_OnClick);
        }
    }

    private void Start()
    {
        LevelMananger.OnLevelCompleted += LevelMananger_OnLevelCompleted;

        gameObject.SetActive(false);
        menuOpened = false;
    }

    protected override void OnDestroy()
    {
        LevelMananger.OnLevelCompleted -= LevelMananger_OnLevelCompleted;

        if (playAgainButton)
        {
            playAgainButton.onClick.RemoveListener(PlayAgainButton_OnClick);
        }

        if (nextLevelButton)
        {
            nextLevelButton.onClick.RemoveListener(NextLevelButton_OnClick);
        }

        if (mainMenuButton)
        {
            mainMenuButton.onClick.RemoveListener(MainMenuButton_OnClick);
        }

        if (upgradeRocketButton)
        {
            upgradeRocketButton.onClick.RemoveListener(UpgradeRocketButton_OnClick);
        }

        base.OnDestroy();
    }

    private void PlayAgainButton_OnClick()
    {
        CloseMenu(PlayAgain);
    }

    private void NextLevelButton_OnClick()
    {
        OnNextLevelButtonPressed?.Invoke();
    }

    private void MainMenuButton_OnClick()
    {
        CloseMenu(LoadMainMenuScene);
    }
    private void UpgradeRocketButton_OnClick()
    {
        OnUpgradeRocketButtonPressed?.Invoke();
    }

    private void LoadMainMenuScene()
    {
        SceneManagement.LoadScene(GameScene.MainMenu);
    }

    private void PlayAgain()
    {
        SceneManagement.ReloadCurrentScene();
    }

    private void LevelMananger_OnLevelCompleted(LevelMananger.RewardsData rewardsData)
    {
        SetLevelRewards(rewardsData);        
        OpenMenu();
    }

    private void SetLevelRewards(LevelMananger.RewardsData rewardsData)
    {
        expecienceAmountText.text = $"{ rewardsData.partialExperiece}";
        lifesRemainingAmountText.text = $"x{ rewardsData.lifesMultiplier.ToString("0.00")}";
        landingTriesAmountText.text = $"x{ rewardsData.landingTriesMultiplier.ToString("0.00")}";
        landingScoreAmountText.text = $"x{ rewardsData.landingScoreMultiplier.ToString("0.00")}";
        totalAmountText.text = $"{rewardsData.totalExperience}";
    }
}
