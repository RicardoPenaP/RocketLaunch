using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameSceneManagement;

[System.Serializable]
public class Mission 
{
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

    public bool GetUnlocked()
    {
        return unlocked;
    }

    public bool GetCompleted()
    {
        return completed;
    }
}
