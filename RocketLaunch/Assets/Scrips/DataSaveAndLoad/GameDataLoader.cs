using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameDataLoader : MonoBehaviour
{
    public static GameDataLoader Instance { get; private set; }

    public static event Action<PlayerData> OnLoadPlayerData;

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

        SaveAndLoadSystem.OnPlayerDataDeleted += SaveAndLoadSystem_OnPlayerDataDeleted;
        DeleteSavedDataPanel.OnDeleteAllButtonPressed += DeleteSavedDataPanel_OnDeleteAllButtonPressed;
    }

    private void Start()
    {
        LoadPlayerData();
        if (RocketLevelMananger.Instance)
        {
            RocketLevelMananger.Instance.OnSavedExperience += RocketLevelMananger_OnSavedExperience;
        }
    }

    private void OnDestroy()
    {
        SaveAndLoadSystem.OnPlayerDataDeleted -= SaveAndLoadSystem_OnPlayerDataDeleted;
        DeleteSavedDataPanel.OnDeleteAllButtonPressed -= DeleteSavedDataPanel_OnDeleteAllButtonPressed;
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

    private void RocketLevelMananger_OnSavedExperience()
    {
        SavePlayerData();
    }

    private void SaveAndLoadSystem_OnPlayerDataDeleted()
    {
        LoadPlayerData();
    }

    private void DeleteSavedDataPanel_OnDeleteAllButtonPressed()
    {       
        SaveAndLoadSystem.DeletePlayerData();
    }
}
