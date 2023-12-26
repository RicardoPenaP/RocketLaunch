using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;

public static class SaveAndLoadSystem
{
    private const int DEFAULT_PLAYER_LEVEL = 1;
    private const float DEFAULT_CURRENT_EXPERIENCE = 0f;

    private static readonly string playerDataPath = Application.persistentDataPath +"/player.data";
    private static readonly string statsDataPath = Application.persistentDataPath + "/stats.data";

    public static event Action OnPlayerDataDeleted;
    public static void SavePlayerData(PlayerData playerData)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(playerDataPath, FileMode.Create);

        formatter.Serialize(stream, playerData);
        stream.Close();
    }

    public static PlayerData LoadPlayerData()
    {
        if (File.Exists(playerDataPath))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(playerDataPath, FileMode.Open);

            PlayerData playerData = formatter.Deserialize(stream) as PlayerData;
            stream.Close();

            return playerData;            
        }
        else
        {
            CreateNewPlayerDataSaveFile();
            return LoadPlayerData();
        }
    }

    public static void DeletePlayerData()
    {
        Debug.Log("Deleted data");
        CreateNewPlayerDataSaveFile();
        OnPlayerDataDeleted?.Invoke();
    }

    private static void CreateNewPlayerDataSaveFile()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(playerDataPath, FileMode.Create);
        PlayerData playerData = new PlayerData(DEFAULT_PLAYER_LEVEL, DEFAULT_CURRENT_EXPERIENCE);

        formatter.Serialize(stream, playerData);
        stream.Close();
    }

    public static void SavePlayerStats(StatsData statsData)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(statsDataPath, FileMode.Create); 
        formatter.Serialize(stream, statsData);
        stream.Close();
    }

    public static StatsData LoadStatsData()
    {
        if (File.Exists(statsDataPath))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(statsDataPath, FileMode.Open);

            StatsData statsData = formatter.Deserialize(stream) as StatsData;
            stream.Close();

            return statsData;
        }
        else
        {
            CreateNewStatDataSaveFile();
            return LoadStatsData();
        }
    }



    private static void CreateNewStatDataSaveFile()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(statsDataPath, FileMode.Create);
        StatsData statsData = new StatsData();

        formatter.Serialize(stream, statsData);
        stream.Close();
    }

}
