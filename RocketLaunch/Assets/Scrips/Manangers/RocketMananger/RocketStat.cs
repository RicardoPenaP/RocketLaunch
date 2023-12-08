using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketStat 
{
    private const int MAX_STAT_LEVEL = 100;
    private const int MIN_STAT_LEVEL = 1;

    public StatType statType;
    public int statLevel;

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

    
}
