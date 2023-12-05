using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameSceneManagement;

[System.Serializable]
public class Mission 
{
    [Header("Mission")]
    [SerializeField] private GameScene gameScene;
    [SerializeField] private int missionIndex;
    [SerializeField] private bool unlocked = false;
    [SerializeField] private bool completed = false;

    public GameScene GetGameScene()
    {
        return gameScene;
    }

    public int GetMissionIndex()
    {
        return missionIndex;
    }

    public void SetUnlocked(bool state)
    {
        unlocked = state;
    }

    public bool GetUnlocked()
    {
        return unlocked;
    }

    public void SetCompleted(bool state)
    {
        completed = state;
    }

    public bool GetCompleted()
    {
        return completed;
    }
}
