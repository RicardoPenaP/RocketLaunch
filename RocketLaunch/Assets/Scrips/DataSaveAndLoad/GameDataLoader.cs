using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameDataLoader : MonoBehaviour
{
    public static GameDataLoader Instance { get; private set; }

    public event Action<PlayerData> OnLoadPlayerData;


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

        LoadPlayerData();
    }

    private void LoadPlayerData()
    {
        OnLoadPlayerData?.Invoke(SaveAndLoadSystem.LoadPlayerData());
    }

    public void SavePlayerData()
    {
        int level = RocketLevelMananger.Instance.GetCurrentLevel();
        float currentExperience = RocketLevelMananger.Instance.GetCurrentExperience();

        PlayerData playerData = new PlayerData(level,currentExperience);

        SaveAndLoadSystem.SavePlayerData(playerData);
    }
    
}
