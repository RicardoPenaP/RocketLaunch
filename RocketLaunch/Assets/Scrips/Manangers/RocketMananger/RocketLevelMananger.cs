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
    }

    private void Start()
    {
        LevelMananger.OnLevelCompleted += LevelMananger_OnLevelCompleted;
        if (GameDataLoader.Instance)
        {
            GameDataLoader.Instance.OnLoadPlayerData += GameDataLoader_OnLoadPlayerPlayerData;
        }
    }

    private void OnDestroy()
    {
        LevelMananger.OnLevelCompleted -= LevelMananger_OnLevelCompleted;
    }

    private void LevelMananger_OnLevelCompleted(LevelMananger.RewardsData rewardsData)
    {
        SaveExperience(rewardsData.totalExperience);
    }

    private void GameDataLoader_OnLoadPlayerPlayerData(PlayerData playerData)
    {
        if (playerData != null)
        {
            currentLevel = playerData.GetLevel();
            for (int i = 0; i < currentLevel; i++)
            {
                maxExperience *= experienceAugmentCoeficient;
            }
            currentExperience = playerData.GetCurrentExperience();
        }
        else
        {
            currentLevel = 1;
            maxExperience = baseExperienceForNextLevel;
        }
    }
    
    private void SaveExperience(float amount)
    {
        currentExperience += amount;

        if (currentExperience > maxExperience)
        {
            float remainingExp = currentExperience - maxExperience;
            LevelUp();
            SaveExperience(remainingExp);
            return;
        }

        if (currentExperience == maxExperience)
        {
            LevelUp();
        }
    }

    private void LevelUp()
    {
        if (currentLevel < maxLevel)
        {
            currentLevel++;
            maxExperience *= experienceAugmentCoeficient;
            currentExperience = 0;
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
