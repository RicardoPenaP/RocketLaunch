using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameDataLoader : MonoBehaviour
{
    public static GameDataLoader Instance { get; private set; }

    public static event Action<PlayerData> OnLoadPlayerData;
    public static event Action<StatsData> OnLoadStatsData;
    public static event Action<MissionsData> OnLoadMissionsData;
    
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
        LoadAllSavedData();

        if (RocketLevelMananger.Instance)
        {
            RocketLevelMananger.Instance.OnSavedExperience += RocketLevelMananger_OnSavedExperience;
        }

        if (MissionMananger.Instance)
        {
            MissionMananger.Instance.OnMissionCompleted += MissionMananger_OnMissionCompleted;
        }
    }

    private void OnDestroy()
    {       
        DeleteSavedDataPanel.OnDeleteAllButtonPressed -= DeleteSavedDataPanel_OnDeleteAllButtonPressed;
        UpgradeRocketMenu.OnSaveTheStatsData -= UpgradeRocketMenu_OnSaveTheStatsData;

        if (RocketLevelMananger.Instance)
        {
            RocketLevelMananger.Instance.OnSavedExperience += RocketLevelMananger_OnSavedExperience;
        }

        if (MissionMananger.Instance)
        {
            MissionMananger.Instance.OnMissionCompleted -= MissionMananger_OnMissionCompleted;
        }
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

    private void LoadMissionsData()
    {
        OnLoadMissionsData?.Invoke(SaveAndLoadSystem.LoadMissionsData()); 
    }

    private void SaveMissionsData()
    {       
        MissionsData missionsData = new MissionsData(MissionMananger.Instance.GetStelarSystems());
        SaveAndLoadSystem.SaveMissionData(missionsData);
    }

    private void RocketLevelMananger_OnSavedExperience()
    {
        SavePlayerData();
        SaveStatData();
    }

    private void DeleteSavedDataPanel_OnDeleteAllButtonPressed()
    {       
        SaveAndLoadSystem.DeleteSavedData();
        LoadAllSavedData();
    }

    private void UpgradeRocketMenu_OnSaveTheStatsData()
    {
        SaveStatData();
    }

    private void MissionMananger_OnMissionCompleted()
    {
        SaveMissionsData();
    }

    private void LoadAllSavedData()
    {
        LoadPlayerData();
        LoadStatData();
        LoadMissionsData();
    }
    
}
