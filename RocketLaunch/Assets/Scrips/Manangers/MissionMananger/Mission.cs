using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Mission 
{
    [SerializeField] private int missionIndex;
    [SerializeField] private bool unlocked = false;
    [SerializeField] private bool completed = false;
}
