using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideEngineVFXController : MonoBehaviour
{
    private ParticleSystem[] sideEngineParticleSystems;

    private void Awake()
    {
        sideEngineParticleSystems = GetComponentsInChildren<ParticleSystem>();       
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

}
