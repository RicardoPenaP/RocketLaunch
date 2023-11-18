using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MainEngineVFXController : MonoBehaviour
{
    private ParticleSystem[] mainEngineParticleSystems;
    private EngineController engineController;

    private void Awake()
    {
        mainEngineParticleSystems = GetComponentsInChildren<ParticleSystem>();       
        engineController = GetComponent<EngineController>();
    }

    private void Start()
    {
        if (engineController)
        {
            engineController.OnIsEngineOnChange += EngineController_OnIsEngineOnChange;
        }
    }

    private void OnDestroy()
    {
        if (engineController)
        {
            engineController.OnIsEngineOnChange -= EngineController_OnIsEngineOnChange;
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


    private void EngineController_OnIsEngineOnChange(bool isEngineOn)
    {
        ToggleEngineVFX(isEngineOn);
    }
}
