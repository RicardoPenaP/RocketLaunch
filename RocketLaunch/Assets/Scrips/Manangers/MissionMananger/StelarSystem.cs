using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StelarSystem
{
    [Header("Estelar System")]
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
}
