using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum StatType { MainEngine, SideEngine, CoolingSystem, LandingSystem, PickupSystem, FuelSystem }
public class RocketStatsMananger : MonoBehaviour
{
    public const int STATS_AMOUNT = 6;   
   
    public static RocketStatsMananger Instance { get; private set; }

    [Header("Rocket Stats Mananger")]
    [SerializeField] private RocketStatsData rocketStatsData;
    [SerializeField] private int statsPointsGivenPerLevelUp = 6;

    public event Action<int> OnCurrentStatPointsChanged;
    public event Action OnResetStatPoints;
    
    private int currentStatPoints = 0;
    //For Testing
    [SerializeField]private RocketStat[] rocketStats;

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

        if (UpgradeRocketMenu.Instance)
        {
            UpgradeRocketMenu.Instance.OnResetStatsButtonPressed += UpgradeRocketMenu_OnResetStatsButtonPressed;
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

        if (UpgradeRocketMenu.Instance)
        {
            UpgradeRocketMenu.Instance.OnResetStatsButtonPressed -= UpgradeRocketMenu_OnResetStatsButtonPressed;
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

    private void UpgradeRocketMenu_OnResetStatsButtonPressed()
    {
        ResetStatPoints();
    }

    private void ResetStatPoints()
    {
        OnResetStatPoints?.Invoke();
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

    public float GetMainEngineSpeedMultiplierAugmentCoeficient() => rocketStatsData.GetMainEngineSpeedMultiplierAugmentCoeficient();

    public float GetMainEngineTemperatureMultiplierAugmentCoeficient() => rocketStatsData.GetMainEngineTemperatureMultiplierAugmentCoeficient();

    public float GetMainEngineFuelConsumptionMultiplierAugmentCoeficient() => rocketStatsData.GetMainEngineFuelConsumptionMultiplierAugmentCoeficient();

    public float GetSideEngineTurningSpeedMultiplierAugmentCoeficient() => rocketStatsData.GetSideEngineTurningSpeedMultiplierAugmentCoeficient();

    public float GetSideEngineTemperatureMultiplierAugmentCoeficient() => rocketStatsData.GetSideEngineTemperatureMultiplierAugmentCoeficient();

    public float GetSideEngineFuelConsumptionMultiplierAugmentCoeficient() => rocketStatsData.GetSideEngineFuelConsumptionMultiplierAugmentCoeficient();

    public float GetMaxTemperatureMultiplierAugmentCoeficient() => rocketStatsData.GetMaxTemperatureMultiplierAugmentCoeficient();

    public float GetCoolingSpeedMultiplierAugmentCoeficient() => rocketStatsData.GetCoolingSpeedMultiplierAugmentCoeficient();

    public float GetOverheatTimeMultiplierAugmentCoeficient() => rocketStatsData.GetOverheatTimeMultiplierAugmentCoeficient();

    public float GetAngularDragMultiplierAugmentCoeficient() => rocketStatsData.GetAngularDragMultiplierAugmentCoeficient();

    public float GetGreenAreaMultiplierAugmentCoeficient() => rocketStatsData.GetGreenAreaMultiplierAugmentCoeficient();

    public float GetYellowAreaMultiplierAugmentCoeficient() => rocketStatsData.GetYellowAreaMultiplierAugmentCoeficient();

    public float GetEffectivePercentageMultiplierAugmentCoeficient() => rocketStatsData.GetEffectivePercentageMultiplierAugmentCoeficient();

    public float GetPullDistanceMultiplierAugmentCoeficient() => rocketStatsData.GetPullDistanceMultiplierAugmentCoeficient();

    public float GetExperienceMultiplierAugmentCoeficient() => rocketStatsData.GetExperienceMultiplierAugmentCoeficient();

    public float GetFuelCapacityMultiplierAugmentCoeficient() => rocketStatsData.GetFuelCapacityMultiplierAugmentCoeficient();
}
