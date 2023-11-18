using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideEngineVFXController : MonoBehaviour
{
    [Header("Side Engine VFX Controller")]
    [SerializeField] PlayerMovement.RotationDirection engineSide;

    private ParticleSystem[] sideEngineParticleSystems;
    private PlayerMovement playerMovement;
    private EngineController engineController;

    private void Awake()
    {
        sideEngineParticleSystems = GetComponentsInChildren<ParticleSystem>();
        engineController = GetComponentInParent<EngineController>();
    }

    private void Start()
    {
        if (engineController)
        {
            engineController.OnSideEngineOn += EngineController_OnSideEngineStateChange;            
        }
    }

    private void OnDestroy()
    {
        if (engineController)
        {          
            engineController.OnSideEngineOn -= EngineController_OnSideEngineStateChange;
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


}
