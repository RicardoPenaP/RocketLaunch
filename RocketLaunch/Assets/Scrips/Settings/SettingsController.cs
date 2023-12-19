using UnityEngine;
using System;

namespace Settings
{    
    public static class SettingsController
    {
        public static event Action OnTargetFPSChange;
        public static event Action OnFPSCounterStateChange;
        public static int CurrentTargetFPS { get; private set; } = SettingsData.DEFAULT_TARGET_FPS;
        public static QualityOptions CurrentQualityOption { get; private set; } = SettingsData.DEFAULT_QUALITY;

        public static bool FPSCounterIsActive { get; private set; } = false;

        public static void SetQuality(QualityOptions qualityOption)
        {
            CurrentQualityOption = qualityOption;
            QualitySettings.SetQualityLevel((int)qualityOption, false);
        }

        public static void SetTargetFPS(TargetFPSOptions targetFPS)
        {
            switch (targetFPS)
            {
                case TargetFPSOptions.Low:
                    CurrentTargetFPS = 30;
                    break;
                case TargetFPSOptions.Mid:
                    CurrentTargetFPS = 60;
                    break;
                case TargetFPSOptions.High:
                    CurrentTargetFPS = 120;
                    break;
                case TargetFPSOptions.Ultra:
                    CurrentTargetFPS = 240;
                    break;
                case TargetFPSOptions.Unlimited:
                    CurrentTargetFPS = -1;
                    break;
                default:
                    break;
            }            
            OnTargetFPSChange?.Invoke();
        }

        public static void SetFullScreen(bool state)
        {
            if (state != Screen.fullScreen)
            {
                Screen.fullScreen = state;
            }
        }

        public static void SetFPSCounterState(bool state)
        {
            FPSCounterIsActive = state;
            OnFPSCounterStateChange?.Invoke();
        }
    }
}

