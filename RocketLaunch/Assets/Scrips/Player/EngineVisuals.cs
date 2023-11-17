using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineVisuals : MonoBehaviour
{
    [Header("Engine Visuals")]
    [SerializeField] UIBar enginePowerBar;
    [SerializeField] UIBar engineTemperatureBar;

    private EngineController engineController;

    private void Awake()
    {
        engineController = FindObjectOfType<EngineController>();
        if (engineController)
        {
            engineController.OnEnginePowerChange += EngineController_OnEnginePowerChange;
            engineController.OnEngineTemperatureChange += EngineController_OnEngineTemperatureChange;
        }
    }

    private void OnDestroy()
    {
        if (engineController)
        {
            engineController.OnEnginePowerChange -= EngineController_OnEnginePowerChange;
            engineController.OnEngineTemperatureChange -= EngineController_OnEngineTemperatureChange;
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
}
