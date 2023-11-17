using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EngineController : MonoBehaviour
{
    [Header("Engine Controller")]

    [Header("Power settings")]
    [SerializeField] private float maxEnginePower;
    [SerializeField] private float powerSpeed;

    [Header("Temperature settings")]
    [SerializeField] private float maxTemperature;
    [SerializeField] private float heatRate;
    [SerializeField] private float overHeatRestTime;
    [Tooltip("The percentage of the heat rate that is used to cool the engine")]
    [SerializeField,Range(0,100)] private int coolingRatePercentage;

    private PlayerMovement playerMovement;

    private float currentEnginePower;
    private float currentEngineTemperature;

    private bool isAddingPower = false;

    public bool IsOverHeated { get; private set; }

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Start()
    {
        if (playerMovement)
        {
            playerMovement.OnStartMovingUpwards += PlayerMovement_OnStartMovingUpwards;
            playerMovement.OnStopMovingUpwards += PlayerMovement_OnStopMovingUpwards;
        }
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
        if (isAddingPower)
        {
            AddPower();
        }
        else
        {
            CoolTheEngine();
        }
    }

    private void AddPower()
    {
        if (IsOverHeated)
        {
            return;
        }
        currentEnginePower = Mathf.Clamp(currentEnginePower+ powerSpeed * Time.deltaTime, 0 ,maxEnginePower);
        currentEngineTemperature = Mathf.Clamp(currentEngineTemperature + heatRate * Time.deltaTime, 0, maxTemperature);

        if (currentEngineTemperature >= maxTemperature)
        {
            IsOverHeated = true;
            StartCoroutine(OverHeatRestRoutine());
        }
    }

    private void CoolTheEngine()
    {
        currentEnginePower = Mathf.Clamp(currentEnginePower - powerSpeed * Time.deltaTime, 0, maxEnginePower);
        float coolRate = (heatRate * coolingRatePercentage) / 100;
        currentEngineTemperature = Mathf.Clamp(currentEngineTemperature - coolRate * Time.deltaTime, 0, maxTemperature);
    }

    private void PlayerMovement_OnStartMovingUpwards(object sender, EventArgs e)
    {
        isAddingPower = true;
    }

    private void PlayerMovement_OnStopMovingUpwards(object sender, EventArgs e)
    {
        isAddingPower = true;
    }

    private IEnumerator OverHeatRestRoutine()
    {
        float timer = 0;

        while (timer < overHeatRestTime)
        {
            timer += Time.deltaTime;
            float progress = timer / overHeatRestTime;
            currentEngineTemperature = Mathf.Lerp(maxTemperature, 0, progress);
            yield return null;
        }

        IsOverHeated = false;
    }
}
