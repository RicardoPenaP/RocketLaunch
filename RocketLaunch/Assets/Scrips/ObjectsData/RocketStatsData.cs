using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Rocket Stats Data", fileName ="New RocketStatsData")]
public class RocketStatsData: ScriptableObject
{
    [Header("Rocket Stats Data")]

    [Header("Main Engine Stat")]
    [SerializeField, Range(0f, 1f)] private float mainEngineSpeedMultiplierAugmentCoeficient= 0.1f;
    [SerializeField, Range(0f, 1f)] private float mainEngineTemperatureMultiplierAugmentCoeficient = 0.1f;
    [SerializeField, Range(0f, 1f)] private float mainEngineFuelConsumptionMultiplierAugmentCoeficient = 0.1f;

    [Header("Side Engine Stat")]
    [SerializeField, Range(0f, 1f)] private float sideEngineTurningSpeedMultiplierAugmentCoeficient = 0.1f;
    [SerializeField, Range(0f, 1f)] private float sideEngineTemperatureMultiplierAugmentCoeficient = 0.1f;
    [SerializeField, Range(0f, 1f)] private float sideEngineFuelConsumptionMultiplierAugmentCoeficient = 0.1f;

    [Header("Cooling System Stat")]
    [SerializeField, Range(0f, 1f)] private float maxTemperatureMultiplierAugmentCoeficient = 0.1f;
    [SerializeField, Range(0f, 1f)] private float coolingSpeedMultiplierAugmentCoeficient = 0.1f;
    [SerializeField, Range(0f, 1f)] private float overheatTimeMultiplierAugmentCoeficient = 0.1f;

    [Header("Landing System Stat")]
    [SerializeField, Range(0f, 1f)] private float angularDragMultiplierAugmentCoeficient = 0.1f;
    [SerializeField, Range(0f, 1f)] private float greenAreaMultiplierAugmentCoeficient = 0.1f;
    [SerializeField, Range(0f, 1f)] private float yellowAreaMultiplierAugmentCoeficient = 0.1f;

    [Header("Pickup System Stat")]
    [SerializeField, Range(0f, 1f)] private float effectivePercentageMultiplierAugmentCoeficient = 0.1f;
    [SerializeField, Range(0f, 1f)] private float pullDistanceMultiplierAugmentCoeficient = 0.1f;
    [SerializeField, Range(0f, 1f)] private float experienceMultiplierAugmentCoeficient = 0.1f;

    [Header("Fuel System Stat")]
    [SerializeField, Range(0f, 1f)] private float fuelCapacityMultiplierAugmentCoeficient = 0.1f; 

    public float GetMainEngineSpeedMultiplierAugmentCoeficient()
    {
        return mainEngineSpeedMultiplierAugmentCoeficient;
    }

    public float GetMainEngineTemperatureMultiplierAugmentCoeficient()
    {
        return mainEngineTemperatureMultiplierAugmentCoeficient;
    }

    public float GetMainEngineFuelConsumptionMultiplierAugmentCoeficient()
    {
        return mainEngineFuelConsumptionMultiplierAugmentCoeficient;
    }

    public float GetSideEngineTurningSpeedMultiplierAugmentCoeficient()
    {
        return sideEngineTurningSpeedMultiplierAugmentCoeficient;
    }

    public float GetSideEngineTemperatureMultiplierAugmentCoeficient()
    {
        return sideEngineTemperatureMultiplierAugmentCoeficient;
    }

    public float GetSideEngineFuelConsumptionMultiplierAugmentCoeficient()
    {
        return sideEngineFuelConsumptionMultiplierAugmentCoeficient;
    }

    public float GetMaxTemperatureMultiplierAugmentCoeficient()
    {
        return maxTemperatureMultiplierAugmentCoeficient;
    }

    public float GetCoolingSpeedMultiplierAugmentCoeficient()
    {
        return maxTemperatureMultiplierAugmentCoeficient;
    }

    public float GetOverheatTimeMultiplierAugmentCoeficient()
    {
        return overheatTimeMultiplierAugmentCoeficient;
    }

    public float GetAngularDragMultiplierAugmentCoeficient()
    {
        return angularDragMultiplierAugmentCoeficient;
    }

    public float GetGreenAreaMultiplierAugmentCoeficient()
    {
        return greenAreaMultiplierAugmentCoeficient;
    }

    public float GetYellowAreaMultiplierAugmentCoeficient()
    {
        return yellowAreaMultiplierAugmentCoeficient;
    }

    public float GetEffectivePercentageMultiplierAugmentCoeficient()
    {
        return yellowAreaMultiplierAugmentCoeficient;
    }

    public float GetPullDistanceMultiplierAugmentCoeficient()
    {
        return pullDistanceMultiplierAugmentCoeficient;
    }

    public float GetExperienceMultiplierAugmentCoeficient()
    {
        return experienceMultiplierAugmentCoeficient;
    }

    public float GetFuelCapacityMultiplierAugmentCoeficient()
    {
        return fuelCapacityMultiplierAugmentCoeficient;
    }
}
