using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameSceneManagement;
using System;

public class MissionMananger : MonoBehaviour
{
    public const GameScene LAST_MISSION = GameScene.Level3_10;
    public static MissionMananger Instance { get; private set; }

    [Header("Mission Mananger")]
    [SerializeField] StelarSystemData stelarSystemData;

    public event Action OnMissionCompleted;

    private StelarSystem[] stelarSystems;

    private Mission currentMission;
    private Mission nextMission;

    private void Awake()
    {
        if (Instance && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        GameDataLoader.OnLoadMissionsData += GameDataLoader_OnLoadMissionsData;
    }

    private void Start()
    {
        MissionButton.OnAnyMissionButtonPressed += MissionButton_OnAnyMissionButtonPressed;
        LevelMananger.OnLevelCompleted += LevelMananger_OnLevelCompleted;
        LevelMananger.OnGoToNextLevel += LevelMananager_OnGoToNextLevel;
    }

    private void OnDestroy()
    {
        GameDataLoader.OnLoadMissionsData -= GameDataLoader_OnLoadMissionsData;
        MissionButton.OnAnyMissionButtonPressed -= MissionButton_OnAnyMissionButtonPressed;
        LevelMananger.OnLevelCompleted -= LevelMananger_OnLevelCompleted;
        LevelMananger.OnGoToNextLevel -= LevelMananager_OnGoToNextLevel;
    }

    private void LoadMission(Mission mission)
    {
        currentMission = mission;
        SceneManagement.LoadScene(mission.GetGameScene());
    }

    private void LoadNextMission()
    {
        LoadMission(nextMission);
        nextMission = null;
    }

    private void SetCurrentMissionCompleted()
    {
        currentMission.SetCompleted(true);
    }

    private void UnlockNextMission()
    {       
        if (IsTheLastMission(currentMission))
        {
            return;
        }

        for (int i = 0; i < stelarSystems.Length; i++)
        {
            Mission[] missions = stelarSystems[i].GetMissions();
            for (int j = 0; j < missions.Length; j++)
            {
                if (missions[j] == currentMission)
                {
                    if (j < missions.Length - 1)
                    {
                        nextMission = missions[j + 1];
                        nextMission.SetLocked(false);
                    }
                    else
                    {
                        if (j == missions.Length - 1 && i < stelarSystems.Length - 1)
                        {
                            stelarSystems[i + 1].SetLocked(false);                            
                            nextMission = stelarSystems[i + 1].GetMissions()[0];
                            nextMission.SetLocked(false);
                        }
                    }
                    return;
                }
            }
        }
    }

    private bool IsTheLastMission(Mission currentMission)
    {
        Mission[] missions = stelarSystems[stelarSystems.Length - 1].GetMissions();
        if (missions.Length < 1)
        {
            return false;
        }
        Mission lastMission = missions[missions.Length - 1];
        if (lastMission == null)
        {
            return false;
        }
        return currentMission == lastMission;
    }

    private void MissionButton_OnAnyMissionButtonPressed(StelarSystemID stelarSystemID, int missionIndex)
    {
        foreach (StelarSystem stelarSystem in stelarSystems)
        {
            if (stelarSystem.GetStelarSystemID() == stelarSystemID)
            {
                foreach (Mission mission in stelarSystem.GetMissions())
                {
                    if (mission.GetMissionIndex() == missionIndex)
                    {
                        LoadMission(mission);
                    }
                }
            }
        }
    }

    private void LevelMananger_OnLevelCompleted(LevelMananger.RewardsData rewardsData)
    {
        SetCurrentMissionCompleted();
        UnlockNextMission();
        OnMissionCompleted?.Invoke();
    }

    private void LevelMananager_OnGoToNextLevel()
    {
        LoadNextMission();
    }

    private void GameDataLoader_OnLoadMissionsData(MissionsData missionsData)
    {
        //stelarSystems = missionsData.GetStelarSystems();
        StelarSystem[] savedStelarSystems = missionsData.GetStelarSystems();
        StelarSystem[] defaultStelarSystems = stelarSystemData.GetStelarSystems();

        if (savedStelarSystems.Length == defaultStelarSystems.Length)
        {
            for (int i = 0; i < savedStelarSystems.Length; i++)
            {
                if (savedStelarSystems[i].GetMissions().Length != defaultStelarSystems[i].GetMissions().Length)
                {
                    Mission[] savedMissions = savedStelarSystems[i].GetMissions();
                    Mission[] newMissions = defaultStelarSystems[i].GetMissions();
                    for (int j = 0; j < newMissions.Length; j++)
                    {
                        if (j < savedMissions.Length)
                        {
                            newMissions[j] = savedMissions[j];
                        }
                    }

                    savedStelarSystems[i].SetMissions(newMissions);
                }
            }
        }

        stelarSystems = savedStelarSystems;


    }

    public StelarSystem GetStelarSystem(StelarSystemID stelarSystemID)
    {
        foreach (StelarSystem stelarSystem in stelarSystems)
        {
            if (stelarSystem.GetStelarSystemID() == stelarSystemID)
            {
                return stelarSystem;
            }
        }
        return null;
    }

    public StelarSystem[] GetStelarSystems()
    {
        return stelarSystems;
    }

    public MissionsData GetNewMissionsData()
    {        
        StelarSystem[] stelarSystems = stelarSystemData.GetStelarSystems();

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

        return missionsData;
    }

  
}
