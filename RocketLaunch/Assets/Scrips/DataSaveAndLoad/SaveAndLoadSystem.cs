using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;

public static class SaveAndLoadSystem
{
    private static readonly string playerDataPath = Application.persistentDataPath +"/player.data";
    private static readonly string statsDataPath = Application.persistentDataPath + "/stats.data";
    private static readonly string missionsDataPath = Application.persistentDataPath + "/missions.data";

    public static void DeleteSavedData()
    {        
        CreateNewPlayerDataSaveFile();
        CreateNewStatDataSaveFile();
        CreateNewMissionsDataSaveFile();
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
        SavePlayerData(RocketLevelMananger.GetNewPlayerData());
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
        SaveStatsData(RocketStatsMananger.GetNewStatsData());
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
