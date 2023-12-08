using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StatType { MainEngine, SideEngine, CoolingSystem, LandingSystem, PickupSystem, ShieldSystem }
public class RocketStatsMananger : MonoBehaviour
{
    public const int STATS_AMOUNT = 6;
   
    public static RocketStatsMananger Instance { get; private set; }

    [Header("Rocket Stats Mananger")]
    [SerializeField] private int statsPointsGivenPerLevelUp = 6;

    private int totalStatPoints = 0;
    private int currentStatPoints = 0;

    private RocketStat[] rocketStats = new RocketStat[STATS_AMOUNT];

    private void Awake()
    {
        if (Instance && Instance != this)
        {
            Destroy(this);
            return;
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }

        for (int i = 0; i < rocketStats.Length; i++)
        {
            rocketStats[i].SetStatType((StatType)i);
        }
    }

    private void Start()
    {
        if (RocketLevelMananger.Instance)
        {
            RocketLevelMananger.Instance.OnRocketLevelUp += RocketLevelMananger_OnRocketLevelUp;
        }
    }

    private void OnDestroy()
    {
        if (RocketLevelMananger.Instance)
        {
            RocketLevelMananger.Instance.OnRocketLevelUp -= RocketLevelMananger_OnRocketLevelUp;
        }
    }

    private void RocketLevelMananger_OnRocketLevelUp()
    {
        AddStatPoints();
    }

    private void AddStatPoints()
    {
        totalStatPoints += statsPointsGivenPerLevelUp;
        currentStatPoints += statsPointsGivenPerLevelUp;
    }
}
