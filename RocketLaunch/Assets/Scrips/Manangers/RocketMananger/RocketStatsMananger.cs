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
        RocketStatPanel.OnAnyLevelUpButtonPressed += RocketStatPanel_OnAnyLevelUpButtonPressed;

        if (RocketLevelMananger.Instance)
        {
            RocketLevelMananger.Instance.OnRocketLevelUp += RocketLevelMananger_OnRocketLevelUp;
        }
    }

    private void OnDestroy()
    {
        RocketStatPanel.OnAnyLevelUpButtonPressed -= RocketStatPanel_OnAnyLevelUpButtonPressed;

        if (RocketLevelMananger.Instance)
        {
            RocketLevelMananger.Instance.OnRocketLevelUp -= RocketLevelMananger_OnRocketLevelUp;
        }
    }

    private void RocketLevelMananger_OnRocketLevelUp()
    {
        AddStatPoints();
    }

    private void RocketStatPanel_OnAnyLevelUpButtonPressed()
    {
        if (currentStatPoints > 1)
        {
            currentStatPoints--;
        }
    }

    private void RocketStatPanel_OnAnyLevelDownButtonPressed()
    {
        currentStatPoints++;
    }

    private void AddStatPoints()
    {
        totalStatPoints += statsPointsGivenPerLevelUp;
        currentStatPoints += statsPointsGivenPerLevelUp;
    }

    public RocketStat GetRocketStat(StatType statType)
    {
        return rocketStats[(int)statType];
    }
}
