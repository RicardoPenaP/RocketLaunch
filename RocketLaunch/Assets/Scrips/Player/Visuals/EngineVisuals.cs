using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EngineVisuals : MonoBehaviour
{
    [Header("Engine Visuals")]
    [SerializeField] UIBar enginePowerBar;
    [SerializeField] UIBar engineTemperatureBar;
    [SerializeField] UIBar fuelBar;

    private EngineController engineController;
    private PlayerLandingController playerLandingController;

    private void Awake()
    {
        engineController = FindObjectOfType<EngineController>();
        playerLandingController = engineController.GetComponentInParent<PlayerLandingController>();

        if (engineController)
        {
            engineController.OnEnginePowerChange += EngineController_OnEnginePowerChange;
            engineController.OnEngineTemperatureChange += EngineController_OnEngineTemperatureChange;
            engineController.OnFuelChange += EngineController_OnFuelChange;
        }

        if (playerLandingController)
        {
            playerLandingController.OnPreLandingStart += PlayerLandingController_OnPreLandingStart;
        }
    }

    private void OnDestroy()
    {
        if (engineController)
        {
            engineController.OnEnginePowerChange -= EngineController_OnEnginePowerChange;
            engineController.OnEngineTemperatureChange -= EngineController_OnEngineTemperatureChange;
            engineController.OnFuelChange -= EngineController_OnFuelChange;
        }

        if (playerLandingController)
        {
            playerLandingController.OnPreLandingStart -= PlayerLandingController_OnPreLandingStart;
        }
    }

    private void EngineController_OnEnginePowerChange(float currentValue, float maxValue)
    {
        enginePowerBar.UpdateFill(currentValue, maxValue);
    }

    private void EngineController_OnEngineTemperatureChange(float currentValue, float maxValue)
    {
        engineTemperatureBar.UpdateFill(currentValue, maxValue);
    }

    private void EngineController_OnFuelChange(float currentValue, float maxValue)
    {
        fuelBar.UpdateFill(currentValue, maxValue);
    }

    private void PlayerLandingController_OnPreLandingStart(object sender, EventArgs e)
    {
        transform.parent.gameObject.SetActive(false);
    }
}
