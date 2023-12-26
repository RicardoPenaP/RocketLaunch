using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RocketLevelMananger : MonoBehaviour
{
    public static RocketLevelMananger Instance { get; private set; }

    [Header("Experience Mananger")]
    [SerializeField] private int maxLevel = 100;
    [SerializeField] private float baseExperienceForNextLevel = 1000f;
    [SerializeField,Range(1f,10f)] private float experienceAugmentCoeficient = 2f;

    public event Action OnRocketLevelUp;
    public event Action OnSavedExperience;

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
        currentExperience += amount;

        if (currentExperience >= maxExperience)
        {
            float remainingExp = currentExperience - maxExperience;
            currentExperience = 0;
            LevelUp();
            SaveExperience(remainingExp);
            return;
        }

        OnSavedExperience?.Invoke();
    }

    private void LevelUp()
    {
        if (currentLevel < maxLevel)
        {
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
}
