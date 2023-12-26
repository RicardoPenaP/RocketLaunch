using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameDataLoader : MonoBehaviour
{
    public static GameDataLoader Instance { get; private set; }

    public static event Action<PlayerData> OnLoadPlayerData;
    public static event Action<StatsData> OnLoadStatsData;
    
    private void Awake()
    {
        if (Instance && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        DeleteSavedDataPanel.OnDeleteAllButtonPressed += DeleteSavedDataPanel_OnDeleteAllButtonPressed;
        UpgradeRocketMenu.OnSaveTheStatsData += UpgradeRocketMenu_OnSaveTheStatsData;
    }

    private void Start()
    {
        LoadPlayerData();
        LoadStatData();

        if (RocketLevelMananger.Instance)
        {
            RocketLevelMananger.Instance.OnSavedExperience += RocketLevelMananger_OnSavedExperience;
        }
    }

    private void OnDestroy()
    {
       
        DeleteSavedDataPanel.OnDeleteAllButtonPressed -= DeleteSavedDataPanel_OnDeleteAllButtonPressed;
        UpgradeRocketMenu.OnSaveTheStatsData -= UpgradeRocketMenu_OnSaveTheStatsData;
    }

    private void LoadPlayerData()
    {
        OnLoadPlayerData?.Invoke(SaveAndLoadSystem.LoadPlayerData());
    }

    private void SavePlayerData()
    {
        int level = RocketLevelMananger.Instance.GetCurrentLevel();
        float currentExperience = RocketLevelMananger.Instance.GetCurrentExperience();

        PlayerData playerData = new PlayerData(level,currentExperience);

        SaveAndLoadSystem.SavePlayerData(playerData);
    }

    private void LoadStatData()
    {
        OnLoadStatsData?.Invoke(SaveAndLoadSystem.LoadStatsData());
    }

    private void SaveStatData()
    {
        int currentStatsPoints = RocketStatsMananger.Instance.GetCurrentStatPoints();
        RocketStat[] rocketStats = RocketStatsMananger.Instance.GetRocketStats();

        StatsData statsData = new StatsData(currentStatsPoints, rocketStats);
        SaveAndLoadSystem.SaveStatsData(statsData);
    }

    private void RocketLevelMananger_OnSavedExperience()
    {
        SavePlayerData();
        SaveStatData();
    }

    private void DeleteSavedDataPanel_OnDeleteAllButtonPressed()
    {       
        SaveAndLoadSystem.DeleteSavedData();
        ReloadAllSavedData();
    }

    private void UpgradeRocketMenu_OnSaveTheStatsData()
    {
        SaveStatData();
    }

    private void ReloadAllSavedData()
    {
        LoadPlayerData();
        LoadStatData();
    }
    
}
