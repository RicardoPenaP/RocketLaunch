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
    }

    private void OnDestroy()
    {
        MissionButton.OnAnyMissionButtonPressed -= MissionButton_OnAnyMissionButtonPressed;
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

    private void LoadMission(Mission mission)
    {
        currentMission = mission;
        SceneManagement.LoadScene(mission.GetGameScene());
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

    public void SetCurrentMissionCompleted()
    {
        currentMission.SetCompleted(true);
    }
}
