using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MainEngineVFXController : MonoBehaviour
{
    private ParticleSystem[] mainEngineParticleSystems;
    private PlayerMovement playerMovement;

    private void Awake()
    {
        mainEngineParticleSystems = GetComponentsInChildren<ParticleSystem>();
        playerMovement = GetComponentInParent<PlayerMovement>();
    }

    private void Start()
    {
        if (playerMovement)
        {
            playerMovement.OnStartMovingUpwards += PlayerMovement_OnStartMovingUpwars;
            playerMovement.OnStopMovingUpwards += PlayerMovement_OnStopMovingUpwars;
        }
       
    }

    private void OnDestroy()
    {
        if (playerMovement)
        {
            playerMovement.OnStartMovingUpwards -= PlayerMovement_OnStartMovingUpwars;
            playerMovement.OnStopMovingUpwards -= PlayerMovement_OnStopMovingUpwars;
        }
    }

    private void TurnOnEngineVFX()
    {
        foreach (ParticleSystem particleSystem in mainEngineParticleSystems)
        {
            particleSystem.Play();
        }
    }

    private void TurnOffEngineVFX()
    {
        foreach (ParticleSystem particleSystem in mainEngineParticleSystems)
        {
            particleSystem.Stop();
        }
    }

    private void PlayerMovement_OnStartMovingUpwars(object sender, EventArgs e)
    {       
        TurnOnEngineVFX();
    }

    private void PlayerMovement_OnStopMovingUpwars(object sender, EventArgs e)
    {       
        TurnOffEngineVFX();        
    }
}
