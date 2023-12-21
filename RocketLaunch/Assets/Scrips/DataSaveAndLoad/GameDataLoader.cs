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
        int availableStatPoints = RocketStatsMananger.Instance.GetCurrentStatPoints();
        RocketStat[] rocketStats = RocketStatsMananger.Instance.GetRocketStats();
        int[] rocketStatsLevel = new int[rocketStats.Length];

        for (int i = 0; i < rocketStats.Length; i++)
        {
            rocketStatsLevel[i] = rocketStats[i].GetStatLevel();
        }

        PlayerData playerData = new PlayerData(level,availableStatPoints,rocketStatsLevel);

        SaveAndLoadSystem.SavePlayerData(playerData);
    }
    
}
