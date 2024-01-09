using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Settings;
using System;

public class SFXController : MonoBehaviour
{
    [Header("SFX Controller")]
    [Header("Audio Source References")]
    [SerializeField] private AudioSource primaryAudioSource;
    [SerializeField] private AudioSource secondaryAudioSource;
    [Header("Audio Clips References")]
    [SerializeField] private AudioClip mainEngineSFX;
    [SerializeField] private AudioClip sideEngineSFX;
    [SerializeField] private AudioClip explosionSFX;

    private PlayerController playerController;
    private EngineController engineController;
    private PlayerLandingController playerLandingController;

    private bool playingMainEngineSFXOnLoop = false;
    private bool playingSideEngineSFXOnLoop = false;

   
    private void Awake()
    {
        playerController = GetComponentInParent<PlayerController>();
        engineController = playerController.GetComponentInChildren<EngineController>();
        playerLandingController = playerController.GetComponent<PlayerLandingController>();

        SettingsController.OnSFXVolumeChange += SettingsController_OnSFXVolumeChange;
        SetAudioSourceVolume();
    }

    private void Start()
    {
        if (playerController)
        {
            playerController.OnPlayerCrash += PlayerController_OnPlayerCrash;
            playerController.OnPlayerReset += PlayerController_OnPlayerReset;
        }

        if (engineController)
        {
            engineController.OnMainEngineStateChange += EngineController_OnMainEngineStateChange;
            engineController.OnSideEngineStateChange += EngineController_OnSideEngineStateChange;
        }

        if (playerLandingController)
        {
            playerLandingController.OnPreLandingStart += PlayerLandingController_OnPreLandingStart;
            playerLandingController.OnLandingFinished += PlayerLandingController_OnLandingFinished;
            playerLandingController.OnSideEngineStarted += PlayerLandingController_OnSideEngineStarted;
            playerLandingController.OnSideEngineStoped += PlayerLandingController_OnSideEngineStoped;
        }
    }

    private void OnDestroy()
    {
        SettingsController.OnSFXVolumeChange -= SettingsController_OnSFXVolumeChange;

        if (playerController)
        {
            playerController.OnPlayerCrash -= PlayerController_OnPlayerCrash;
            playerController.OnPlayerReset -= PlayerController_OnPlayerReset;
        }

        if (engineController)
        {
            engineController.OnMainEngineStateChange -= EngineController_OnMainEngineStateChange;
            engineController.OnSideEngineStateChange -= EngineController_OnSideEngineStateChange;
        }

        if (playerLandingController)
        {
            playerLandingController.OnPreLandingStart -= PlayerLandingController_OnPreLandingStart;
            playerLandingController.OnLandingFinished -= PlayerLandingController_OnLandingFinished;
            playerLandingController.OnSideEngineStarted -= PlayerLandingController_OnSideEngineStarted;
            playerLandingController.OnSideEngineStoped -= PlayerLandingController_OnSideEngineStoped;
        }
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

    private void PlayMainEngineSFXOnLoop()
    {
        StartCoroutine(MainEngineSFXOnLoopRoutine());
    }

    private void StopPlayingMainEngineSFXOnLoop()
    {
        playingMainEngineSFXOnLoop = false;        
    }

    private void PlaySideEngineSFXOnLoop()
    {
        StartCoroutine(SideEngineSFXOnLoopRoutine());
    }

    private void StopPlayingSideEngineSFXOnLoop()
    {
        playingSideEngineSFXOnLoop = false;        
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
        primaryAudioSource.PlayOneShot(explosionSFX);
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

    private void SettingsController_OnSFXVolumeChange()
    {
        SetAudioSourceVolume();
    }

    private void PlayerController_OnPlayerReset(object sender, EventArgs e)
    {
        primaryAudioSource.Stop();
    }

    private void PlayerLandingController_OnPreLandingStart(object sender, EventArgs e)
    {
        PlayMainEngineSFXOnLoop();
    }

    private void PlayerLandingController_OnLandingFinished(object sender, EventArgs e)
    {
        StopPlayingMainEngineSFXOnLoop();
    }

    private void PlayerLandingController_OnSideEngineStarted(PlayerMovement.RotationDirection rotationDirection)
    {
        PlaySideEngineSFXOnLoop();
    }

    private void PlayerLandingController_OnSideEngineStoped(PlayerMovement.RotationDirection rotationDirection)
    {
        StopPlayingSideEngineSFXOnLoop();
    }

    private IEnumerator MainEngineSFXOnLoopRoutine()
    {
        playingMainEngineSFXOnLoop = true;
        while (playingMainEngineSFXOnLoop)
        {
            PlayMainEngineSFX();
            yield return null;
        }
        StopPlayingMainEngineSFX();
    }

    private IEnumerator SideEngineSFXOnLoopRoutine()
    {
        playingSideEngineSFXOnLoop = true;
        while (playingSideEngineSFXOnLoop)
        {
            PlaySideEngineSFX();
            yield return null;
        }
        StopPlayingSideEngineSFX();
    }


}
