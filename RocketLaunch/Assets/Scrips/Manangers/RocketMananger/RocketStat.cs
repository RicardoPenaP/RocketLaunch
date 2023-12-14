using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class RocketStat 
{
    public const int MAX_STAT_LEVEL = 100;
    public const int MIN_STAT_LEVEL = 1;
    //for testing
    [SerializeField]private StatType statType;
    [SerializeField]private int statLevel;

    public RocketStat(StatType statType)
    {
        this.statType = statType;
        statLevel = 1;
    }

    public void LevelUp()
    {
        if (statLevel >= MAX_STAT_LEVEL)
        {
            return;
        }

        statLevel++;
    }

    public void LevelDown()
    {
        if (statLevel <= MIN_STAT_LEVEL)
        {
            return;
        }

        statLevel--;
    }

    public void SetStatType(StatType statType)
    {
        this.statType = statType;
    }

    public StatType GetStatType()
    {
        return statType;
    }

    public int GetStatLevel()
    {
        return statLevel;
    }

    
}
