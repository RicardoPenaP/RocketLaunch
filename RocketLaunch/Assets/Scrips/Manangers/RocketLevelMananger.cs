using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketLevelMananger : MonoBehaviour
{
    public static RocketLevelMananger Instance { get; private set; }

    [Header("Experience Mananger")]
    [SerializeField] private int maxLevel = 100;
    [SerializeField] private float baseExperienceForNextLevel = 1000f;
    [SerializeField,Range(1f,10f)] private float experienceAugmentCoeficient = 2f;

    private int currentLevel = 1;
    private float currentExperience = 0;

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
    }

    private void OnDestroy()
    {
        LevelMananger.OnLevelCompleted -= LevelMananger_OnLevelCompleted;
    }

    private void LevelMananger_OnLevelCompleted(LevelMananger.RewardsData rewardsData)
    {
        SaveExperience(rewardsData.totalExperience);
    }
    
    private void SaveExperience(float amount)
    {

    }

}
