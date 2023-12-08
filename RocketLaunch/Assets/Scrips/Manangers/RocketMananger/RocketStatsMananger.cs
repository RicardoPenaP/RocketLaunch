using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum StatType { MainEngine, SideEngine, CoolingSystem, LandingSystem, PickupSystem, ShieldSystem }
public class RocketStatsMananger : MonoBehaviour
{
    public const int STATS_AMOUNT = 6;
   
    public static RocketStatsMananger Instance { get; private set; }

    [Header("Rocket Stats Mananger")]
    [SerializeField] private int statsPointsGivenPerLevelUp = 6;

    public event Action<int> OnCurrentStatPointsChanged;   
    
    private int currentStatPoints = 0;

    private RocketStat[] rocketStats;

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

        rocketStats = new RocketStat[STATS_AMOUNT];

        for (int i = 0; i < rocketStats.Length; i++)
        {
            rocketStats[i] = new RocketStat((StatType)i);
        }
    }

    private void Start()
    {
        RocketStatPanel.OnAnyLevelUpButtonPressed += RocketStatPanel_OnAnyLevelUpButtonPressed;
        RocketStatPanel.OnAnyLevelDownButtonPressed += RocketStatPanel_OnAnyLevelDownButtonPressed;

        if (RocketLevelMananger.Instance)
        {
            RocketLevelMananger.Instance.OnRocketLevelUp += RocketLevelMananger_OnRocketLevelUp;
        }
    }

    private void OnDestroy()
    {
        RocketStatPanel.OnAnyLevelUpButtonPressed -= RocketStatPanel_OnAnyLevelUpButtonPressed;
        RocketStatPanel.OnAnyLevelDownButtonPressed -= RocketStatPanel_OnAnyLevelDownButtonPressed;

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
        if (currentStatPoints == 0)
        {
            return;
        }

        currentStatPoints--;
        OnCurrentStatPointsChanged?.Invoke(currentStatPoints);

    }

    private void RocketStatPanel_OnAnyLevelDownButtonPressed()
    {        
        currentStatPoints++;
        OnCurrentStatPointsChanged?.Invoke(currentStatPoints);
    }

    private void AddStatPoints()
    {       
        currentStatPoints += statsPointsGivenPerLevelUp;
    }

    public RocketStat GetRocketStat(StatType statType)
    {
        return rocketStats[(int)statType];
    }

    public int GetCurrentStatPoints()
    {
        return currentStatPoints;
    }
}
