using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RocketLevelMananger : MonoBehaviour
{
    private const int DEFAULT_PLAYER_LEVEL = 1;
    private const float DEFAULT_CURRENT_EXPERIENCE = 0f;
    public static RocketLevelMananger Instance { get; private set; }

    [Header("Experience Mananger")]
    [SerializeField] private int maxLevel = 100;
    [SerializeField] private float baseExperienceForNextLevel = 1000f;
    [SerializeField,Range(1f,10f)] private float experienceAugmentCoeficient = 2f;

    public event Action OnRocketLevelUp;
    public event Action OnSavedExperience;
    public event Action<PlayerExperienceData> OnUpdateVisuals;

    private int currentLevel = 1;
    private float currentExperience = 0;
    private float maxExperience;

    private void Awake()
    {
        if (Instance && Instance != this)
        {
            Destroy(this);
            return;
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }

        GameDataLoader.OnLoadPlayerData += GameDataLoader_OnLoadPlayerPlayerData;
    }

    private void Start()
    {
        LevelMananger.OnLevelCompleted += LevelMananger_OnLevelCompleted;       
    }

    private void OnDestroy()
    {
        LevelMananger.OnLevelCompleted -= LevelMananger_OnLevelCompleted;
        GameDataLoader.OnLoadPlayerData -= GameDataLoader_OnLoadPlayerPlayerData;
    }

    private void LevelMananger_OnLevelCompleted(LevelMananger.RewardsData rewardsData)
    {
        SaveExperience(rewardsData.totalExperience);
    }

    private void GameDataLoader_OnLoadPlayerPlayerData(PlayerData playerData)
    {
        currentLevel = 1;
        maxExperience = baseExperienceForNextLevel;
        if (playerData != null)
        {
            currentLevel = playerData.GetLevel();
            for (int i = 0; i < currentLevel; i++)
            {
                maxExperience *= experienceAugmentCoeficient;
            }
            currentExperience = playerData.GetCurrentExperience();
        }
    }
    
    private void SaveExperience(float amount)
    {
        float targetExperience = currentExperience + amount;
        //currentExperience += amount;

        if (targetExperience >= maxExperience)
        {            
            float remainingExp = targetExperience - maxExperience;
            targetExperience = maxExperience;
            PlayerExperienceData playerExperienceData = new PlayerExperienceData(currentExperience, targetExperience, maxExperience);
            OnUpdateVisuals(playerExperienceData);
            LevelUp();
            SaveExperience(remainingExp);
            return;
        }
        else
        {
            PlayerExperienceData playerExperienceData = new PlayerExperienceData(currentExperience, targetExperience, maxExperience);
            OnUpdateVisuals(playerExperienceData);
            currentExperience = targetExperience;
        }

        OnSavedExperience?.Invoke();
    }

    private void LevelUp()
    {
        if (currentLevel < maxLevel)
        {
            currentExperience = 0;
            currentLevel++;
            maxExperience *= experienceAugmentCoeficient;            
            OnRocketLevelUp?.Invoke();            
        }
    }

    public int GetCurrentLevel()
    {
        return currentLevel;
    }

    public float GetCurrentExperience()
    {
        return currentExperience;
    }

    public static PlayerData GetNewPlayerData()
    {
        PlayerData playerData = new PlayerData(DEFAULT_PLAYER_LEVEL,DEFAULT_CURRENT_EXPERIENCE);
        return playerData;
    }
}
