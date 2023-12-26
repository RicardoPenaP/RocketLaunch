using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StatsData 
{
    private int currentStatsPoints;
    private RocketStat[] rocketStats;

    public StatsData(int currentStatsPoints, RocketStat[] rocketStats)
    {
        this.currentStatsPoints = currentStatsPoints;
        this.rocketStats = rocketStats;
    }

    public int GetCurrentStatsPoints()
    {
        return currentStatsPoints;
    }

    public RocketStat[] GetRocketStats()
    {
        return rocketStats;
    }

    
}
