using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SideEngineVFXController : MonoBehaviour
{
    [Header("Side Engine VFX Controller")]
    [SerializeField] PlayerMovement.RotationDirection engineSide;

    private ParticleSystem[] sideEngineParticleSystems;    
    private EngineController engineController;
    private PlayerLandingController playerLandingController;
    private PlayerController playerController;

    private void Awake()
    {
        sideEngineParticleSystems = GetComponentsInChildren<ParticleSystem>();
        engineController = GetComponentInParent<EngineController>();
        playerLandingController = GetComponentInParent<PlayerLandingController>();
        playerController = GetComponentInParent<PlayerController>();
    }

    private void Start()
    {
        if (engineController)
        {
            engineController.OnSideEngineStateChange += EngineController_OnSideEngineStateChange;            
        }

        if (playerLandingController)
        {
            playerLandingController.OnSideEngineStarted += PlayerLandingController_OnSideEngineStarted;
            playerLandingController.OnSideEngineStoped += PlayerLandingController_OnSideEngineStoped;
        }

        if (playerController)
        {
            playerController.OnPlayerCrash += PlayerController_OnPlayerCrash;
        }
    }

    private void OnDestroy()
    {
        if (engineController)
        {          
            engineController.OnSideEngineStateChange -= EngineController_OnSideEngineStateChange;
        }

        if (playerLandingController)
        {
            playerLandingController.OnSideEngineStarted -= PlayerLandingController_OnSideEngineStarted;
            playerLandingController.OnSideEngineStoped -= PlayerLandingController_OnSideEngineStoped;
        }

        if (playerController)
        {
            playerController.OnPlayerCrash -= PlayerController_OnPlayerCrash;
        }
    }

    private void TurnOnEngineVFX()
    {
        foreach (ParticleSystem particleSystem in sideEngineParticleSystems)
        {
            particleSystem.Play();
        }
    }

    private void TurnOffEngineVFX()
    {
        foreach (ParticleSystem particleSystem in sideEngineParticleSystems)
        {
            particleSystem.Stop();
        }
    }

    private void EngineController_OnSideEngineStateChange(PlayerMovement.RotationDirection rotationDirection, bool engineState)
    {
        if (engineSide != rotationDirection)
        {
            return;
        }

        if (engineState)
        {
            TurnOnEngineVFX();
        }
        else
        {
            TurnOffEngineVFX();
        }       
    }

    private void PlayerLandingController_OnSideEngineStarted(PlayerMovement.RotationDirection rotationDirection)
    {
        if (engineSide != rotationDirection)
        {
            return;
        }

        TurnOnEngineVFX();
    }

    private void PlayerLandingController_OnSideEngineStoped(PlayerMovement.RotationDirection rotationDirection)
    {
        if (engineSide != rotationDirection)
        {
            return;
        }

        TurnOffEngineVFX();
    }

    private void PlayerController_OnPlayerCrash()
    {
        TurnOffEngineVFX();
    }

}
