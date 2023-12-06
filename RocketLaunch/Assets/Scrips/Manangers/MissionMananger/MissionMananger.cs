using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameSceneManagement;

public class MissionMananger : MonoBehaviour
{
    public static MissionMananger Instance { get; private set; }

    [Header("Mission Mananger")]
    [SerializeField] StelarSystem[] stelarSystems;

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
    }

    private void Start()
    {
        MissionButton.OnAnyMissionButtonPressed += MissionButton_OnAnyMissionButtonPressed;
        LevelMananger.OnAnyLevelCompleted += LevelMananger_OnAnyLevelCompleted;
        LevelMananger.OnGoToNextLevel += LevelMananager_OnGoToNextLevel;
    }

    private void OnDestroy()
    {
        MissionButton.OnAnyMissionButtonPressed -= MissionButton_OnAnyMissionButtonPressed;
        LevelMananger.OnAnyLevelCompleted -= LevelMananger_OnAnyLevelCompleted;
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
        Mission lastMission = missions[missions.Length - 1];
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

    private void LevelMananger_OnAnyLevelCompleted()
    {
        SetCurrentMissionCompleted();
        UnlockNextMission();
    }

    private void LevelMananager_OnGoToNextLevel()
    {
        LoadNextMission();
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

  
}
