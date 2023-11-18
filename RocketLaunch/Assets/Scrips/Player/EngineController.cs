using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EngineController : MonoBehaviour
{    
    [Header("Engine Controller")]
    [Header("Fuel settings")]
    [SerializeField] private float maxFuelAmount;
    [SerializeField] private float fuelConsuptionSpeed;

    [Header("Power settings")]
    [SerializeField] private float maxEnginePower;
    [SerializeField] private float powerSpeed;
    [SerializeField,Range(0f,1f)] private float powerHeatRateMultiplierPercentage = 0.2f;

    [Header("Temperature settings")]
    [SerializeField] private float maxEngineTemperature;
    [SerializeField] private float heatRate;
    [SerializeField] private float overHeatRestTime;    
    [SerializeField] private int coolingRate;

    public event Action<float, float> OnEnginePowerChange;
    public event Action<float, float> OnEngineTemperatureChange;
    public event Action<float, float> OnFuelChange;
    public event Action<bool> OnMainEngineStateChange;
    public event Action<PlayerMovement.RotationDirection,bool> OnSideEngineStateChange;

    private PlayerMovement playerMovement;

    private float currentEnginePower;
    private float currentEngineTemperature;
    private float currentFuelAmount;

    private bool isMainEngineOn = false;  
    public bool IsOverHeated { get; private set; }
    public bool HasFuel { get; private set; }

    private void Awake()
    {        
        playerMovement = GetComponentInParent<PlayerMovement>();
        IsOverHeated = false;
        HasFuel = true;
    }

    private void Start()
    {
        if (playerMovement)
        {
            playerMovement.OnStartMovingUpwards += PlayerMovement_OnStartMovingUpwards;
            playerMovement.OnStopMovingUpwards += PlayerMovement_OnStopMovingUpwards;
            playerMovement.OnStartRotating += PlayerMovement_OnStartRotating;
            playerMovement.OnStopRotating += PlayerMovement_OnStopRotating;
        }
        currentEnginePower = 0;
        currentEngineTemperature = 0;
        currentFuelAmount = maxFuelAmount;
        OnEnginePowerChange?.Invoke(currentEnginePower, maxEnginePower);
        OnEngineTemperatureChange?.Invoke(currentEngineTemperature, maxEngineTemperature);
        OnFuelChange?.Invoke(currentFuelAmount, maxFuelAmount);
    }

    private void OnDestroy()
    {
        if (playerMovement)
        {
            playerMovement.OnStartMovingUpwards -= PlayerMovement_OnStartMovingUpwards;
            playerMovement.OnStopMovingUpwards -= PlayerMovement_OnStopMovingUpwards;
        }
    }

    private void Update()
    {
        UpdateEngineState();
    }

    private void UpdateEngineState()
    {        
        if (!isMainEngineOn && !IsOverHeated)
        {
            CoolTheEngine();
        }        
    }

    private void MainEngineOn()
    {
        if (IsOverHeated || !HasFuel)
        {
            return;
        }

        UpdateMainEngineState(true);
        currentEnginePower = Mathf.Clamp(currentEnginePower+ powerSpeed * Time.deltaTime, 0 ,maxEnginePower);

        float powerPercentageMultiplier = currentEnginePower * powerHeatRateMultiplierPercentage;
        currentEngineTemperature = Mathf.Clamp(currentEngineTemperature + heatRate * powerPercentageMultiplier * Time.deltaTime, 0, maxEngineTemperature);

        currentFuelAmount = Mathf.Clamp(currentFuelAmount - fuelConsuptionSpeed * Time.deltaTime, 0, maxFuelAmount);

        if (currentEngineTemperature >= maxEngineTemperature)
        {
            UpdateMainEngineState(false);
            IsOverHeated = true;
            StartCoroutine(OverHeatRestRoutine());
        }

        if (currentFuelAmount <= 0)
        {
            UpdateMainEngineState(false);
            HasFuel = false;
        }

        OnEnginePowerChange?.Invoke(currentEnginePower, maxEnginePower);
        OnEngineTemperatureChange?.Invoke(currentEngineTemperature, maxEngineTemperature);
        OnFuelChange?.Invoke(currentFuelAmount, maxFuelAmount);       
    }

    private void CoolTheEngine()
    {
        currentEnginePower = Mathf.Clamp(currentEnginePower - powerSpeed * Time.deltaTime, 0, maxEnginePower);
        OnEnginePowerChange?.Invoke(currentEnginePower, maxEnginePower);        

        if (IsOverHeated)
        {
            return;
        }
        
        currentEngineTemperature = Mathf.Clamp(currentEngineTemperature - coolingRate * Time.deltaTime, 0, maxEngineTemperature);
        OnEngineTemperatureChange?.Invoke(currentEngineTemperature, maxEngineTemperature);
    }

    private void UpdateMainEngineState(bool state)
    {
        if (isMainEngineOn != state)
        {
            isMainEngineOn = state;
            OnMainEngineStateChange?.Invoke(isMainEngineOn);
        }
    }

    private void PlayerMovement_OnStartMovingUpwards(object sender, EventArgs e)
    {       
        MainEngineOn();        
    }

    private void PlayerMovement_OnStopMovingUpwards(object sender, EventArgs e)
    {
        UpdateMainEngineState(false);
    }

    private void PlayerMovement_OnStartRotating(PlayerMovement.RotationDirection rotationDirection)
    {
        OnSideEngineStateChange?.Invoke(rotationDirection, true);
    }

    private void PlayerMovement_OnStopRotating(PlayerMovement.RotationDirection rotationDirection)
    {
        OnSideEngineStateChange?.Invoke(rotationDirection, false);
    }

    private IEnumerator OverHeatRestRoutine()
    {
        float timer = 0;

        while (timer < overHeatRestTime)
        {
            timer += Time.deltaTime;
            float progress = timer / overHeatRestTime;
            currentEngineTemperature = Mathf.Lerp(maxEngineTemperature, 0, progress);
            OnEngineTemperatureChange?.Invoke(currentEngineTemperature, maxEngineTemperature);
            yield return null;
        }

        IsOverHeated = false;
    }
}
