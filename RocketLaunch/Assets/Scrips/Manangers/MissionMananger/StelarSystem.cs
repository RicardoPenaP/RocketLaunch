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
    [SerializeField] private bool locked = true;
    [SerializeField] private bool completed = false;

    public void SetLocked(bool state)
    {
        locked = state;
    }

    public bool GetLocked()
    {
        return locked;
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

    public void SetMissions(Mission[] missions)
    {
        this.missions = missions;
    }

    public string GetSystemName()
    {
        return systemName;
    }
}
