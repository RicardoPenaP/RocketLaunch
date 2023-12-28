using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Settings
{
    public class SettingsDataLoader : MonoBehaviour
    {
        private const string TARGET_FPS_KEY = "TargetFPS";
        private const string QUALITY_OPTION_KEY = "QualityOption";
        private const string FPS_COUNTER_IS_ACTIVE_KEY = "FPSCounterIsActive";
        private const string MUSIC_VOLUME_KEY = "MusicVolume";
        private const string SFX_VOLUME_KEY = "SFXVolume";

        private void Awake()
        {            
            LoadSettingsSavedData();
        }

        private void LoadSettingsSavedData()
        {
            LoadTargetFPS();
            LoadQualityOption();
            LoadFPSCounterIsActive();
            LoadMusicVolume();
            LoadSFXVolume();

        }

        private static void LoadTargetFPS()
        {
            if (PlayerPrefs.HasKey(TARGET_FPS_KEY))
            {
                int targetFps = PlayerPrefs.GetInt(TARGET_FPS_KEY);
                SettingsController.SetTargetFPS(targetFps);
            }
            else
            {
                SaveTargetFPS();
            }
        }

        private static void SaveTargetFPS()
        {
            PlayerPrefs.SetInt(TARGET_FPS_KEY, SettingsController.CurrentTargetFPS);
        }

        private static void LoadQualityOption()
        {
            if (PlayerPrefs.HasKey(QUALITY_OPTION_KEY))
            {
                int qualityOption = PlayerPrefs.GetInt(QUALITY_OPTION_KEY);
                SettingsController.SetQuality((QualityOptions)qualityOption);
            }
            else
            {
                SaveQualityOption();
            }
        }

        private static void SaveQualityOption()
        {
            PlayerPrefs.SetInt(QUALITY_OPTION_KEY, (int)SettingsController.CurrentQualityOption);
        }

        private static void LoadFPSCounterIsActive()
        {
            if (PlayerPrefs.HasKey(FPS_COUNTER_IS_ACTIVE_KEY))
            {
                bool isActive = PlayerPrefs.GetInt(FPS_COUNTER_IS_ACTIVE_KEY) == 1 ? true : false;
                SettingsController.SetFPSCounterState(isActive);
            }
            else
            {
                SaveFPSCounterIsActive();
            }
        }

        private static void SaveFPSCounterIsActive()
        {
            int isActive = SettingsController.FPSCounterIsActive ? 1 : 0;
            PlayerPrefs.SetInt(FPS_COUNTER_IS_ACTIVE_KEY, isActive);
        }

        private static void LoadMusicVolume()
        {
            if (PlayerPrefs.HasKey(MUSIC_VOLUME_KEY))
            {
                float volume = PlayerPrefs.GetFloat(MUSIC_VOLUME_KEY);
                SettingsController.SetMusicVolume(volume);
            }
            else
            {
                SaveMusicVolume();
            }
        }

        private static void SaveMusicVolume()
        {
            PlayerPrefs.SetFloat(MUSIC_VOLUME_KEY, SettingsController.MusicVolume);
        }

        private static void LoadSFXVolume()
        {
            if (PlayerPrefs.HasKey(SFX_VOLUME_KEY))
            {
                float volume = PlayerPrefs.GetFloat(SFX_VOLUME_KEY);
                SettingsController.SetSFXVolume(volume);
            }
            else
            {
                SaveSFXVolume();
            }
        }

        private static void SaveSFXVolume()
        {
            PlayerPrefs.SetFloat(SFX_VOLUME_KEY, SettingsController.SFXVolume);
        }

        public static void SaveSettingsData()
        {
            SaveQualityOption();
            SaveTargetFPS();
            SaveFPSCounterIsActive();
            SaveMusicVolume();
            SaveSFXVolume();            
        }
    }
}

