using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StelarSystemID { StelarSystem1 = 1, StelarSystem2, StelarSystem3}
[System.Serializable]
public class StelarSystem
{
    [Header("Estelar System")]
    [SerializeField] private StelarSystemID stelarSystemID;
    [SerializeField] private string systemName;
    [SerializeField] private Mission[] missions;
    [SerializeField] private bool unlocked = false;
    [SerializeField] private bool completed = false;

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

    public StelarSystemID GetStelarSystemID()
    {
        return stelarSystemID;
    }

    public Mission[] GetMissions()
    {
        return missions;
    }

    public string GetSystemName()
    {
        return systemName;
    }
}
