using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using GameSceneManagement;

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

    [Header("Level Mananger")]
    [SerializeField] private GameScene nextLevel;
    [SerializeField,Min(0)] private float levelExperienceReward = 100f;
    [SerializeField,Range(0f, 10f)] private float maxRemainingLifesMultiplier = 3f;
    [SerializeField, Range(0f, 10f)] private float maxLandingTriesMultiplier = 3f;
    [SerializeField, Range(0f, 1f)] private float eachLandingTryCost = 0.1f;
    [SerializeField, Range(0f, 10f)] private float maxLandingScoreMultiplier = 3f;

    public static LevelMananger Instance { get; private set; }

    public event Action OnGameOver;
    public event Action<RewardsData> OnLevelCompleted;

    private PlayerController playerController;
    private PlayerLandingController playerLandingController;

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
            return;
        }
    }

    private void Start()
    {
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
    }

    private void OnDestroy()
    {
        if (playerController)
        {
            playerController.OnDie -= PlayerController_OnDie;
        }
        if (playerLandingController)
        {
            playerLandingController.OnLandingFinished += PlayerLandingController_OnLandingFinished;
        }
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
        rewardsData.landingTriesMultiplier = maxLandingTriesMultiplier - (eachLandingTryCost * landingCompleteData.landingTries) > 1f ? maxLandingTriesMultiplier - (eachLandingTryCost * landingCompleteData.landingTries) : 1f;
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

}
