using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class PlayerData 
{
    private int level;
    private float currentExperience;

    public PlayerData(int level, float currentExperience)
    {
        this.level = level;
        this.currentExperience = currentExperience;      
    }

    public int GetLevel()
    {
        return level;
    }

    public float GetCurrentExperience()
    {
        return currentExperience;
    }

}
