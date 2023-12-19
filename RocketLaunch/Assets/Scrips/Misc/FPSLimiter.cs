using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Settings;

public class FPSLimiter : MonoBehaviour
{
    public static FPSLimiter Instance { get; private set; }

    private void Awake()
    {
        if (Instance && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }

        SettingsController.OnTargetFPSChange += SettingsController_OnTargetFPSChange;

        Application.targetFrameRate = SettingsController.CurrentTargetFPS;
    }

    private void OnDestroy()
    {
        SettingsController.OnTargetFPSChange -= SettingsController_OnTargetFPSChange;
    }

    private void SettingsController_OnTargetFPSChange()
    {
        Application.targetFrameRate = SettingsController.CurrentTargetFPS;
    }

}
