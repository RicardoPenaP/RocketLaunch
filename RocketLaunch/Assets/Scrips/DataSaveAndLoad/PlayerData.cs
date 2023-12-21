using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class PlayerData 
{
    private int level;
    private int availableSkillPoints;
    private int[] eachStatLevel;

    public PlayerData(int level, int availableSkillPoints, int[] eachStatLevel)
    {
        this.level = level;
        this.availableSkillPoints = availableSkillPoints;
        this.eachStatLevel = eachStatLevel;
    }

    public int GetLevel()
    {
        return level;
    }

    public int GetAvailableSkillPoints()
    {
        return availableSkillPoints;
    }

    public int[] GetEachStatLevel()
    {
        return eachStatLevel;
    }
}
