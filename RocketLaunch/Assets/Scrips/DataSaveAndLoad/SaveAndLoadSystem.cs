using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;

public static class SaveAndLoadSystem
{
    private const int DEFAULT_PLAYER_LEVEL = 1;
    private const float DEFAULT_CURRENT_EXPERIENCE = 0f;
    private const int DEFAULT_STATS_AMOUNT = 6;

    private static readonly string playerDataPath = Application.persistentDataPath +"/player.data";
    private static readonly string statsDataPath = Application.persistentDataPath + "/stats.data";
    private static readonly string missionsDataPath = Application.persistentDataPath + "/missions.data";

    public static void DeleteSavedData()
    {
        Debug.Log("Deleted data");
        CreateNewPlayerDataSaveFile();
        CreateNewStatDataSaveFile();

    }

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

    private static void CreateNewPlayerDataSaveFile()
    {
        PlayerData playerData = new PlayerData(DEFAULT_PLAYER_LEVEL, DEFAULT_CURRENT_EXPERIENCE);
        SavePlayerData(playerData);
    }

    public static void SaveStatsData(StatsData statsData)
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
        RocketStat[] rocketStats = new RocketStat[DEFAULT_STATS_AMOUNT];
        for (int i = 0; i < rocketStats.Length; i++)
        {
            rocketStats[i] = new RocketStat((StatType)i);
        }
        StatsData statsData = new StatsData(0, rocketStats);
        SaveStatsData(statsData);
    }

    public static void SaveMissionData(MissionsData missionsData)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(missionsDataPath, FileMode.Create);
        formatter.Serialize(stream, missionsData);
        stream.Close();
    }

    public static MissionsData LoadMissionsData()
    {
        if (File.Exists(missionsDataPath))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(missionsDataPath, FileMode.Open);

            MissionsData missionsData = formatter.Deserialize(stream) as MissionsData;
            stream.Close();

            return missionsData;
        }
        else
        {
            CreateNewMissionsDataSaveFile();
            return LoadMissionsData();
        }
    }

    private static void CreateNewMissionsDataSaveFile()
    {
        StelarSystem[] stelarSystems = MissionMananger.Instance.GetStelarSystems();

        foreach (StelarSystem stelarSystem in stelarSystems)
        {
            foreach (Mission mission in stelarSystem.GetMissions())
            {
                mission.SetCompleted(false);
                mission.SetLocked(true);
            }
            stelarSystem.SetCompleted(false);
            stelarSystem.SetLocked(true);
        }

        stelarSystems[0].SetLocked(false);
        stelarSystems[0].GetMissions()[0].SetLocked(false);

        MissionsData missionsData = new MissionsData(stelarSystems);
        SaveMissionData(missionsData);
    }

}
