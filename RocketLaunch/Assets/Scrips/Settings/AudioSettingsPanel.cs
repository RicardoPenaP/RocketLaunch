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

    private void Start()
    {
        if (SettingsMenu.Instance)
        {
            SettingsMenu.Instance.OnMenuOpened += SettingsMenu_OnMenuOpened;
            SettingsMenu.Instance.OnResetButtonPressed += SettingsMenu_OnResetButtonPressed;
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

        if (SettingsMenu.Instance)
        {
            SettingsMenu.Instance.OnMenuOpened -= SettingsMenu_OnMenuOpened;
            SettingsMenu.Instance.OnResetButtonPressed -= SettingsMenu_OnResetButtonPressed;
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

    private void SettingsMenu_OnMenuOpened()
    {
        musicVolumeSlider.value = SettingsController.MusicVolume;
        sfxVolumeSlider.value = SettingsController.SFXVolume;
    }

    private void SettingsMenu_OnResetButtonPressed()
    {
        musicVolumeSlider.value = SettingsController.DEFAULT_MUSIC_VOLUME;
        sfxVolumeSlider.value = SettingsController.DEFAULT_SFX_VOLUME;
    }
}
