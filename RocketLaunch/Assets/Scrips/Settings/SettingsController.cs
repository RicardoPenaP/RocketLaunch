using UnityEngine;
using System;

namespace Settings
{
    public enum QualityOptions { Ultra, High, Mid, Low }
    public enum TargetFPSOptions { Low, Mid, High, Ultra, Unlimited }

    public static class SettingsController
    {
        public const QualityOptions DEFAULT_QUALITY = QualityOptions.Ultra;
        public const int DEFAULT_TARGET_FPS = 60;
        public const bool DEFAULT_FULLSCREEN_MODE_STATE = true;
        public const float DEFAULT_MUSIC_VOLUME = 0.5f;
        public const float DEFAULT_SFX_VOLUME = 0.5f;

        public static event Action OnTargetFPSChange;
        public static event Action OnFPSCounterStateChange;
        public static event Action OnMusicVolumeChange;
        public static event Action OnSFXVolumeChange;

        public static int CurrentTargetFPS { get; private set; } = DEFAULT_TARGET_FPS;
        public static QualityOptions CurrentQualityOption { get; private set; } = DEFAULT_QUALITY;

        public static bool FPSCounterIsActive { get; private set; } = false;

        public static float MusicVolume { get; private set; } = DEFAULT_MUSIC_VOLUME;
        public static float SFXVolume { get; private set; } = DEFAULT_SFX_VOLUME;

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

        public static void SetTargetFPS(int targetFPS)
        {
            CurrentTargetFPS = targetFPS;
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

        public static void SetMusicVolume(float volume)
        {
            MusicVolume = Mathf.Clamp(volume,0f,1f);            
            OnMusicVolumeChange?.Invoke();
        }

        public static void SetSFXVolume(float volume)
        {
            SFXVolume = Mathf.Clamp(volume, 0f, 1f);            
            OnSFXVolumeChange?.Invoke();
        }
    }

    
}

