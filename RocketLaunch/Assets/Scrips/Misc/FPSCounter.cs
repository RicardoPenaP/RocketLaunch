using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Settings;
using TMPro;

public class FPSCounter : MonoBehaviour
{
    [Header("FPS Counter")]
    [SerializeField] private TextMeshProUGUI counterText;
    [SerializeField] private float counterUpdateTime = 0.5f;

    private float timer = 0f;

    private void Awake()
    {
        SettingsController.OnFPSCounterStateChange += SettingsController_OnFPSCounterStateChange;
        if (SettingsController.FPSCounterIsActive != gameObject.activeInHierarchy)
        {
            gameObject.SetActive(SettingsController.FPSCounterIsActive);
        }        
    }

    private void Update()
    {
        float fpsAproximate = 1 / Time.deltaTime;
        int roundedFPS = Mathf.RoundToInt(fpsAproximate);
        timer += Time.deltaTime;
        if (timer >= counterUpdateTime)
        {           
            counterText.text = $"FPS: {roundedFPS}";
            timer = 0;
        }
       
    }


    private void OnDestroy()
    {
        SettingsController.OnFPSCounterStateChange -= SettingsController_OnFPSCounterStateChange;
    }

    private void SettingsController_OnFPSCounterStateChange()
    {
        if (SettingsController.FPSCounterIsActive != gameObject.activeInHierarchy)
        {
            gameObject.SetActive(SettingsController.FPSCounterIsActive);
            
        }
    }

}
