using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LevelMananger : MonoBehaviour
{
    public struct RewardsData
    {
        public float partialExperiece;
        public float totalExperience;
        public float lifesMultiplier;
        public float landingTriesMultiplier;
        public float landingScoreMultiplier;
    }
    
    public static event Action OnGoToNextLevel;
    public static event Action<RewardsData> OnLevelCompleted;

    [Header("Level Mananger")]    
    [SerializeField,Min(0)] private float levelExperienceReward = 100f;
    [SerializeField,Range(0f, 10f)] private float maxRemainingLifesMultiplier = 3f;
    [SerializeField, Range(0f, 10f)] private float maxLandingTriesMultiplier = 3f;
    [SerializeField, Range(0f, 1f)] private float eachLandingTryCost = 0.1f;
    [SerializeField, Range(0f, 10f)] private float maxLandingScoreMultiplier = 3f;

    public static LevelMananger Instance { get; private set; }

    public event Action OnGameOver;

    private PlayerController playerController;
    private PlayerLandingController playerLandingController;

    private float levelExperienceRewardMultiplier;

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
        else
        {
            if (Instance != this)
            {
                Destroy(this);
                return;
            }
        }
    }

    private void Start()
    {
        //For testing only
        Application.targetFrameRate = 60;
        
        playerController = FindAnyObjectByType<PlayerController>();
        playerLandingController = playerController.GetComponent<PlayerLandingController>();

        if (playerController)
        {
            playerController.OnDie += PlayerController_OnDie;
        }
        if (playerLandingController)
        {
            playerLandingController.OnLandingFinished += PlayerLandingController_OnLandingFinished;
        }
        if (LevelCompletedMenu.Instance)
        {
            LevelCompletedMenu.Instance.OnNextLevelButtonPressed += LevelCompletedMenu_OnNextLevelButtonPressed;
        }

        Initialize();
    }

    private void OnDestroy()
    {
        if (playerController)
        {
            playerController.OnDie -= PlayerController_OnDie;
        }
        if (playerLandingController)
        {
            playerLandingController.OnLandingFinished -= PlayerLandingController_OnLandingFinished;
        }
        if (LevelCompletedMenu.Instance)
        {
            LevelCompletedMenu.Instance.OnNextLevelButtonPressed -= LevelCompletedMenu_OnNextLevelButtonPressed;
        }
    }

    private void Initialize()
    {
        levelExperienceRewardMultiplier = 1f;
        if (RocketStatsMananger.Instance)
        {
            int rocketStatLevel = RocketStatsMananger.Instance.GetRocketStat(StatType.PickupSystem).GetStatLevel();
            float rocketStatMultiplierAugmentCoeficient = RocketStatsMananger.Instance.GetExperienceMultiplierAugmentCoeficient();
            levelExperienceRewardMultiplier += rocketStatMultiplierAugmentCoeficient * rocketStatLevel;            
        }
        levelExperienceReward *= levelExperienceRewardMultiplier;
    }

    private void PlayerController_OnDie(object sender, EventArgs e)
    {
        OnGameOver?.Invoke();
    }

    private void PlayerLandingController_OnLandingFinished(object sender, EventArgs e)
    {
        PlayerLandingController.LandingCompleteData landingCompleteData = (e as PlayerLandingController.LandingCompleteData);
        RewardsData rewardsData = new RewardsData();
        rewardsData.partialExperiece = levelExperienceReward;
        rewardsData.lifesMultiplier = maxRemainingLifesMultiplier * landingCompleteData.normalizedAmountOfRemaningLifes;
        rewardsData.landingTriesMultiplier = maxLandingTriesMultiplier - (eachLandingTryCost * (landingCompleteData.landingTries - 1)) > 1f ? maxLandingTriesMultiplier - (eachLandingTryCost * (landingCompleteData.landingTries-1)) : 1f;
        rewardsData.landingScoreMultiplier = maxLandingScoreMultiplier * landingCompleteData.normalizedLandingScore;
        rewardsData.totalExperience = CalculateTotalExperience(rewardsData);
        OnLevelCompleted?.Invoke(rewardsData);        
    }

    private float CalculateTotalExperience(RewardsData rewardsData)
    {
        float total = 0;
        total += rewardsData.partialExperiece * rewardsData.lifesMultiplier;
        total += rewardsData.partialExperiece * rewardsData.landingTriesMultiplier;
        total += rewardsData.partialExperiece * rewardsData.landingScoreMultiplier;
        return total;
    }

    private void LevelCompletedMenu_OnNextLevelButtonPressed()
    {
        OnGoToNextLevel?.Invoke();
    }

}
