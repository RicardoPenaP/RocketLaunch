using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EngineController : MonoBehaviour
{
    private const int FULL_PERCENTAGE = 100;    

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
    [SerializeField] private float overheatRestTime;    
    [SerializeField] private int coolingRate;

    [Header("Side engine settings")]
    [SerializeField, Range(0f, 1f)] private float sideEngineComsuptionPercentage = 0.5f;

    public event Action<float, float> OnEnginePowerChange;
    public event Action<float, float> OnEngineTemperatureChange;
    public event Action<float, float> OnFuelChange;
    public event Action<bool> OnMainEngineStateChange;
    public event Action<PlayerMovement.RotationDirection,bool> OnSideEngineStateChange;

    private PlayerMovement playerMovement;
    private PlayerPickupsHandler playerPickupsHandler;
    private PlayerController playerController;

    private float currentEnginePower;
    private float currentEngineTemperature;
    private float currentFuelAmount;

    private float mainEngineTemperatureMultiplier;
    private float mainEngineFuelConsumptionMultiplier;
    private float sideEngineTemperatureMultiplier;
    private float sideEngineFuelConsumptionMultiplier;
    private float maxEngineTemperatureMultiplier;
    private float coolingRateMultiplier;
    private float overheatTimeMultiplier;
    

    private bool isMainEngineOn = false;
    private bool isSideEngineOn = false;
    public bool IsOverHeated { get; private set; }
    public bool HasFuel { get; private set; }

    private void Awake()
    {        
        playerMovement = GetComponentInParent<PlayerMovement>();
        playerPickupsHandler = transform.parent.GetComponentInChildren<PlayerPickupsHandler>();
        playerController = GetComponentInParent<PlayerController>();
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

        if (playerPickupsHandler)
        {
            playerPickupsHandler.OnPickupPiked += PlayerPickupsHandler_OnPickupPicked;
        }

        if (playerController)
        {
            playerController.OnPlayerReset += PlayerController_OnPlayerReset;
        }
        Initialize();
    }

    private void OnDestroy()
    {
        if (playerMovement)
        {
            playerMovement.OnStartMovingUpwards -= PlayerMovement_OnStartMovingUpwards;
            playerMovement.OnStopMovingUpwards -= PlayerMovement_OnStopMovingUpwards;
            playerMovement.OnStartRotating -= PlayerMovement_OnStartRotating;
            playerMovement.OnStopRotating -= PlayerMovement_OnStopRotating;
        }

        if (playerPickupsHandler)
        {
            playerPickupsHandler.OnPickupPiked -= PlayerPickupsHandler_OnPickupPicked;
        }

        if (playerController)
        {
            playerController.OnPlayerReset -= PlayerController_OnPlayerReset;
        }
    }

    private void Update()
    {
        UpdateEngineCooling();
    }

    private void Initialize()
    {
        currentEnginePower = 0;
        currentEngineTemperature = 0;
        currentFuelAmount = maxFuelAmount;
        OnEnginePowerChange?.Invoke(currentEnginePower, maxEnginePower);
        OnEngineTemperatureChange?.Invoke(currentEngineTemperature, maxEngineTemperature);
        OnFuelChange?.Invoke(currentFuelAmount, maxFuelAmount);

        mainEngineTemperatureMultiplier = 1f;
        mainEngineFuelConsumptionMultiplier = 1f;

        sideEngineTemperatureMultiplier = 1f;
        sideEngineFuelConsumptionMultiplier = 1f;

        maxEngineTemperatureMultiplier = 1f;
        coolingRateMultiplier = 1f;
        overheatTimeMultiplier = 1f;

        if (RocketStatsMananger.Instance)
        {
            //Main engine stat multiplier settings
            int rocketStatLevel = RocketStatsMananger.Instance.GetRocketStat(StatType.MainEngine).GetStatLevel();
            float rocketStatMultiplierAugmentCoeficient = RocketStatsMananger.Instance.GetMainEngineTemperatureMultiplierAugmentCoeficient();            
            mainEngineTemperatureMultiplier += rocketStatMultiplierAugmentCoeficient * rocketStatLevel;
            rocketStatMultiplierAugmentCoeficient = RocketStatsMananger.Instance.GetMainEngineFuelConsumptionMultiplierAugmentCoeficient();
            mainEngineFuelConsumptionMultiplier += rocketStatMultiplierAugmentCoeficient * rocketStatLevel;

            //Side engine stat multiplier settings
            rocketStatLevel = RocketStatsMananger.Instance.GetRocketStat(StatType.SideEngine).GetStatLevel();
            rocketStatMultiplierAugmentCoeficient = RocketStatsMananger.Instance.GetSideEngineTemperatureMultiplierAugmentCoeficient();
            sideEngineTemperatureMultiplier += rocketStatMultiplierAugmentCoeficient * rocketStatLevel;
            rocketStatMultiplierAugmentCoeficient = RocketStatsMananger.Instance.GetSideEngineFuelConsumptionMultiplierAugmentCoeficient();
            sideEngineFuelConsumptionMultiplier += rocketStatMultiplierAugmentCoeficient * rocketStatLevel;

            //Colling system multiplier settings
            rocketStatLevel = RocketStatsMananger.Instance.GetRocketStat(StatType.CoolingSystem).GetStatLevel();
            rocketStatMultiplierAugmentCoeficient = RocketStatsMananger.Instance.GetMaxTemperatureMultiplierAugmentCoeficient();
            maxEngineTemperatureMultiplier += rocketStatMultiplierAugmentCoeficient * rocketStatLevel;
            rocketStatMultiplierAugmentCoeficient = RocketStatsMananger.Instance.GetCoolingSpeedMultiplierAugmentCoeficient();
            coolingRateMultiplier += rocketStatMultiplierAugmentCoeficient * rocketStatLevel;
            rocketStatMultiplierAugmentCoeficient = RocketStatsMananger.Instance.GetOverheatTimeMultiplierAugmentCoeficient();
            overheatTimeMultiplier -= rocketStatMultiplierAugmentCoeficient * rocketStatLevel;
        }

        maxEngineTemperature *= maxEngineTemperatureMultiplier;
        overheatRestTime *= overheatTimeMultiplier;
    }

    private void UpdateEngineCooling()
    {        
        if (!isMainEngineOn && !IsOverHeated && !isSideEngineOn)
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
        currentEngineTemperature = Mathf.Clamp(currentEngineTemperature + heatRate * powerPercentageMultiplier * mainEngineTemperatureMultiplier * Time.deltaTime, 0, maxEngineTemperature);

        currentFuelAmount = Mathf.Clamp(currentFuelAmount - fuelConsuptionSpeed * mainEngineFuelConsumptionMultiplier * Time.deltaTime, 0, maxFuelAmount);

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
        
        currentEngineTemperature = Mathf.Clamp(currentEngineTemperature - coolingRate * coolingRateMultiplier * Time.deltaTime, 0, maxEngineTemperature);
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

    private void UpdateSideEngineState(bool state, PlayerMovement.RotationDirection rotationDirection)
    {        
        isSideEngineOn = state;
        OnSideEngineStateChange?.Invoke(rotationDirection, state);
    }

    private void SideEngineOn(PlayerMovement.RotationDirection rotationDirection)
    {
        if (IsOverHeated || !HasFuel)
        {
            return;
        }
        UpdateSideEngineState(true, rotationDirection);

        currentEnginePower = Mathf.Clamp(currentEnginePower + (powerSpeed * sideEngineComsuptionPercentage) * Time.deltaTime, 0, maxEnginePower);

        float powerPercentageMultiplier = currentEnginePower * powerHeatRateMultiplierPercentage;
        currentEngineTemperature = Mathf.Clamp(currentEngineTemperature + (heatRate * sideEngineComsuptionPercentage)* sideEngineTemperatureMultiplier * powerPercentageMultiplier * Time.deltaTime, 0, maxEngineTemperature);

        currentFuelAmount = Mathf.Clamp(currentFuelAmount - (fuelConsuptionSpeed * sideEngineComsuptionPercentage)* sideEngineFuelConsumptionMultiplier * Time.deltaTime, 0, maxFuelAmount);

        if (currentEngineTemperature >= maxEngineTemperature)
        {
            UpdateSideEngineState(false, rotationDirection);
            IsOverHeated = true;
            StartCoroutine(OverHeatRestRoutine());
        }

        if (currentFuelAmount <= 0)
        {
            UpdateSideEngineState(false, rotationDirection);
            HasFuel = false;
        }

        OnEnginePowerChange?.Invoke(currentEnginePower, maxEnginePower);
        OnEngineTemperatureChange?.Invoke(currentEngineTemperature, maxEngineTemperature);
        OnFuelChange?.Invoke(currentFuelAmount, maxFuelAmount);
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
        SideEngineOn(rotationDirection);
        
    }

    private void PlayerMovement_OnStopRotating(PlayerMovement.RotationDirection rotationDirection)
    {
        UpdateSideEngineState(false, rotationDirection);
    }

    private void PlayerPickupsHandler_OnPickupPicked(Pickup pickup)
    {
        switch (pickup.GetPickupType())
        {
            case Pickup.PickupType.Cooland:
                if (IsOverHeated)
                {
                    IsOverHeated = false;
                }
                InstanlyCoolTheEngine(pickup.GetEffectivePercentage());
                break;
            case Pickup.PickupType.Fuel:
                RefillFuel(pickup.GetEffectivePercentage());
                break;
            default:
                break;
        }
    }

    private void PlayerController_OnPlayerReset(object sender, EventArgs e)
    {
        ResetEngine();
    }

    private void InstanlyCoolTheEngine(int percentage)
    {
        float coolAmount = maxEngineTemperature * CalculateNormalizedPercentage(percentage);
        if (currentEngineTemperature > coolAmount)
        {
            currentEngineTemperature -= coolAmount;
        }
        else
        {
            currentEngineTemperature = 0;
        }
        OnEngineTemperatureChange?.Invoke(currentEngineTemperature, maxEngineTemperature);
    }

    private void RefillFuel(int percentage)
    {
        float fuelAmount = maxFuelAmount * CalculateNormalizedPercentage(percentage);
        if (maxFuelAmount - currentFuelAmount > fuelAmount)
        {
            currentFuelAmount += fuelAmount;
        }
        else
        {
            currentFuelAmount = maxFuelAmount;
        }
        OnFuelChange(currentFuelAmount, maxFuelAmount);
    }

    private float CalculateNormalizedPercentage(int percentage)
    {
        return (float)percentage / 100;
    }

    private void ResetEngine()
    {
        StopAllCoroutines();
        InstanlyCoolTheEngine(FULL_PERCENTAGE);
        RefillFuel(FULL_PERCENTAGE);
        currentEnginePower = 0f;
        HasFuel = true;
        OnEnginePowerChange?.Invoke(currentEnginePower, maxEnginePower);
    }

    private IEnumerator OverHeatRestRoutine()
    {
        float timer = 0;

        while (timer < overheatRestTime && IsOverHeated)
        {
            timer += Time.deltaTime;
            float progress = timer / overheatRestTime;
            currentEngineTemperature = Mathf.Lerp(maxEngineTemperature, 0, progress);
            OnEngineTemperatureChange?.Invoke(currentEngineTemperature, maxEngineTemperature);
            yield return null;
        }

        IsOverHeated = false;
    }
}
