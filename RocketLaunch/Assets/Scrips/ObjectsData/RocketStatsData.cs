using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum RocketStatData { MainEngineSpeed, MainEngineHeatRate, MainEngineFuelConsumption, SideEngineTurningSpeed, SideEngineHeatRate,
                            SideEngineFuelConsumptionRate, MaxTemperature, EngineCoolingSpeed, OverheatRestTime, LandingAngularDrag, 
                            LandingGreenAreaPercentage, LandingYellowAreaPercentage }
public static class RocketStatsData
{
    //Main Engine Stats
    private static float mainEngineSpeed;
    private static float mainEngineHeatRate;
    private static float mainEngineFuelConsumptionRate;
    //Side Engine Stats
    private static float sideEngineTurningSpeed;
    private static float sideEngineHeatRate;
    private static float sideEngineFuelConsumptionRate;
    //Cooling System Stats
    private static float maxTemperature;
    private static float engineCoolingSpeed;
    private static float overheatRestTime;
    //Landing System Stats
    private static float landingAngularDrag;
    private static float landingGreenAreaPercentage;
    private static float landingYellowAreaPercentage;
    //Pickup System Stats
    private static float pickupsEffectPercentage;
    private static float pickupsDistanceFromThePlayer;
    private static float experienceMultiplier;
    //Fuel System Stats
    private static float fuelMaxCapacity;
    



}
