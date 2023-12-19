using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Settings;

public class AudioSettingsPanel : MonoBehaviour
{
    [Header("Audios Settings Panel")]
    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private Slider sfxVolumeSlider;

    private void Awake()
    {
        if (musicVolumeSlider)
        {
            musicVolumeSlider.onValueChanged.AddListener(MusicVolumeSlider_OnValueChange);
        }

        if (sfxVolumeSlider)
        {
            sfxVolumeSlider.onValueChanged.AddListener(SFXVolumeSlider_OnValueChange);
        }
    }

    private void OnDestroy()
    {
        if (musicVolumeSlider)
        {
            musicVolumeSlider.onValueChanged.RemoveListener(MusicVolumeSlider_OnValueChange);
        }

        if (sfxVolumeSlider)
        {
            sfxVolumeSlider.onValueChanged.RemoveListener(SFXVolumeSlider_OnValueChange);
        }
    }

    private void MusicVolumeSlider_OnValueChange(float value)
    {
        SettingsController.SetMusicVolume(value);
    }

    private void SFXVolumeSlider_OnValueChange(float value)
    {
        SettingsController.SetSFXVolume(value);
    }
}
