using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MainEngineVFXController : MonoBehaviour
{
    private ParticleSystem[] mainEngineParticleSystems;
    private EngineController engineController;
    private PlayerLandingController playerLandingController;
    private PlayerController playerController;

    private void Awake()
    {
        mainEngineParticleSystems = GetComponentsInChildren<ParticleSystem>();       
        engineController = GetComponentInParent<EngineController>();
        playerLandingController = GetComponentInParent<PlayerLandingController>();
        playerController = GetComponentInParent<PlayerController>();
    }

    private void Start()
    {
        if (engineController)
        {
            engineController.OnMainEngineStateChange += EngineController_OnMainEngineStateChange;
        }

        if (playerLandingController)
        {
            playerLandingController.OnPreLandingStart += PlayerLandingController_OnPreLandingStart;
            playerLandingController.OnLandingFinished += PlayerLandingController_OnLandingFinished;
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
            engineController.OnMainEngineStateChange -= EngineController_OnMainEngineStateChange;
        }

        if (playerLandingController)
        {
            playerLandingController.OnPreLandingStart -= PlayerLandingController_OnPreLandingStart;
            playerLandingController.OnLandingFinished -= PlayerLandingController_OnLandingFinished;
        }

        if (playerController)
        {
            playerController.OnPlayerCrash -= PlayerController_OnPlayerCrash;
        }
    }

    private void ToggleEngineVFX(bool isEngineOn)
    {
        foreach (ParticleSystem particleSystem in mainEngineParticleSystems)
        {
            if (isEngineOn)
            {
                particleSystem.Play();
            }
            else
            {
                particleSystem.Stop();
            }            
        }
    }


    private void EngineController_OnMainEngineStateChange(bool engineState)
    {
        ToggleEngineVFX(engineState);
    }

    private void PlayerLandingController_OnPreLandingStart(object sender, EventArgs e)
    {
        ToggleEngineVFX(true);
    }

    private void PlayerLandingController_OnLandingFinished(object sender, EventArgs e)
    {
        ToggleEngineVFX(false);
    }

    private void PlayerController_OnPlayerCrash()
    {
        ToggleEngineVFX(false);
    }
}
