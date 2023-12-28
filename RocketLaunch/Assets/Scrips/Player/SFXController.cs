using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Settings;

public class SFXController : MonoBehaviour
{
    [Header("SFX Controller")]
    [Header("Audio Source References")]
    [SerializeField] private AudioSource primaryAudioSource;
    [SerializeField] private AudioSource secondaryAudioSource;
    [Header("Audio Clips References")]
    [SerializeField] private AudioClip mainEngineSFX;
    [SerializeField] private AudioClip sideEngineSFX;
    [SerializeField] private AudioClip explosionEngineSFX;

    private PlayerController playerController;
    private EngineController engineController;
   
    private void Awake()
    {
        playerController = GetComponentInParent<PlayerController>();
        engineController = playerController.GetComponentInChildren<EngineController>();

        SettingsController.OnSFXVolumeChange += SettingsController_OnSFXVolumeChange;
        SetAudioSourceVolume();
    }

    private void Start()
    {
        if (playerController)
        {
            playerController.OnPlayerCrash += PlayerController_OnPlayerCrash;
        }

        if (engineController)
        {
            engineController.OnMainEngineStateChange += EngineController_OnMainEngineStateChange;
            engineController.OnSideEngineStateChange += EngineController_OnSideEngineStateChange;
        }
    }

    private void OnDestroy()
    {
        SettingsController.OnSFXVolumeChange -= SettingsController_OnSFXVolumeChange;

        if (playerController)
        {
            playerController.OnPlayerCrash -= PlayerController_OnPlayerCrash;
        }

        if (engineController)
        {
            engineController.OnMainEngineStateChange -= EngineController_OnMainEngineStateChange;
            engineController.OnSideEngineStateChange -= EngineController_OnSideEngineStateChange;
        }
    }

    private void SettingsController_OnSFXVolumeChange()
    {
        SetAudioSourceVolume();
    }

    private void SetAudioSourceVolume()
    {
        primaryAudioSource.volume = SettingsController.SFXVolume;
        secondaryAudioSource.volume = SettingsController.SFXVolume;
    }

    private void PlayMainEngineSFX()
    {
        if (!primaryAudioSource.isPlaying)
        {
            primaryAudioSource.PlayOneShot(mainEngineSFX);
        }
    }

    private void StopPlayingMainEngineSFX()
    {
        if (primaryAudioSource.isPlaying)
        {
            primaryAudioSource.Stop();
        }
    }

    private void PlaySideEngineSFX()
    {
        if (!secondaryAudioSource.isPlaying)
        {
            secondaryAudioSource.PlayOneShot(sideEngineSFX);
        }       
    }

    private void StopPlayingSideEngineSFX()
    {
        if (secondaryAudioSource.isPlaying)
        {
            secondaryAudioSource.Stop();
        }
    }

    private void PlayExposionSFX()
    {
        primaryAudioSource.PlayOneShot(explosionEngineSFX);
    }

    private void EngineController_OnMainEngineStateChange(bool state)
    {
        if (state)
        {
            PlayMainEngineSFX();
        }
        else
        {
            StopPlayingMainEngineSFX();
        }
    }

    private void EngineController_OnSideEngineStateChange(PlayerMovement.RotationDirection rotationDirection,bool state)
    {
        if (state)
        {
            PlaySideEngineSFX();
        }
        else
        {
            StopPlayingSideEngineSFX();
        }
    }

    private void PlayerController_OnPlayerCrash()
    {
        PlayExposionSFX();
    }

}
