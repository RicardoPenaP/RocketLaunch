using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Settings;

public class BackgroundMusicMananger : MonoBehaviour
{
    [Header("Backgroud Music Mananger")]
    [SerializeField] private AudioSource audioSource;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        SettingsController.OnMusicVolumeChange += SettingsController_OnMusicVolumeChange;
    }

    private void OnDestroy()
    {
        SettingsController.OnMusicVolumeChange -= SettingsController_OnMusicVolumeChange;
    }

    private void SettingsController_OnMusicVolumeChange()
    {
        audioSource.volume = SettingsController.MusicVolume;
    }
}
